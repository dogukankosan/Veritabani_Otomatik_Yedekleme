using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace VeriTabaniYedeklemeServis.Classes
{
    internal static class TextLog
    {
        private static readonly object _lockObj = new object();

        internal static void TextLogging(string message)
        {
            TextLogging(message, "0");
        }
        internal static void TextLogging(string message, string status = "0")
        {
            try
            {
                string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string rootDirectory = Directory.GetParent(exeDirectory)?.FullName;
                string logDir = Path.Combine(rootDirectory ?? "", "Logs");
                Directory.CreateDirectory(logDir);
                string logFilePath = Path.Combine(logDir, "ServisLog.txt");
                string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | {status.ToUpperInvariant()} | {message}{Environment.NewLine}";
                lock (_lockObj)
                {
                    File.AppendAllText(logFilePath, logLine, Encoding.UTF8);
                }
                if (string.Equals(status, "Hatalı", StringComparison.OrdinalIgnoreCase))
                    _ = EMailSenderManager.MailSendService("Hatalı", message);
            }
            catch (Exception)
            {
            }
        }
    }
}