using System;
using System.IO;
using System.Text;

namespace AsyenOtomatikYedekleme.Classes
{
    internal static class TextLog
    {
        private static readonly object _lockObj = new object();
        internal static void TextLogging(string message)
        {
            try
            {
                string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                string logFilePath = Path.Combine(logDir, "UILog.txt");
                Directory.CreateDirectory(logDir);
                string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | {message}{Environment.NewLine}";
                lock (_lockObj)
                    File.AppendAllText(logFilePath, logLine, Encoding.UTF8);
            }
            catch
            {

            }
        }
    }
}