using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace VeriTabaniYedeklemeServis.Classes
{
    internal class BackUp
    {
        internal static async Task BackupDatabasesToSqlServerServiceAsync(DataTable databaseNames, string sqlServerConnectionString)
        {
            DataTable dt = SQLLiteConnection.GetDataFromSQLite("SELECT BackUpFolder,WinrarFolder,WinrarPassword,BackUpDelete FROM DbBackUpSettings LIMIT 1");
            if (dt.Rows.Count == 0)
            {
                TextLog.TextLogging("Yedek Alma İşlemi Hatalı Lütfen Önce Yedek Alma Ayarlarınızı Giriniz", "Hatalı");
                return;
            }
            try
            {
                if (!Directory.Exists(dt.Rows[0][0].ToString()))
                    Directory.CreateDirectory(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                TextLog.TextLogging(ex.Message, "Hatalı");
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
                    TextLog.TextLogging(ex.Message, "Hatalı");
                    return;
                }
                for (byte i = 0; i < databaseNames.Rows.Count; i++)
                {
                    string backupFilePath = Path.Combine(dt.Rows[0][0].ToString(), $"{databaseNames.Rows[i][0]}.bak");
                    string backupQuery = $"BACKUP DATABASE [{databaseNames.Rows[i][0]}] TO DISK = '{backupFilePath}' WITH INIT";
                    using (SqlCommand command = new SqlCommand(backupQuery, connection))
                    {
                        try
                        {
                            command.CommandTimeout = 0;
                            await command.ExecuteNonQueryAsync();
                            string rarFilePath = Path.Combine(dt.Rows[0][0].ToString(), $"{databaseNames.Rows[i][0]}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.rar");
                            await Task.Run(() => WinrarManager.CompressAndEncryptService(backupFilePath, rarFilePath, dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString()));
                        }
                        catch (Exception ex)
                        {
                            TextLog.TextLogging(ex.Message, "Hatalı");
                            return;
                        }
                    }
                    DeleteBak(dt.Rows[0][0].ToString());
                }
            }
            if ("" == DeleteBakFiles(dt.Rows[0][0].ToString(), dt.Rows[0][3].ToString()))
                _ = EMailSenderManager.MailSendService("Başarılı");
        }
        private static string DeleteBak(string directoryPath)
        {
            try
            {
                foreach (string filePath in Directory.GetFiles(directoryPath, "*.bak"))
                    File.Delete(filePath);
            }
            catch (Exception ex)
            {
                TextLog.TextLogging(ex.Message, "Hatalı");
                return "Hatalı";
            }
            return "";
        }
        private static string DeleteBakFiles(string directoryPath, string status)
        {
            try
            {
                foreach (string filePath in Directory.GetFiles(directoryPath, "*.bak"))
                    File.Delete(filePath);
                if (status == "1")
                {
                    foreach (string rarFilePath in Directory.GetFiles(directoryPath, "*.rar"))
                    {
                        FileInfo fileInfo = new FileInfo(rarFilePath);
                        if (fileInfo.CreationTime.Date < DateTime.Now.Date)
                            File.Delete(rarFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging(ex.Message, "Hatalı");
                return "Hatalı";
            }
            return "";
        }
    }
}