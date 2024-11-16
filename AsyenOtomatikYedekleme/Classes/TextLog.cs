using System;
using System.IO;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Classes
{
    internal static class TextLog
    {
        internal static void TextLogging(string message)
        {
            try
            {
                string logFilePath = $"{Application.StartupPath}\\log.txt";
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}\n");
            }
            catch (Exception)
            {

            }
        }
    }
}