using AsyenOtomatikYedekleme.Forms;
using DevExpress.XtraEditors;
using System;
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
            DataTable dt = SQLLiteConnection.GetDataFromSQLite("SELECT BackUpFolder,WinrarFolder,WinrarPassword,BackUpDelete FROM DbBackUpSettings LIMIT 1");
            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show("Yedek Alma İşlemi Hatalı Lütfen Önce Yedek Alma Ayarlarınızı Giriniz", "Hatalı Yedek Alma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BackUpSettingsForms frm = new BackUpSettingsForms();
                frm.ShowDialog();
                return;
            }
            DataTable dt2 = SQLLiteConnection.GetDataFromSQLite("SELECT DbName FROM DbNameBackup");
            if (dt2.Rows.Count == 0)
            {
                XtraMessageBox.Show("Yedek Alma İşlemi Hatalı Lütfen Önce Yedek Alınacak Veritabanlarını Kaydedin", "Hatalı Yedek Alma", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            main.Enabled = false;
            try
            {
                if (!Directory.Exists(dt.Rows[0][0].ToString()))
                    Directory.CreateDirectory(dt.Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hatalı Klasör Oluşturma İşlemi");
                TextLog.TextLogging(ex.Message);
                main.Enabled = true;
                return;
            }
            SplashScreenLoading splashScreen = new SplashScreenLoading();
            splashScreen.Show();
            using (SqlConnection connection = new SqlConnection(sqlServerConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Yedek Alınacak Veri Tabanı Sunucusuna Bağlantı Yapılamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextLog.TextLogging(ex.Message);
                    main.Enabled = true;
                    splashScreen.Close();
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
                            splashScreen.status = string.Concat("Veritabanı Adı: ",databaseNames.Rows[i][0].ToString()," şuanda yedekleniyor...");
                            await command.ExecuteNonQueryAsync();
                            string rarFilePath = Path.Combine(dt.Rows[0][0].ToString(), $"{databaseNames.Rows[i][0]}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.rar");
                            splashScreen.status = string.Concat("Veritabanı Adı: ", databaseNames.Rows[i][0].ToString(), " şuanda winrarlanıyor...");
                            await Task.Run(() => WinrarManager.CompressAndEncrypt(backupFilePath, rarFilePath, dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString()));
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show($"Yedek Alma İşlemi Hatalı '{databaseNames.Rows[i][0]}': {ex.Message}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TextLog.TextLogging(ex.Message);
                            main.Enabled = true;
                            splashScreen.Close();
                            return;
                        }
                    }
                    DeleteBak(dt.Rows[0][0].ToString());
                }
            }
            DeleteBakFiles(dt.Rows[0][0].ToString(), dt.Rows[0][3].ToString());
            XtraMessageBox.Show("Yedek Alma İşlemi Başarılı", "Başarılı Yedek Alma", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _ = EMailSenderManager.MailSend();
            main.Enabled = true;
            splashScreen.Close();
        }
        private static void DeleteBak(string directoryPath)
        {
            foreach (string filePath in Directory.GetFiles(directoryPath, "*.bak"))
                File.Delete(filePath);
        }
        private static void DeleteBakFiles(string directoryPath, string status)
        {
            foreach (string filePath in Directory.GetFiles(directoryPath, "*.bak"))
                File.Delete(filePath);
            if (status == "1")
            {
                foreach (string rarFilePath in Directory.GetFiles(directoryPath, "*.rar"))
                {
                    FileInfo fileInfo = new FileInfo(rarFilePath);
                    if (fileInfo.CreationTime.Date < DateTime.Now.Date)
                    {
                        File.Delete(rarFilePath);
                    }
                }
            }
        }
    }
}