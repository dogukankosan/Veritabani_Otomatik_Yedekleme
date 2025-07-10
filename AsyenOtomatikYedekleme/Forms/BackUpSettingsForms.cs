using AsyenOtomatikYedekleme.Bussiness;
using AsyenOtomatikYedekleme.Classes;
using AsyenYedekleme.Classes;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
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
        private async void BackUpSettingscs_Load(object sender, EventArgs e)
        {
            try
            {
                txt_WinrarFolder.Text = GetWinrarPathAsync();
                DataTable emailResult = await SQLiteHelper.GetDataTableAsync("SELECT RecipientEmail FROM EMailSetting LIMIT 1");
                if (emailResult.Rows.Count > 0)
                    txt_recipientEmail.Text = emailResult.Rows[0][0]?.ToString();
                btn_Eye.Visible = false;
                DataTable backupSettings = await SQLiteHelper.GetDataTableAsync("SELECT BackUpTime,BackUpFolder,WinrarFolder,WinrarPassword,Days,BackUpDelete,CompanyName FROM DbBackUpSettings LIMIT 1");
                if (backupSettings.Rows.Count > 0)
                {
                    DataRow row = backupSettings.Rows[0];
                    timeEdit1.EditValue = row[0]?.ToString();
                    txt_FolderBackUp.Text = row[1]?.ToString();
                    txt_WinrarFolder.Text = string.IsNullOrEmpty(row[2]?.ToString()) ? txt_WinrarFolder.Text : row[2].ToString();
                    txt_WinrarPassword.Text = EncryptionHelper.Decrypt(row[3]?.ToString());
                    SetDayCheckboxes(row[4]?.ToString());
                    chk_BackDelete.Checked = row[5]?.ToString() == "1";
                    txt_Company.Text = row[6]?.ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Yedekleme Bilgileri Okunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging("[Load Error] " + ex);
            }
        }
        private string GetWinrarPathAsync()
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe"))
                {
                    string path = key?.GetValue("")?.ToString();
                    if (!string.IsNullOrEmpty(path))
                        return path.Replace("WinRAR.exe", "Rar.exe");
                }
                XtraMessageBox.Show("WinRAR yüklü değil!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://www.win-rar.com/start.html?&L=5",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging("[WinRAR Path Error] " + ex);
            }
            return @"C:\\Program Files\\WinRAR\\Rar.exe";
        }
        private void SetDayCheckboxes(string days)
        {
            if (string.IsNullOrWhiteSpace(days)) return;
            List<string> selectedDays = days.Split(',').Select(d => d.Trim()).ToList();
            chk_Monday.Checked = selectedDays.Contains("Pazartesi");
            chk_Tuesday.Checked = selectedDays.Contains("Salı");
            chk_Wednesday.Checked = selectedDays.Contains("Çarşamba");
            chk_Thursday.Checked = selectedDays.Contains("Perşembe");
            chk_Friday.Checked = selectedDays.Contains("Cuma");
            chk_Saturday.Checked = selectedDays.Contains("Cumartesi");
            chk_Sunday.Checked = selectedDays.Contains("Pazar");
        }
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (!BackUpSettingsManager.BackUpSettingControl(txt_FolderBackUp, txt_WinrarFolder, txt_Company)) return;
            if (!IsAnyDaySelected())
            {
                XtraMessageBox.Show("Günlerden En Az Biri Seçilmelidir", "Hatalı Gün Seçimi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string days = GetSelectedDays();
            string time = Convert.ToDateTime(timeEdit1.EditValue).ToString("HH:mm:ss");
            string encryptedPwd = EncryptionHelper.Encrypt(txt_WinrarPassword.Text);
            try
            {
                await SQLiteHelper.ExecuteNonQueryAsync(
                    "UPDATE DbBackUpSettings SET BackUpTime=@time, BackUpFolder=@folder, WinrarFolder=@rar, WinrarPassword=@pwd, Days=@days, BackUpDelete=@del, CompanyName=@company",
                    new SQLiteParameter("@time", time),
                    new SQLiteParameter("@folder", txt_FolderBackUp.Text),
                    new SQLiteParameter("@rar", txt_WinrarFolder.Text),
                    new SQLiteParameter("@pwd", encryptedPwd),
                    new SQLiteParameter("@days", days),
                    new SQLiteParameter("@del", chk_BackDelete.Checked ? 1 : 0),
                new SQLiteParameter("@company", txt_Company.Text.Trim())
                );
                await SQLiteHelper.ExecuteNonQueryAsync(
                   "UPDATE EMailSetting SET RecipientEmail=@mail",
                   new SQLiteParameter("@mail", txt_recipientEmail.Text)
               );
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging("[Save Exception] " + ex);
            }
        }
        private bool IsAnyDaySelected() =>
            chk_Monday.Checked || chk_Tuesday.Checked || chk_Wednesday.Checked || chk_Thursday.Checked || chk_Friday.Checked || chk_Saturday.Checked || chk_Sunday.Checked;
        private string GetSelectedDays()
        {
            string[] days = new[]
            {
        chk_Monday.Checked ? "Pazartesi" : null,
        chk_Tuesday.Checked ? "Salı" : null,
        chk_Wednesday.Checked ? "Çarşamba" : null,
        chk_Thursday.Checked ? "Perşembe" : null,
        chk_Friday.Checked ? "Cuma" : null,
        chk_Saturday.Checked ? "Cumartesi" : null,
        chk_Sunday.Checked ? "Pazar" : null
    };
            return string.Join(",", days.Where(d => d != null));
        }
        private void chc_AllChoose_CheckedChanged(object sender, EventArgs e)
        {
            checkStatus = !checkStatus;
            chk_Monday.Checked = checkStatus;
            chk_Tuesday.Checked = checkStatus;
            chk_Wednesday.Checked = checkStatus;
            chk_Thursday.Checked = checkStatus;
            chk_Friday.Checked = checkStatus;
            chk_Saturday.Checked = checkStatus;
            chk_Sunday.Checked = checkStatus;
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
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_FolderBackUp_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Lütfen yedek alınacak klasörü seçiniz.";
                folderDialog.ShowNewFolderButton = true;
                DialogResult result = folderDialog.ShowDialog(this);
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    if (Directory.Exists(folderDialog.SelectedPath))
                        txt_FolderBackUp.Text = folderDialog.SelectedPath;
                    else
                        XtraMessageBox.Show("Seçilen klasör geçerli değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void btn_WinrarFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Lütfen WinRAR klasörünü seçiniz.";
                folderDialog.ShowNewFolderButton = false;
                DialogResult result = folderDialog.ShowDialog(this);
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                {
                    if (Directory.Exists(folderDialog.SelectedPath))
                        txt_WinrarFolder.Text = folderDialog.SelectedPath;
                    else
                        XtraMessageBox.Show("Seçilen klasör geçerli değil.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}