using System.Windows.Forms;

namespace ProxyGuardian
{
    public partial class FormEditor : Form
    {
        readonly bool isEditingServer;

        public ISelected Data { get; }

        public FormEditor(ISelected data = null)
        {
            Data = data;
            this.isEditingServer = data is ServerSetting;

            InitializeComponent();
            Initialize();
        }

        void Initialize()
        {
            this.txtAddress.Label.Text = "Address  ";
            this.txtAddress.TextBox.Text = (Data as ScriptSetting)?.Address;

            this.txtPort.Label.Text = "Port        ";
            this.txtPort.TextBox.Text = (Data as ServerSetting)?.Port?.ToString();

            this.txtExceptions.Label.Text = "Excludes";
            this.txtExceptions.TextBox.Multiline = true;
            this.txtExceptions.TextBox.Height = 60;
            this.txtExceptions.TextBox.ScrollBars = ScrollBars.Vertical;
            this.txtExceptions.TextBox.Text = (Data as ServerSetting)?.Exceptions;

            if (!this.isEditingServer)
            {
                this.txtPort.Visible = false;
                this.txtExceptions.Visible = false;
                this.chkBypass.Visible = false;
                Text = "Script Editor";
            }
            else
            {
                this.chkBypass.Checked = (Data as ServerSetting)?.Bypass ?? false;
                Text = "Server Editor";
            }
        }

        void btnSave_Click(object sender, System.EventArgs e)
        {
            ((ScriptSetting)Data).Address = this.txtAddress.TextBox.Text;
            if (this.isEditingServer)
            {
                var server = (ServerSetting)Data;
                server.Port = this.txtPort.TextBox.Text.ToInt32();
                server.Exceptions = this.txtExceptions.TextBox.Text;
                server.Bypass = this.chkBypass.Checked;
            }

            Close();
        }
    }
}