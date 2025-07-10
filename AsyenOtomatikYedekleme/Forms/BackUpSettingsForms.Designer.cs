
namespace AsyenOtomatikYedekleme.Forms
{
    partial class BackUpSettingsForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackUpSettingsForms));
            this.timeEdit1 = new DevExpress.XtraEditors.TimeEdit();
            this.chk_Monday = new System.Windows.Forms.CheckBox();
            this.chk_Tuesday = new System.Windows.Forms.CheckBox();
            this.chk_Wednesday = new System.Windows.Forms.CheckBox();
            this.chk_Thursday = new System.Windows.Forms.CheckBox();
            this.chk_Friday = new System.Windows.Forms.CheckBox();
            this.chk_Saturday = new System.Windows.Forms.CheckBox();
            this.chk_Sunday = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.chc_AllChoose = new System.Windows.Forms.CheckBox();
            this.btn_Save = new DevExpress.XtraEditors.SimpleButton();
            this.txt_FolderBackUp = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_WinrarPassword = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_WinrarFolder = new DevExpress.XtraEditors.TextEdit();
            this.btn_NotEye = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Eye = new DevExpress.XtraEditors.SimpleButton();
            this.chk_BackDelete = new System.Windows.Forms.CheckBox();
            this.btn_Exit = new DevExpress.XtraEditors.SimpleButton();
            this.btn_FolderBackUp = new DevExpress.XtraEditors.SimpleButton();
            this.btn_WinrarFolder = new DevExpress.XtraEditors.SimpleButton();
            this.txt_recipientEmail = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Company = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FolderBackUp.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_WinrarPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_WinrarFolder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_recipientEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Company.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // timeEdit1
            // 
            this.timeEdit1.EditValue = new System.DateTime(2024, 9, 7, 0, 0, 0, 0);
            this.timeEdit1.Location = new System.Drawing.Point(186, 26);
            this.timeEdit1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.timeEdit1.Name = "timeEdit1";
            this.timeEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timeEdit1.Size = new System.Drawing.Size(163, 24);
            this.timeEdit1.TabIndex = 0;
            // 
            // chk_Monday
            // 
            this.chk_Monday.AutoSize = true;
            this.chk_Monday.Location = new System.Drawing.Point(12, 32);
            this.chk_Monday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_Monday.Name = "chk_Monday";
            this.chk_Monday.Size = new System.Drawing.Size(83, 21);
            this.chk_Monday.TabIndex = 3;
            this.chk_Monday.Text = "Pazartesi";
            this.chk_Monday.UseVisualStyleBackColor = true;
            // 
            // chk_Tuesday
            // 
            this.chk_Tuesday.AutoSize = true;
            this.chk_Tuesday.Location = new System.Drawing.Point(119, 33);
            this.chk_Tuesday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_Tuesday.Name = "chk_Tuesday";
            this.chk_Tuesday.Size = new System.Drawing.Size(49, 21);
            this.chk_Tuesday.TabIndex = 4;
            this.chk_Tuesday.Text = "Salı";
            this.chk_Tuesday.UseVisualStyleBackColor = true;
            // 
            // chk_Wednesday
            // 
            this.chk_Wednesday.AutoSize = true;
            this.chk_Wednesday.Location = new System.Drawing.Point(208, 33);
            this.chk_Wednesday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_Wednesday.Name = "chk_Wednesday";
            this.chk_Wednesday.Size = new System.Drawing.Size(91, 21);
            this.chk_Wednesday.TabIndex = 5;
            this.chk_Wednesday.Text = "Çarşamba";
            this.chk_Wednesday.UseVisualStyleBackColor = true;
            // 
            // chk_Thursday
            // 
            this.chk_Thursday.AutoSize = true;
            this.chk_Thursday.Location = new System.Drawing.Point(12, 60);
            this.chk_Thursday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_Thursday.Name = "chk_Thursday";
            this.chk_Thursday.Size = new System.Drawing.Size(90, 21);
            this.chk_Thursday.TabIndex = 6;
            this.chk_Thursday.Text = "Perşembe";
            this.chk_Thursday.UseVisualStyleBackColor = true;
            // 
            // chk_Friday
            // 
            this.chk_Friday.AutoSize = true;
            this.chk_Friday.Location = new System.Drawing.Point(119, 61);
            this.chk_Friday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_Friday.Name = "chk_Friday";
            this.chk_Friday.Size = new System.Drawing.Size(66, 21);
            this.chk_Friday.TabIndex = 7;
            this.chk_Friday.Text = "Cuma";
            this.chk_Friday.UseVisualStyleBackColor = true;
            // 
            // chk_Saturday
            // 
            this.chk_Saturday.AutoSize = true;
            this.chk_Saturday.Location = new System.Drawing.Point(208, 61);
            this.chk_Saturday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_Saturday.Name = "chk_Saturday";
            this.chk_Saturday.Size = new System.Drawing.Size(91, 21);
            this.chk_Saturday.TabIndex = 8;
            this.chk_Saturday.Text = "Cumartesi";
            this.chk_Saturday.UseVisualStyleBackColor = true;
            // 
            // chk_Sunday
            // 
            this.chk_Sunday.AutoSize = true;
            this.chk_Sunday.Location = new System.Drawing.Point(12, 89);
            this.chk_Sunday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_Sunday.Name = "chk_Sunday";
            this.chk_Sunday.Size = new System.Drawing.Size(63, 21);
            this.chk_Sunday.TabIndex = 9;
            this.chk_Sunday.Text = "Pazar";
            this.chk_Sunday.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Yedekleme Saati:";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.chc_AllChoose);
            this.groupControl1.Controls.Add(this.chk_Monday);
            this.groupControl1.Controls.Add(this.chk_Tuesday);
            this.groupControl1.Controls.Add(this.chk_Sunday);
            this.groupControl1.Controls.Add(this.chk_Wednesday);
            this.groupControl1.Controls.Add(this.chk_Saturday);
            this.groupControl1.Controls.Add(this.chk_Thursday);
            this.groupControl1.Controls.Add(this.chk_Friday);
            this.groupControl1.Location = new System.Drawing.Point(17, 224);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(332, 126);
            this.groupControl1.TabIndex = 11;
            this.groupControl1.Text = "Yedek Alınacak Günler";
            // 
            // chc_AllChoose
            // 
            this.chc_AllChoose.AutoSize = true;
            this.chc_AllChoose.Location = new System.Drawing.Point(119, 90);
            this.chc_AllChoose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chc_AllChoose.Name = "chc_AllChoose";
            this.chc_AllChoose.Size = new System.Drawing.Size(98, 21);
            this.chc_AllChoose.TabIndex = 10;
            this.chc_AllChoose.Text = "Hepsini Seç";
            this.chc_AllChoose.UseVisualStyleBackColor = true;
            this.chc_AllChoose.CheckedChanged += new System.EventHandler(this.chc_AllChoose_CheckedChanged);
            // 
            // btn_Save
            // 
            this.btn_Save.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btn_Save.Appearance.Font = new System.Drawing.Font("Tahoma", 18.25F);
            this.btn_Save.Appearance.Options.UseBackColor = true;
            this.btn_Save.Appearance.Options.UseFont = true;
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.ImageOptions.Image")));
            this.btn_Save.Location = new System.Drawing.Point(12, 407);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(194, 48);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "Kaydet";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // txt_FolderBackUp
            // 
            this.txt_FolderBackUp.Location = new System.Drawing.Point(186, 58);
            this.txt_FolderBackUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_FolderBackUp.Name = "txt_FolderBackUp";
            this.txt_FolderBackUp.Properties.MaxLength = 50;
            this.txt_FolderBackUp.Size = new System.Drawing.Size(163, 22);
            this.txt_FolderBackUp.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Yedek Klasörü:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Winrar Şifresi:";
            // 
            // txt_WinrarPassword
            // 
            this.txt_WinrarPassword.Location = new System.Drawing.Point(186, 126);
            this.txt_WinrarPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_WinrarPassword.Name = "txt_WinrarPassword";
            this.txt_WinrarPassword.Properties.MaxLength = 50;
            this.txt_WinrarPassword.Properties.PasswordChar = '*';
            this.txt_WinrarPassword.Size = new System.Drawing.Size(163, 22);
            this.txt_WinrarPassword.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label4.Location = new System.Drawing.Point(14, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 20;
            this.label4.Text = "Winrar Yolu:";
            // 
            // txt_WinrarFolder
            // 
            this.txt_WinrarFolder.Location = new System.Drawing.Point(186, 90);
            this.txt_WinrarFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_WinrarFolder.Name = "txt_WinrarFolder";
            this.txt_WinrarFolder.Properties.MaxLength = 100;
            this.txt_WinrarFolder.Size = new System.Drawing.Size(163, 22);
            this.txt_WinrarFolder.TabIndex = 2;
            // 
            // btn_NotEye
            // 
            this.btn_NotEye.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btn_NotEye.Appearance.Options.UseBackColor = true;
            this.btn_NotEye.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_NotEye.ImageOptions.Image")));
            this.btn_NotEye.Location = new System.Drawing.Point(116, 127);
            this.btn_NotEye.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_NotEye.Name = "btn_NotEye";
            this.btn_NotEye.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_NotEye.Size = new System.Drawing.Size(42, 25);
            this.btn_NotEye.TabIndex = 22;
            this.btn_NotEye.Click += new System.EventHandler(this.btn_NotEye_Click);
            // 
            // btn_Eye
            // 
            this.btn_Eye.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btn_Eye.Appearance.Options.UseBackColor = true;
            this.btn_Eye.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Eye.ImageOptions.Image")));
            this.btn_Eye.Location = new System.Drawing.Point(117, 128);
            this.btn_Eye.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Eye.Name = "btn_Eye";
            this.btn_Eye.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_Eye.Size = new System.Drawing.Size(41, 25);
            this.btn_Eye.TabIndex = 21;
            this.btn_Eye.Click += new System.EventHandler(this.btn_Eye_Click);
            // 
            // chk_BackDelete
            // 
            this.chk_BackDelete.AutoSize = true;
            this.chk_BackDelete.Location = new System.Drawing.Point(17, 367);
            this.chk_BackDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chk_BackDelete.Name = "chk_BackDelete";
            this.chk_BackDelete.Size = new System.Drawing.Size(267, 21);
            this.chk_BackDelete.TabIndex = 6;
            this.chk_BackDelete.Text = "Yedekleme Sonrası Eski Yedekler Silinsin";
            this.chk_BackDelete.UseVisualStyleBackColor = true;
            // 
            // btn_Exit
            // 
            this.btn_Exit.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.btn_Exit.Appearance.Font = new System.Drawing.Font("Tahoma", 18.25F);
            this.btn_Exit.Appearance.Options.UseBackColor = true;
            this.btn_Exit.Appearance.Options.UseFont = true;
            this.btn_Exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Exit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Exit.ImageOptions.Image")));
            this.btn_Exit.Location = new System.Drawing.Point(217, 407);
            this.btn_Exit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(194, 48);
            this.btn_Exit.TabIndex = 8;
            this.btn_Exit.Text = "Vazgeç";
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_FolderBackUp
            // 
            this.btn_FolderBackUp.Appearance.ForeColor = System.Drawing.Color.Red;
            this.btn_FolderBackUp.Appearance.Options.UseForeColor = true;
            this.btn_FolderBackUp.Location = new System.Drawing.Point(116, 61);
            this.btn_FolderBackUp.Name = "btn_FolderBackUp";
            this.btn_FolderBackUp.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_FolderBackUp.Size = new System.Drawing.Size(27, 25);
            this.btn_FolderBackUp.TabIndex = 8;
            this.btn_FolderBackUp.Text = "...";
            this.btn_FolderBackUp.Click += new System.EventHandler(this.btn_FolderBackUp_Click);
            // 
            // btn_WinrarFolder
            // 
            this.btn_WinrarFolder.Appearance.ForeColor = System.Drawing.Color.Red;
            this.btn_WinrarFolder.Appearance.Options.UseForeColor = true;
            this.btn_WinrarFolder.Location = new System.Drawing.Point(98, 93);
            this.btn_WinrarFolder.Name = "btn_WinrarFolder";
            this.btn_WinrarFolder.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btn_WinrarFolder.Size = new System.Drawing.Size(27, 25);
            this.btn_WinrarFolder.TabIndex = 9;
            this.btn_WinrarFolder.Text = "...";
            this.btn_WinrarFolder.Click += new System.EventHandler(this.btn_WinrarFolder_Click);
            // 
            // txt_recipientEmail
            // 
            this.txt_recipientEmail.Location = new System.Drawing.Point(186, 158);
            this.txt_recipientEmail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_recipientEmail.Name = "txt_recipientEmail";
            this.txt_recipientEmail.Properties.MaxLength = 50;
            this.txt_recipientEmail.Size = new System.Drawing.Size(163, 22);
            this.txt_recipientEmail.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 17);
            this.label5.TabIndex = 27;
            this.label5.Text = "Mail Kime Gönderilecek:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 17);
            this.label6.TabIndex = 29;
            this.label6.Text = "Şirket Adı:";
            // 
            // txt_Company
            // 
            this.txt_Company.Location = new System.Drawing.Point(186, 188);
            this.txt_Company.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_Company.Name = "txt_Company";
            this.txt_Company.Properties.MaxLength = 50;
            this.txt_Company.Size = new System.Drawing.Size(163, 22);
            this.txt_Company.TabIndex = 5;
            // 
            // BackUpSettingsForms
            // 
            this.AcceptButton = this.btn_Save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(434, 470);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Company);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_recipientEmail);
            this.Controls.Add(this.btn_WinrarFolder);
            this.Controls.Add(this.btn_FolderBackUp);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.chk_BackDelete);
            this.Controls.Add(this.btn_NotEye);
            this.Controls.Add(this.btn_Eye);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_WinrarFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_WinrarPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_FolderBackUp);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeEdit1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("BackUpSettingsForms.IconOptions.Image")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "BackUpSettingsForms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayarlar";
            this.Load += new System.EventHandler(this.BackUpSettingscs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_FolderBackUp.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_WinrarPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_WinrarFolder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_recipientEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_Company.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TimeEdit timeEdit1;
        private System.Windows.Forms.CheckBox chk_Monday;
        private System.Windows.Forms.CheckBox chk_Tuesday;
        private System.Windows.Forms.CheckBox chk_Wednesday;
        private System.Windows.Forms.CheckBox chk_Thursday;
        private System.Windows.Forms.CheckBox chk_Friday;
        private System.Windows.Forms.CheckBox chk_Saturday;
        private System.Windows.Forms.CheckBox chk_Sunday;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btn_Save;
        private DevExpress.XtraEditors.TextEdit txt_FolderBackUp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.TextEdit txt_WinrarPassword;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit txt_WinrarFolder;
        private DevExpress.XtraEditors.SimpleButton btn_NotEye;
        private DevExpress.XtraEditors.SimpleButton btn_Eye;
        private System.Windows.Forms.CheckBox chk_BackDelete;
        private System.Windows.Forms.CheckBox chc_AllChoose;
        private DevExpress.XtraEditors.SimpleButton btn_Exit;
        private DevExpress.XtraEditors.SimpleButton btn_FolderBackUp;
        private DevExpress.XtraEditors.SimpleButton btn_WinrarFolder;
        private DevExpress.XtraEditors.TextEdit txt_recipientEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit txt_Company;
    }
}