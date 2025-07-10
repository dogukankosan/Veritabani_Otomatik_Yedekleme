using System;
using System.Data;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading.Tasks;
using AsyenServiceListen.Classes;

namespace AsyenServiceListen
{
    internal class Program
    {
        internal class NativeMethods
        {
            [DllImport("kernel32.dll")]
            public static extern IntPtr GetConsoleWindow();
            [DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
            public const int SW_HIDE = 0;
        }
        static void Main()
        {
            NativeMethods.ShowWindow(NativeMethods.GetConsoleWindow(), NativeMethods.SW_HIDE);
            RunAsync().GetAwaiter().GetResult();
        }
        private static async Task RunAsync()
        {
            string serviceName = "Asyen Veritabani Yedekleme Servis";
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                if (sc.Status != ServiceControllerStatus.Running)
                {
                    await TrySendMailAsync();
                }
            }
            catch (Exception ex)
            {
                   TextLog.TextLogging(ex.Message);
            }
        }
        private static async Task TrySendMailAsync()
        {
            DataTable dt = await SQLiteHelper.GetDataTableAsync("SELECT * FROM EMailSetting LIMIT 1");
            DataTable company = await SQLiteHelper.GetDataTableAsync("SELECT CompanyName FROM DbBackUpSettings LIMIT 1");
            if (dt?.Rows.Count != 1 || company?.Rows.Count != 1)
            {
                TextLog.TextLogging("Mail gönderimi için gerekli ayarlar bulunamadı.");
                return;
            }
            DataRow row = dt.Rows[0];
            string email = row["EMail"]?.ToString();
            string password = EncryptionHelper.Decrypt(row["Password"]?.ToString());
            string smtp = row["Server"]?.ToString();
            string portStr = row["Port"]?.ToString();
            bool ssl = row["SSL"]?.ToString() == "1";
            string companyName = company.Rows[0]["CompanyName"]?.ToString();
            if (!int.TryParse(portStr, out int port) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(smtp))
            {
                TextLog.TextLogging("Mail ayarlarında eksik veya hatalı bilgi var.");
                return;
            }
            await EMailSenderManager.MailSendForm(companyName, email, email, password, smtp, port, ssl);
        }
    }
}