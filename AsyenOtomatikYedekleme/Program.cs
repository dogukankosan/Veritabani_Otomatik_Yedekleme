using AsyenOtomatikYedekleme.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Security.Principal;
using System.Linq;
using System.Xml.Linq;
using DevExpress.XtraEditors;
using AsyenOtomatikYedekleme.Classes;

namespace AsyenOtomatikYedekleme
{
    static class Program
    {
        [STAThread]
        static void Main()
        {// Programın adıyla çalışan bir örneğin olup olmadığını kontrol ediyoruz
            string processName = Process.GetCurrentProcess().ProcessName;
            var runningProcesses = Process.GetProcessesByName(processName);
            if (runningProcesses.Length > 1)
            {
                XtraMessageBox.Show("Program zaten açık!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Servisin kurulu olup olmadığını kontrol et
            if (!IsServiceInstalled("Asyen Veritabani Yedekleme Servis"))
            {
                // Yönetici olarak çalıştırılıp çalıştırılmadığını kontrol et
                if (!IsRunningAsAdministrator())
                {
                    MessageBox.Show("Yedekleme servisi kurulacaktır programı yönetici olarak açmanız gerekiyor!", "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    InstallService();
                    ServiceStart();
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
        static bool IsServiceInstalled(string serviceName)
        {
            return ServiceController.GetServices().Any(s => s.ServiceName == serviceName);
        }
        // Programın yönetici olarak çalışıp çalışmadığını kontrol eden fonksiyon
        static bool IsRunningAsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        // Servisi kuran fonksiyon (örnek amaçlı; gerçek bir kurulum işlemi daha detaylı olacaktır)
        static void InstallService()
        {
            // .net bu yolda kesin kurulu olması gerekiyor.
            string installUtilPath = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe";
            // servis klasörü burda olmalı
            string serviceExePath = Application.StartupPath + "\\Servis";
            if (string.IsNullOrWhiteSpace(serviceExePath))
            {
                XtraMessageBox.Show("Geçerli Bir Servis Dizini Girmelisiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = installUtilPath,
                Arguments = $"\"{serviceExePath + "\\AsyenVeriTabaniYedeklemeServis.exe"}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (process.ExitCode == 0 && string.IsNullOrEmpty(error))
                {
                    var configFile = serviceExePath + "\\AsyenVeriTabaniYedeklemeServis.exe.config";
                    XDocument configXml = XDocument.Load(configFile);
                    var connectionStrings = configXml.Root.Element("connectionStrings");
                    if (connectionStrings != null)
                    {
                        var connectionStringElement = connectionStrings.Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "SQLiteConnectionString");
                        if (connectionStringElement != null)
                        {
                            connectionStringElement.Attribute("connectionString").Value = SQLLiteConnection.connectionString;
                            configXml.Save(configFile);
                        }
                        else
                        {
                            XtraMessageBox.Show("Connection String Bulunamadı.", "SQLITE Connection String Kaydedilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TextLog.TextLogging("Servis Confige ConnectionString Kaydedilemedi");
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("ConnectionStrings Bölümü Bulunamadı.", "SQLITE Connection String Kaydedilemedi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TextLog.TextLogging("Servis Confige ConnectionString Kaydedilemedi Yada Bulamadı");
                    }
                }
                else
                    XtraMessageBox.Show($"Servis kaydedilemedi.\nHata: {error}\nÇıktı: {output}", "Servis Kaydı Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        static void ServiceStart()
        {
            string serviceName = "Asyen Veritabani Yedekleme Servis";
            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c sc query \"{serviceName}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();
                if (!output.Contains("RUNNING"))
                {
                    process.StartInfo.Arguments = $"/c net start \"{serviceName}\"";
                    process.Start();
                    output = process.StandardOutput.ReadToEnd();
                    error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (string.IsNullOrEmpty(error))
                        return;
                    else
                        XtraMessageBox.Show($"Servis Başlatılamadı.\nHata: {error}", "Servis Başlatma Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    XtraMessageBox.Show("Servis Zaten Çalışıyor.", "Servis Durumu Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}