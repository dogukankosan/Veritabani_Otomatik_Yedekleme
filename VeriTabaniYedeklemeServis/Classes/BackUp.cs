using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VeriTabaniYedeklemeServis.Classes
{
    internal class BackUp
    {
        internal static async Task BackupDatabasesToSqlServerServiceAsync(DataTable databaseNames, string sqlServerConnectionString)
        {
            List<string> successfulDbs = new List<string>();
            List<string> failedDbs = new List<string>();
            DataTable settings;
            try
            {
                settings = await SQLiteHelper.GetDataTableAsync("SELECT BackUpFolder, WinrarFolder, WinrarPassword, BackUpDelete, CompanyName FROM DbBackUpSettings LIMIT 1");
                if (settings == null || settings.Rows.Count == 0 || string.IsNullOrEmpty(settings.Rows[0]["BackUpFolder"]?.ToString()))
                {
                    TextLog.TextLogging("Yedek alma ayarları bulunamadı. Lütfen ayarları giriniz.");
                    return;
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[Ayar Okuma Hatası] {ex}", "Hatalı");
                return;
            }
            string backupFolder = settings.Rows[0]["BackUpFolder"].ToString();
            string winrarPath = settings.Rows[0]["WinrarFolder"].ToString();
            string rarPassword = settings.Rows[0]["WinrarPassword"].ToString();
            bool deleteOldRars = settings.Rows[0]["BackUpDelete"].ToString() == "1";
            try
            {
                if (!Directory.Exists(backupFolder))
                    Directory.CreateDirectory(backupFolder);
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[Klasör Oluşturma Hatası] {ex}", "Hatalı");
                return;
            }
            using (SqlConnection connection = new SqlConnection(sqlServerConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                }
                catch (Exception ex)
                {
                    TextLog.TextLogging($"[SQL Bağlantı Hatası] {ex}", "Hatalı");
                    return;
                }
                foreach (DataRow dbRow in databaseNames.Rows)
                {
                    string dbName = dbRow[0]?.ToString();
                    if (string.IsNullOrWhiteSpace(dbName))
                        continue;
                    string bakFile = Path.Combine(backupFolder, $"{dbName}.bak");
                    string rarFile = Path.Combine(backupFolder, $"{dbName}_{DateTime.Now:yyyyMMdd_HHmmss}.rar");
                    string backupQuery = $"BACKUP DATABASE [{dbName}] TO DISK = '{bakFile}' WITH INIT";
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(backupQuery, connection))
                        {
                            cmd.CommandTimeout = 0;
                            await cmd.ExecuteNonQueryAsync();
                        }
                        bool winrarStatus = await Task.Run(() =>
                            WinrarManager.CompressAndEncryptService(bakFile, rarFile, winrarPath, rarPassword)
                        );
                        if (winrarStatus)
                        {
                            successfulDbs.Add(dbName);
                            DeleteBakFiles(backupFolder);
                        }
                        else
                        {
                            failedDbs.Add(dbName);
                        }
                    }
                    catch (Exception ex)
                    {
                        TextLog.TextLogging(ex.Message , "Hatalı");
                        failedDbs.Add(dbName);
                        continue;
                    }
                }
            }
            if (deleteOldRars)
                DeleteOldRarFiles(backupFolder);
            if (successfulDbs.Count > 0)
            {
                string successMessage = $@"
<p style='font-family:Segoe UI, sans-serif; font-size:14px; color:#333;'>
  <strong>Aşağıdaki veritabanları başarıyla yedeklenmiştir:</strong>
</p>
<ul style='font-size:14px; color:#2E8B57;'>
  {string.Join("", successfulDbs.Select(db => $"<li>✔ {db}</li>"))}
</ul>
<hr style='margin-top:20px; margin-bottom:10px;'/>";
                await EMailSenderManager.MailSendService("Başarılı", successMessage);
            }
            if (failedDbs.Count > 0)
            {
                string failMessage = $@"
<p style='font-family:Segoe UI, sans-serif; font-size:14px; color:#333;'>
  <strong>Aşağıdaki veritabanları yedeklenememiştir:</strong>
</p>
<ul style='font-size:14px; color:#B22222;'>
  {string.Join("", failedDbs.Select(db => $"<li>❌ {db}</li>"))}
</ul>
<hr style='margin-top:20px; margin-bottom:10px;'/>
<p style='font-size:13px; color:#666;'>
  Lütfen sistem yöneticiniz ile iletişime geçiniz veya tekrar deneyiniz.
</p>";
               TextLog.TextLogging(failMessage,"Hatalı");
            }
        }
        private static void DeleteBakFiles(string directoryPath)
        {
            try
            {
                foreach (string bakFile in Directory.GetFiles(directoryPath, "*.bak"))
                {
                    try { File.Delete(bakFile); } catch (Exception ex) { TextLog.TextLogging($"[BAK Silme Hatası] {ex}", "Hatalı"); }
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[DeleteBakFiles] {ex}", "Hatalı");
            }
        }
        private static void DeleteOldRarFiles(string directoryPath)
        {
            try
            {
                foreach (string rarFile in Directory.GetFiles(directoryPath, "*.rar"))
                {
                    FileInfo info = new FileInfo(rarFile);
                    if (info.LastWriteTime.Date < DateTime.Now.Date)
                    {
                        try { File.Delete(rarFile); } catch (Exception ex) { TextLog.TextLogging($"[RAR Silme Hatası] {ex}", "Hatalı"); }
                    }
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[DeleteOldRarFiles] {ex}", "Hatalı");
            }
        }
    }
}