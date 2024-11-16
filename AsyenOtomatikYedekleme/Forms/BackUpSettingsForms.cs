using AsyenOtomatikYedekleme.Bussiness;
using AsyenOtomatikYedekleme.Classes;
using AsyenYedekleme.Classes;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Forms
{
    public partial class BackUpSettingsForms : XtraForm
    {
        public BackUpSettingsForms()
        {
            InitializeComponent();
        }
        private bool checkStatus = false;
        private bool status = false;
        private void BackUpSettingscs_Load(object sender, EventArgs e)
        {
            string winrarPath = string.Empty;
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe"))
                {
                    if (key != null)
                        winrarPath = key.GetValue("").ToString();
                }
                if (string.IsNullOrEmpty(winrarPath))
                {
                    XtraMessageBox.Show("WinRAR yüklü değil!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "https://www.win-rar.com/start.html?&L=5",
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txt_WinrarFolder.Text = winrarPath.Replace("WinRAR.exe","Rar.exe");
            DataTable table1 = SQLLiteConnection.GetDataFromSQLite("SELECT RecipientEmail FROM EMailSetting LIMIT 1");
            if (table1.Rows.Count > 0)
                txt_recipientEmail.Text = table1.Rows[0][0].ToString();
            btn_Eye.Visible = false;
            try
            {
                DataTable table = SQLLiteConnection.GetDataFromSQLite("SELECT BackUpTime,BackUpFolder,WinrarFolder,WinrarPassword,Days,BackUpDelete FROM DbBackUpSettings LIMIT 1");
                if (table.Rows.Count > 0)
                {
                    timeEdit1.EditValue = table.Rows[0][0].ToString();
                    txt_FolderBackUp.Text = table.Rows[0][1].ToString();
                    if (!string.IsNullOrEmpty(table.Rows[0][2].ToString()))
                        txt_WinrarFolder.Text = table.Rows[0][2].ToString();
                     txt_WinrarPassword.Text = EncryptionHelper.Decrypt(table.Rows[0][3].ToString());
                    chk_Monday.Checked = table.Rows[0][4].ToString().Contains("Pazartesi") ? true : false;
                    chk_Tuesday.Checked = table.Rows[0][4].ToString().Contains("Salı") ? true : false;
                    chk_Wednesday.Checked = table.Rows[0][4].ToString().Contains("Çarşamba") ? true : false;
                    chk_Thursday.Checked = table.Rows[0][4].ToString().Contains("Perşembe") ? true : false;
                    chk_Friday.Checked = table.Rows[0][4].ToString().Contains("Cuma") ? true : false;
                    chk_Saturday.Checked = table.Rows[0][4].ToString().Contains("Cumartesi") ? true : false;
                    chk_Sunday.Checked = table.Rows[0][4].ToString()
                        .Split(',')
                        .Any(day => day.Trim() == "Pazar");
                    chk_BackDelete.Checked = table.Rows[0][5].ToString() == "1" ? true : false;

                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Yedekleme Bilgileri Okunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging(ex.Message);
                status = false;
            }
            if (string.IsNullOrEmpty(txt_WinrarFolder.Text))
                txt_WinrarFolder.Text = @"C:\Program Files\WinRAR\Rar.exe";
        }
        private void btn_NotEye_Click(object sender, EventArgs e)
        {
            txt_WinrarPassword.Focus();
            btn_Eye.Visible = true;
            btn_NotEye.Visible = false;
            txt_WinrarPassword.Properties.PasswordChar = '\0';
        }
        private void btn_Eye_Click(object sender, EventArgs e)
        {
            txt_WinrarPassword.Focus();
            btn_Eye.Visible = false;
            btn_NotEye.Visible = true;
            txt_WinrarPassword.Properties.PasswordChar = '*';
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (BackUpSettingsManager.BackUpSettingControl(txt_FolderBackUp, txt_WinrarFolder))
            {
                if (chk_Monday.Checked == false && chk_Tuesday.Checked == false && chk_Wednesday.Checked == false && chk_Thursday.Checked == false && chk_Friday.Checked == false && chk_Saturday.Checked == false && chk_Sunday.Checked == false)
                {
                    XtraMessageBox.Show("Günlerden En Az Biri Seçilmelidir", "Hatalı Gün Seçimi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    string days = "";
                    days += chk_Monday.Checked ? "Pazartesi," : "";
                    days += chk_Tuesday.Checked ? "Salı," : "";
                    days += chk_Wednesday.Checked ? "Çarşamba," : "";
                    days += chk_Thursday.Checked ? "Perşembe," : "";
                    days += chk_Friday.Checked ? "Cuma," : "";
                    days += chk_Saturday.Checked ? "Cumartesi," : "";
                    days += chk_Sunday.Checked ? "Pazar," : "";
                    if (days.Length > 0)
                        days = days.Substring(0, days.Length - 1);
                    if (status == true)
                    {
                        string message = SQLLiteConnection.InserUpdateDelete("UPDATE DbBackUpSettings SET BackUpTime='" + Convert.ToDateTime(timeEdit1.EditValue).ToString("HH:mm:ss") + "',BackUpFolder='" + txt_FolderBackUp.Text + "',WinrarFolder='" + txt_WinrarFolder.Text + "',WinrarPassword='" + EncryptionHelper.Encrypt(txt_WinrarPassword.Text) + "', Days='" + days + "',BackUpDelete=" + (chk_BackDelete.Checked == true ? 1 : 0) + "");
                        string message1 = SQLLiteConnection.InserUpdateDelete("UPDATE EMailSetting SET RecipientEmail='" + txt_recipientEmail.Text + "'");
                        if (message == "Başarılı" && message1 == "Başarılı")
                            this.Close();
                        else
                        {
                            XtraMessageBox.Show(message, "Hatalı Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TextLog.TextLogging(message);
                        }
                    }
                    else
                    {
                        string message = SQLLiteConnection.InserUpdateDelete("INSERT INTO DbBackUpSettings (BackUpTime,BackUpFolder,WinrarFolder,WinrarPassword,Days,BackUpDelete) VALUES ('" + Convert.ToDateTime(timeEdit1.EditValue).ToString("HH:mm:ss") + "','" + txt_FolderBackUp.Text + "','" + txt_WinrarFolder.Text + "','" + EncryptionHelper.Encrypt(txt_WinrarPassword.Text) + "','" + days + "'," + (chk_BackDelete.Checked == true ? 1 : 0) + ")");
                        string message1 = SQLLiteConnection.InserUpdateDelete("UPDATE EMailSetting SET RecipientEmail='" + txt_recipientEmail.Text + "'");
                        if (message == "Başarılı" && message1 == "Başarılı")
                            this.Close();
                        else
                        {
                            XtraMessageBox.Show(message, "Hatalı Ekleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TextLog.TextLogging(message);
                        }
                        status = true;
                    }
                }
            }
        }
        private void chc_AllChoose_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkStatus)
            {
                chk_Monday.Checked = true;
                chk_Tuesday.Checked = true;
                chk_Wednesday.Checked = true;
                chk_Thursday.Checked = true;
                chk_Friday.Checked = true;
                chk_Saturday.Checked = true;
                chk_Sunday.Checked = true;
                checkStatus = true;
            }
            else
            {
                checkStatus = false;
                chk_Monday.Checked = false;
                chk_Tuesday.Checked = false;
                chk_Wednesday.Checked = false;
                chk_Thursday.Checked = false;
                chk_Friday.Checked = false;
                chk_Saturday.Checked = false;
                chk_Sunday.Checked = false;
            }
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_FolderBackUp_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Lütfen yedek alınacak klasörü seçiniz.";
                folderDialog.ShowNewFolderButton = true; // Yeni klasör oluşturma butonunu göster
                if (folderDialog.ShowDialog() == DialogResult.OK)
                    txt_FolderBackUp.Text = folderDialog.SelectedPath;
            }
        }
        private void btn_WinrarFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Lütfen winrar klasörü seçiniz.";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                    txt_WinrarFolder.Text = folderDialog.SelectedPath;
            }
        }
    }
}