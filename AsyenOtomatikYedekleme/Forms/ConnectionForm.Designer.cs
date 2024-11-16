
namespace AsyenOtomatikYedekleme.Forms
{
    partial class ConnectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.txt_ServerName = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_LoginName = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Password = new DevExpress.XtraEditors.TextEdit();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Eye = new DevExpress.XtraEditors.SimpleButton();
            this.btn_NotEye = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txt_ServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_LoginName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Password.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_ServerName
            // 
            this.txt_ServerName.Location = new System.Drawing.Point(135, 10);
            this.txt_ServerName.Name = "txt_ServerName";
            this.txt_ServerName.Properties.MaxLength = 50;
            this.txt_ServerName.Size = new System.Drawing.Size(250, 20);
            this.txt_ServerName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server Adı:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kullanıcı Adı:";
            // 
            // txt_LoginName
            // 
            this.txt_LoginName.Location = new System.Drawing.Point(135, 41);
            this.txt_LoginName.Name = "txt_LoginName";
            this.txt_LoginName.Properties.MaxLength = 20;
            this.txt_LoginName.Size = new System.Drawing.Size(250, 20);
            this.txt_LoginName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12.25F);
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Şifre:";
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(135, 74);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Properties.MaxLength = 30;
            this.txt_Password.Properties.PasswordChar = '*';
            this.txt_Password.Size = new System.Drawing.Size(250, 20);
            this.txt_Password.TabIndex = 2;
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btn_Save.Appearance.Font = new System.Drawing.Font("Tahoma", 18.25F);
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.ImageOptions.Image")));
            this.btn_Save.Location = new System.Drawing.Point(0, 121);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(393, 38);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "Kaydet";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Eye
            // 
            this.btn_Eye.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btn_Eye.Appearance.Options.UseBackColor = true;
            this.btn_Eye.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Eye.ImageOptions.Image")));
            this.btn_Eye.Location = new System.Drawing.Point(81, 74);
            this.btn_Eye.Name = "btn_Eye";
            this.btn_Eye.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Eye.Size = new System.Drawing.Size(34, 20);
            this.btn_Eye.TabIndex = 4;
            this.btn_Eye.Click += new System.EventHandler(this.btn_Eye_Click);
            // 
            // btn_NotEye
            // 
            this.btn_NotEye.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btn_NotEye.Appearance.Options.UseBackColor = true;
            this.btn_NotEye.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_NotEye.ImageOptions.Image")));
            this.btn_NotEye.Location = new System.Drawing.Point(80, 74);
            this.btn_NotEye.Name = "btn_NotEye";
            this.btn_NotEye.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_NotEye.Size = new System.Drawing.Size(35, 20);
            this.btn_NotEye.TabIndex = 5;
            this.btn_NotEye.Click += new System.EventHandler(this.btn_NotEye_Click);
            // 
            // ConnectionForm
            // 
            this.AcceptButton = this.btn_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(393, 159);
            this.Controls.Add(this.btn_NotEye);
            this.Controls.Add(this.btn_Eye);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_LoginName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_ServerName);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("ConnectionForm.IconOptions.Image")));
            this.MaximizeBox = false;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Veritabanı Bağlantı Bilgileri";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConnectionForm_FormClosed);
            this.Load += new System.EventHandler(this.ConnectionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_ServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_LoginName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Password.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txt_ServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txt_LoginName;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txt_Password;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraEditors.SimpleButton btn_Eye;
        private DevExpress.XtraEditors.SimpleButton btn_NotEye;
    }
}