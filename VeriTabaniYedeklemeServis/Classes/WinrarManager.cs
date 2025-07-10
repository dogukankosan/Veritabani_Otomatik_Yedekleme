using AsyenVeriTabaniYedeklemeServis.Classes;
using System;
using System.Diagnostics;
using System.IO;

namespace VeriTabaniYedeklemeServis.Classes
{
    internal class WinrarManager
    {
        internal static bool CompressAndEncryptService(string filePath, string rarFilePath, string winrarPath, string encryptedPassword)
        {
            try
            {
                if (!File.Exists(filePath) && !Directory.Exists(filePath))
                {
                    TextLog.TextLogging($"Sıkıştırılacak dosya veya klasör bulunamadı: {filePath}", "Hatalı");
                    return false;
                }
                if (!File.Exists(winrarPath))
                {
                    TextLog.TextLogging($"WinRAR uygulaması bulunamadı: {winrarPath}", "Hatalı");
                    return false;
                }
                string password = EncryptionHelper.Decrypt(encryptedPassword);
                string arguments = string.IsNullOrWhiteSpace(password)
                    ? $"a -m2 \"{rarFilePath}\" \"{filePath}\""
                    : $"a -m2 -hp{password} \"{rarFilePath}\" \"{filePath}\"";
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = winrarPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                using (Process process = Process.Start(processStartInfo))
                {
                    process.WaitForExit();
                    if (process.ExitCode != 0)
                    {
                        string error = process.StandardError.ReadToEnd();
                        TextLog.TextLogging($"WinRAR işlem hatası: {error}", "Hatalı");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[CompressAndEncryptService] {ex}", "Hatalı");
                return false;
            }
            return true;
        }
    }
}