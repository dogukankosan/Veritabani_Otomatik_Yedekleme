using AsyenOtomatikYedekleme.Forms;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Classes
{
    public class BackUp
    {
        public async Task BackupDatabasesToSqlServerAsync(Form main, DataTable databaseNames, string sqlServerConnectionString)
        {
            List<string> successfulDbs = new List<string>();
            List<string> failedDbs = new List<string>();
            List<string> cancelledDbs = new List<string>();
            DataTable settings;
            try
            {
                settings = await SQLiteHelper.GetDataTableAsync("SELECT BackUpFolder,WinrarFolder,WinrarPassword,BackUpDelete,CompanyName FROM DbBackUpSettings LIMIT 1");
                if (settings.Rows.Count == 0)
                {
                    ShowSettingsMissingError();
                    return;
                }
                DataTable dbList = await SQLiteHelper.GetDataTableAsync("SELECT DbName FROM DbNameBackup");
                if (dbList.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Yedeklenecek veritabanı listesi boş.", "Hatalı Yedek Alma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Yedekleme ayarları okunamadı: " + ex.Message);
                TextLog.TextLogging("[Ayar Okuma Hatası] " + ex);
                return;
            }
            main.Enabled = false;
            string backupPath = settings.Rows[0]["BackUpFolder"].ToString();
            string rarPath = settings.Rows[0]["WinrarFolder"].ToString();
            string rarPassword = settings.Rows[0]["WinrarPassword"].ToString();
            bool shouldDeleteOldRars = settings.Rows[0]["BackUpDelete"].ToString() == "1";
            if (!EnsureBackupFolderExists(backupPath, main)) return;
            SplashScreenLoading splashScreen = new SplashScreenLoading();
            splashScreen.Show();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(sqlServerConnectionString);
                await connection.OpenAsync();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Sunucuya bağlanılamadı:\n" + ex.Message, "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging("[SQL Bağlantı Hatası] " + ex);
                splashScreen?.Close();
                main.Enabled = true;
                return;
            }
            for (int i = 0; i < databaseNames.Rows.Count; i++)
            {
                if (splashScreen.IScancel)
                {
                    for (int j = i; j < databaseNames.Rows.Count; j++)
                    {
                        string dbNameLeft = databaseNames.Rows[j][0]?.ToString();
                        if (!string.IsNullOrWhiteSpace(dbNameLeft))
                            cancelledDbs.Add(dbNameLeft);
                    }
                    splashScreen.Close();
                    main.Enabled = true;
                    XtraMessageBox.Show("Yedekleme işlemi iptal edildi.", "İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                }
                string dbName = databaseNames.Rows[i][0]?.ToString();
                string bakFile = Path.Combine(backupPath, $"{dbName}.bak");
                string rarFile = Path.Combine(backupPath, $"{dbName}_{DateTime.Now:yyyyMMdd_HHmmss}.rar");
                splashScreen.Invoke(new Action(() =>
                {
                    splashScreen.status = $"{dbName} yedekleniyor...";
                }));
                if (!await BackupDatabaseAsync(connection, dbName, bakFile))
                {
                    failedDbs.Add(dbName);
                    continue;
                }
                splashScreen.Invoke(new Action(() =>
                {
                    splashScreen.status = $"{dbName} WinRAR ile sıkıştırılıyor...";
                }));
                bool statusWinrar = await Task.Run(() => WinrarManager.CompressAndEncrypt(bakFile, rarFile, rarPath, rarPassword));
                if (statusWinrar)
                    successfulDbs.Add(dbName);
                else
                    failedDbs.Add(dbName);
                DeleteBakFiles(backupPath);
            }
            DeleteOldRarFiles(backupPath, shouldDeleteOldRars);
            splashScreen.Close();
            main.Enabled = true;
            string resultMessage = $"✅ Başarılı Yedeklenen DB'ler:\n{string.Join("\n", successfulDbs)}";
            if (failedDbs.Count > 0)
                resultMessage += $"\n\n❌ Yedeklenemeyen DB'ler:\n{string.Join("\n", failedDbs)}";
            if (cancelledDbs.Count > 0)
                resultMessage += $"\n\n⛔ İptalden Sonra Atlanan DB'ler:\n{string.Join("\n", cancelledDbs)}";
            XtraMessageBox.Show(resultMessage, "Yedekleme Sonucu", MessageBoxButtons.OK,
                (failedDbs.Count > 0 || cancelledDbs.Count > 0) ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
        }
        private void ShowSettingsMissingError()
        {
            XtraMessageBox.Show("Yedek Alma Ayarları Eksik. Lütfen ayarları tamamlayın.", "Hatalı Yedek Alma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            new BackUpSettingsForms().ShowDialog();
        }
        private bool EnsureBackupFolderExists(string path, Form main)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Yedek klasörü oluşturulamadı: " + ex.Message);
                TextLog.TextLogging("[Klasör Oluşturma Hatası] " + ex);
                main.Enabled = true;
                return false;
            }
        }
        private async Task<bool> BackupDatabaseAsync(SqlConnection conn, string dbName, string filePath)
        {
            string query = $"BACKUP DATABASE [{dbName}] TO DISK = '{filePath}' WITH INIT";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.CommandTimeout = 0;
                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"{dbName} için yedekleme başarısız: {ex.Message}");
                    TextLog.TextLogging($"[Backup Error - {dbName}] {ex}");
                    return false;
                }
            }
        }
        private void DeleteBakFiles(string path)
        {
            foreach (string bak in Directory.GetFiles(path, "*.bak"))
            {
                try { File.Delete(bak); } catch (Exception ex) { TextLog.TextLogging("[BAK Delete] " + ex); }
            }
        }
        private void DeleteOldRarFiles(string path, bool delete)
        {
            if (!delete) return;
             try
            {
                foreach (string rarFile in Directory.GetFiles(path, "*.rar"))
                {
                    FileInfo info = new FileInfo(rarFile);
                    if (info.LastWriteTime.Date < DateTime.Now.Date)
                    {
                        try { File.Delete(rarFile); } catch (Exception ex) { TextLog.TextLogging($"[RAR Silme Hatası] {ex}"); }
                    }
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[DeleteOldRarFiles] {ex}");
            }
        }
    }
}