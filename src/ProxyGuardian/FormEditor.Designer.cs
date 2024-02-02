using System.Windows.Forms;

namespace ProxyGuardian
{
    partial class FormEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditor));
            this.txtAddress = new ProxyGuardian.Controls.LabelTextBox();
            this.txtPort = new ProxyGuardian.Controls.LabelTextBox();
            this.txtExceptions = new ProxyGuardian.Controls.LabelTextBox();
            this.chkBypass = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.Location = new System.Drawing.Point(13, 13);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.TabIndex = 0;
            // 
            // txtPort
            // 
            this.txtPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPort.Location = new System.Drawing.Point(13, 45);
            this.txtPort.Name = "txtPort";
            this.txtPort.TabIndex = 1;
            // 
            // txtExceptions
            // 
            this.txtExceptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExceptions.Location = new System.Drawing.Point(13, 77);
            this.txtExceptions.Name = "txtExceptions";
            this.txtExceptions.TabIndex = 2;
            // 
            // chkBypass
            // 
            this.chkBypass.AutoSize = true;
            this.chkBypass.Location = new System.Drawing.Point(13, 144);
            this.chkBypass.Name = "chkBypass";
            this.chkBypass.Size = new System.Drawing.Size(96, 17);
            this.chkBypass.TabIndex = 3;
            this.chkBypass.Text = "Bypass Local";
            this.chkBypass.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(197, 144);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 181);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkBypass);
            this.Controls.Add(this.txtExceptions);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.LabelTextBox txtAddress;
        private Controls.LabelTextBox txtPort;
        private Controls.LabelTextBox txtExceptions;
        private CheckBox chkBypass;
        private Button btnSave;
    }
}