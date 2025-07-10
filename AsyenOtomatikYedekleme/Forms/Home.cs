using AsyenOtomatikYedekleme.Classes;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Forms
{
    public partial class Home : XtraForm
    {
        public Home()
        {
            InitializeComponent();
        }
        private async void btn_BackUp_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dbTable = await SQLiteHelper.GetDataTableAsync("SELECT DbName FROM DbNameBackup");
                string connStr = await SQLServerHelper.ConnectionStringGetAsync();
                if (dbTable != null && !string.IsNullOrEmpty(connStr))
                {
                    BackUp backup = new BackUp();
                    await backup.BackupDatabasesToSqlServerAsync(this, dbTable, connStr);
                }
                else
                {
                    XtraMessageBox.Show("Veritabanı bilgileri alınamadı veya bağlantı hatalı.", "Yedekleme Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Yedekleme işlemi sırasında hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging("[btn_BackUp_Click] " + ex);
            }
        }
        private async void Home_Load(object sender, EventArgs e)
        {
            try
            {
                eMailToolStripMenuItem.Visible = false;
                gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
                gridView1.OptionsBehavior.Editable = false;
                DataTable dt = await SQLiteHelper.GetDataTableAsync("SELECT DbName 'Veritabani Adi' FROM DbNameBackup");
                if (dt != null && dt.Rows.Count > 0)
                    gridControl1.DataSource = dt;
                else
                {
                    await SQLServerHelper.LoadDataIntoGridViewAsync(gridControl1, "SELECT name 'Veritabani Adi' FROM sys.databases WHERE name NOT IN ('master','tempdb','model','msdb')");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Form yüklenirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging("[Home_Load] " + ex);
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            try
            {
                aboutForm.ShowDialog();
            }
            finally
            {
                aboutForm.Dispose();
            }
        }
        private void backUpSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackUpSettingsForms settingsForm = new BackUpSettingsForms();
            try
            {
                settingsForm.ShowDialog();
            }
            finally
            {
                settingsForm.Dispose();
            }
        }
        private void eMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MailSettingsForm mailForm = new MailSettingsForm();
            try
            {
                mailForm.ShowDialog();
            }
            finally
            {
                mailForm.Dispose();
            }
        }
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (gridView1.GetSelectedRows().Length > 0)
                gridView1.DeleteRow(gridView1.GetSelectedRows()[0]);
        }
        private async void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                await SQLiteHelper.ExecuteNonQueryAsync("DELETE FROM DbNameBackup");
                GridView view = gridView1;
                for (int i = 0; i < view.RowCount; i++)
                {
                    DataRow row = view.GetDataRow(view.GetRowHandle(i));
                    if (row == null) continue;
                    string dbName = row["Veritabani Adi"].ToString();
                    await SQLiteHelper.ExecuteNonQueryAsync($"INSERT INTO DbNameBackup (DbName) VALUES ('{dbName.Replace("'", "''")}')");
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Veritabanı isimleri eklenirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging("[btn_Add_Click] " + ex);
            }
        }
        private async void btn_Update_Click(object sender, EventArgs e)
        {
            await SQLServerHelper.LoadDataIntoGridViewAsync(gridControl1, "SELECT name 'Veritabani Adi' FROM sys.databases WHERE name NOT IN ('master','tempdb','model','msdb')");
        }
        private static bool IsRunningAsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        private void servisiDinleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsRunningAsAdministrator())
                {
                    MessageBox.Show("Servis dinleme görevini oluşturmak için programı yönetici olarak çalıştırmalısınız.",
                        "Yetki Gerekli", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string exePath = Path.Combine(AppContext.BaseDirectory, "ListenService", "AsyenServiceListen.exe");
                if (!File.Exists(exePath))
                {
                    MessageBox.Show($"Servis uygulaması bulunamadı:\n{exePath}", "Dosya Eksik", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string taskName = "AsyenServisKontrol";
                string schtasksArgs = $"/Create /F /SC MINUTE /MO 10 /TN \"{taskName}\" /TR \"\\\"{exePath}\\\"\" /RL HIGHEST";
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = "schtasks",
                        Arguments = schtasksArgs,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };
                    process.Start();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (!string.IsNullOrWhiteSpace(error))
                        MessageBox.Show("Görev zamanlayıcıya eklenirken hata oluştu:\n" + error, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Servis kontrol görevi başarıyla oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Servis görevini oluştururken hata:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}