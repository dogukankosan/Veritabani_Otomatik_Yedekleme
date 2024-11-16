using AsyenVeriTabaniYedeklemeServis.Classes;
using System;
using System.Diagnostics;

namespace VeriTabaniYedeklemeServis.Classes
{
    internal class WinrarManager
    {
        internal static void CompressAndEncryptService(string filePath, string rarFilePath, string winrarPath, string password)
        {
            string arguments = "";
            string password1 = EncryptionHelper.Decrypt(password);
            if (string.IsNullOrEmpty(password1))
                 arguments = $"a -m2 \"{rarFilePath}\" \"{filePath}\"";
            else
                arguments = $"a -m2 -hp{password1} \"{rarFilePath}\" \"{filePath}\"";
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = winrarPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (Process process = Process.Start(processStartInfo))
                {
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging(password+ex.Message+" Winrardan Hata","Hatalı");
                return;
            }
        }
    }
}