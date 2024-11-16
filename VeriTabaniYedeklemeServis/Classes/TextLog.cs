using System;
using System.IO;
using System.Reflection;

namespace VeriTabaniYedeklemeServis.Classes
{
    internal class TextLog
    {
        internal static void TextLogging(string message,string status)
        {
            try
            {
                string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string logFilePath = Path.Combine(appDirectory, "log.txt");
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
                File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}\n");
                if (status=="Hatalı")
                    _ = EMailSenderManager.MailSendService("Hatalı");
            }
            catch (Exception)
            {

            }
        }
    }
}