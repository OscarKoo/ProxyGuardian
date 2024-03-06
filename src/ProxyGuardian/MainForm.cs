using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace ProxyGuardian
{
    public partial class MainForm : Form
    {
        readonly RegGuardian regGuardian = new RegGuardian();
        readonly ProxyConfig config;
        readonly Timer timer = new Timer();

        static readonly string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProxyConfig.json");

        public MainForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            this.config = ReadConfig();
            Initialize();
        }

        void Initialize()
        {
            this.menuStartup.Checked = this.config.RunAtStartup;
            this.txtInterval.Text = this.config.IntervalSeconds.ToString();

            foreach (var s in this.config.Scripts)
            {
                InitializeSubMenus(false, s);
            }

            foreach (var s in this.config.Servers)
            {
                InitializeSubMenus(true, s);
            }

            this.timer.Interval = 1000;
            this.timer.Tick += Timer_Tick;

            this.regGuardian.ProxyConfig = this.config;
            this.regGuardian.Start();
        }

        #region Events

        void notify_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.menu.Show(Cursor.Position);
            }
        }

        void menuExit_Click(object sender, EventArgs e)
        {
            this.regGuardian.Stop();
            Application.Exit();
        }

        void MenuStartup_Click(object sender, EventArgs e)
        {
            var nextChecked = !((ToolStripMenuItem)sender).Checked;

            const string registryPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(registryPath, true))
                {
                    if (key != null)
                    {
                        if (nextChecked)
                            key.SetValue(nameof(ProxyGuardian), Application.ExecutablePath);
                        else if (key.GetValueNames().Contains(nameof(ProxyGuardian), StringComparer.OrdinalIgnoreCase))
                            key.DeleteValue(nameof(ProxyGuardian), false);
                    }
                    else
                    {
                        Console.WriteLine("Unable to open reg.");
                    }
                }

                this.config.RunAtStartup = nextChecked;
                WriteConfig(this.config);
                ((ToolStripMenuItem)sender).Checked = nextChecked;
            }
            catch (Exception ex) { }
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            var interval = this.txtInterval.Text.ToInt32();
            if (interval > 0)
            {
                this.config.IntervalSeconds = interval;
                WriteConfig(this.config);
            }
        }

        void TxtInterval_TextChanged(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.timer.Start();
        }

        void MenuAddScript_Click(object sender, EventArgs e)
        {
            using (var form = new FormEditor(new ScriptSetting()))
            {
                form.ShowDialog(this);
                var data = form.Data;
                if (((IName)data).Name?.Length > 1)
                {
                    this.config.Scripts.Add((ScriptSetting)data);
                    InitializeSubMenus(false, data);
                    WriteConfig(this.config);
                }
            }
        }

        void MenuAddServer_Click(object sender, EventArgs e)
        {
            using (var form = new FormEditor(new ServerSetting()))
            {
                form.ShowDialog(this);
                var data = form.Data;
                if (((IName)data).Name.Length > 1)
                {
                    this.config.Servers.Add((ServerSetting)data);
                    InitializeSubMenus(true, data);
                    WriteConfig(this.config);
                }
            }
        }

        #endregion

        #region SubMenus

        readonly List<ToolStripMenuItem> scripts = new List<ToolStripMenuItem>();
        readonly List<ToolStripMenuItem> servers = new List<ToolStripMenuItem>();

        void InitializeSubMenus(bool isServer, ISelected item)
        {
            var index = this.menu.Items.IndexOf(!isServer ? this.separatorScript : this.separatorServer);
            if (index < 0) return;

            var menuItem = AddItemMenu(item);
            menuItem.DropDownItems.AddRange(new ToolStripItem[]
            {
                AddToggleMenu(isServer, item, menuItem),
                AddEditMenu(item),
                AddDeleteMenu(isServer, item, menuItem)
            });
            this.menu.Items.Insert(index, menuItem);

            if (!isServer)
                this.scripts.Add(menuItem);
            else
                this.servers.Add(menuItem);
        }

        static ToolStripMenuItem AddItemMenu(ISelected item)
        {
            var menuItem = new ToolStripMenuItem
            {
                Text = ((IName)item).Name,
                Tag = item,
                Checked = item.Selected
            };
            menuItem.DropDownOpening += (o, e) =>
            {
                if (menuItem.HasDropDownItems == false)
                {
                    return; // not a drop down item
                }

                // Current bounds of the current monitor
                var Bounds = menuItem.GetCurrentParent().Bounds;
                var CurrentScreen = Screen.FromPoint(Bounds.Location);

                // Look how big our children are:
                var maxWidth = (from ToolStripMenuItem subItem in menuItem.DropDownItems
                    select subItem.Width).Prepend(0).Max();
                maxWidth += 10; // Add a little wiggle room

                var farRight = Bounds.Right + maxWidth;
                var currentMonitorRight = CurrentScreen.Bounds.Right;

                menuItem.DropDownDirection = farRight > currentMonitorRight
                    ? ToolStripDropDownDirection.Left
                    : ToolStripDropDownDirection.Right;
            };
            return menuItem;
        }

        ToolStripMenuItem AddToggleMenu(bool isServer, ISelected item, ToolStripMenuItem menuItem)
        {
            var toggle = new ToolStripMenuItem { Text = item.Selected ? "Disable" : "Enable", };
            toggle.Click += (o, e) =>
            {
                var enabled = menuItem.Checked;

                foreach (var each in !isServer ? this.scripts : this.servers)
                {
                    each.Checked = false;
                    each.DropDownItems[0].Text = "Enable";
                }

                foreach (var each in !isServer ? (IEnumerable<ISelected>)this.config.Scripts : this.config.Servers)
                {
                    each.Selected = false;
                }

                item.Selected = !enabled;
                menuItem.Checked = !enabled;
                toggle.Text = menuItem.Checked ? "Disable" : "Enable";
                this.regGuardian.Update();
                WriteConfig(this.config);
            };
            return toggle;
        }

        ToolStripMenuItem AddEditMenu(ISelected item)
        {
            var edit = new ToolStripMenuItem { Text = "Edit" };
            edit.Click += (o, e) =>
            {
                using (var form = new FormEditor(item))
                {
                    form.ShowDialog(this);
                    WriteConfig(this.config);
                }
            };
            return edit;
        }

        ToolStripMenuItem AddDeleteMenu(bool isServer, ISelected item, ToolStripMenuItem menuItem)
        {
            var delete = new ToolStripMenuItem { Text = "Delete" };
            delete.Click += (o, e) =>
            {
                this.menu.Items.Remove(menuItem);
                if (!isServer)
                {
                    this.config.Scripts.Remove((ScriptSetting)item);
                    this.scripts.Remove(menuItem);
                }
                else
                {
                    this.config.Servers.Remove((ServerSetting)item);
                    this.servers.Remove(menuItem);
                }

                WriteConfig(this.config);
            };
            return delete;
        }

        #endregion

        static ProxyConfig ReadConfig()
        {
            if (!File.Exists(configFile))
                return new ProxyConfig();

            var content = File.ReadAllText(configFile);
            return string.IsNullOrWhiteSpace(content)
                ? new ProxyConfig()
                : JsonConvert.DeserializeObject<ProxyConfig>(content);
        }

        static void WriteConfig(ProxyConfig cfg)
        {
            if (cfg == null)
                return;

            var content = JsonConvert.SerializeObject(cfg, Formatting.Indented);
            File.WriteAllText(configFile, content);
        }
    }
}