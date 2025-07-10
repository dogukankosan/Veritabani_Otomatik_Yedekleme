using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace AsyenServiceListen.Classes
{
    internal static class TextLog
    {
        private static readonly object _lockObj = new object();
        internal static void TextLogging(string message)
        {
            try
            {
                string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string rootDirectory = Directory.GetParent(exeDirectory)?.FullName;
                string logDir = Path.Combine(rootDirectory ?? "", "Logs");
                Directory.CreateDirectory(logDir);
                string logFilePath = Path.Combine(logDir, "ServisListenLog.txt");
                string logLine =
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} | {message}{Environment.NewLine}";
                lock (_lockObj)
                {
                    File.AppendAllText(logFilePath, logLine, Encoding.UTF8);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}