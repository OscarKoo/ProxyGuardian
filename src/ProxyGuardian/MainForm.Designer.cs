using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ProxyGuardian
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtInterval = new System.Windows.Forms.ToolStripTextBox();
            this.separatorInterval = new System.Windows.Forms.ToolStripSeparator();
            this.menuAddScript = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorScript = new System.Windows.Forms.ToolStripSeparator();
            this.menuAddServer = new System.Windows.Forms.ToolStripMenuItem();
            this.separatorServer = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStartup = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.menu;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "ProxyGuardian";
            this.notify.Visible = true;
            this.notify.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notify_MouseClick);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtInterval,
            this.separatorInterval,
            this.menuAddScript,
            this.separatorScript,
            this.menuAddServer,
            this.separatorServer,
            this.menuStartup,
            this.menuExit});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(181, 157);
            // 
            // txtInterval
            // 
            this.txtInterval.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(100, 23);
            this.txtInterval.ToolTipText = "Interval Seconds";
            this.txtInterval.TextChanged += new System.EventHandler(this.TxtInterval_TextChanged);
            // 
            // separatorInterval
            // 
            this.separatorInterval.Name = "separatorInterval";
            this.separatorInterval.Size = new System.Drawing.Size(177, 6);
            // 
            // menuAddScript
            // 
            this.menuAddScript.Name = "menuAddScript";
            this.menuAddScript.Size = new System.Drawing.Size(180, 22);
            this.menuAddScript.Text = "Add Script";
            this.menuAddScript.Click += new System.EventHandler(this.MenuAddScript_Click);
            // 
            // separatorScript
            // 
            this.separatorScript.Name = "separatorScript";
            this.separatorScript.Size = new System.Drawing.Size(177, 6);
            // 
            // menuAddServer
            // 
            this.menuAddServer.Name = "menuAddServer";
            this.menuAddServer.Size = new System.Drawing.Size(180, 22);
            this.menuAddServer.Text = "Add Server";
            this.menuAddServer.Click += new System.EventHandler(this.MenuAddServer_Click);
            // 
            // separatorServer
            // 
            this.separatorServer.Name = "separatorServer";
            this.separatorServer.Size = new System.Drawing.Size(177, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(180, 22);
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuStartup
            // 
            this.menuStartup.Name = "menuStartup";
            this.menuStartup.Size = new System.Drawing.Size(180, 22);
            this.menuStartup.Text = "Run at startup";
            this.menuStartup.Click += new System.EventHandler(this.MenuStartup_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MainForm";
            this.Text = nameof(ProxyGuardian);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuAddScript;
        private System.Windows.Forms.ToolStripSeparator separatorScript;
        private System.Windows.Forms.ToolStripMenuItem menuAddServer;
        private System.Windows.Forms.ToolStripSeparator separatorServer;
        private System.Windows.Forms.ToolStripTextBox txtInterval;
        private System.Windows.Forms.ToolStripSeparator separatorInterval;
        private System.Windows.Forms.ToolStripMenuItem menuStartup;
    }
}