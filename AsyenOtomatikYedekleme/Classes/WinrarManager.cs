using AsyenYedekleme.Classes;
using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AsyenOtomatikYedekleme.Classes
{
    internal static class WinrarManager
    {
        internal static bool CompressAndEncrypt(string filePath, string rarFilePath, string winrarPath, string encryptedPassword)
        {
            try
            {
                if (!File.Exists(filePath) && !Directory.Exists(filePath))
                {
                    XtraMessageBox.Show($"Sıkıştırılacak dosya veya klasör bulunamadı:\n{filePath}", "Hatalı Yol", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!File.Exists(winrarPath))
                {
                    XtraMessageBox.Show($"WinRAR uygulaması bulunamadı:\n{winrarPath}", "WinRAR Eksik", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        TextLog.TextLogging($"WinRAR işlem hatası: {error}");
                        XtraMessageBox.Show("Sıkıştırma işlemi sırasında hata oluştu.", "WinRAR Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Beklenmeyen hata: {ex.Message}", "WinRAR Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging($"[CompressAndEncrypt] {ex}");
                return false;
            }
            return true;
        }
    }
}