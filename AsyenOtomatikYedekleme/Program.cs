using AsyenOtomatikYedekleme.Classes;
using AsyenOtomatikYedekleme.Forms;
using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RunAppAsync().GetAwaiter().GetResult();
        }
        private static async Task RunAppAsync()
        {
            try
            {
                string processName = Process.GetCurrentProcess().ProcessName;
                if (Process.GetProcessesByName(processName).Length > 1)
                {
                    XtraMessageBox.Show("Program zaten açık!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string serviceName = "Asyen Veritabani Yedekleme Servis";
                if (!IsServiceInstalled(serviceName))
                {
                    if (!IsRunningAsAdministrator())
                    {
                        MessageBox.Show("Yedekleme servisi kurulacaktır, programı yönetici olarak açmanız gerekiyor!",
                            "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    bool installed = await InstallServiceAsync();
                    if (!installed)
                    {
                        XtraMessageBox.Show("Servis yüklenirken hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    StartService(serviceName);
                }
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                TextLog.TextLogging("[Program Entry Error] " + ex);
                XtraMessageBox.Show("Başlangıç sırasında bir hata oluştu:\n" + ex.Message, "Uygulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static bool IsServiceInstalled(string serviceName)
        {
            return ServiceController.GetServices().Any(s => s.ServiceName == serviceName);
        }
        private static bool IsRunningAsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        private static async Task<bool> InstallServiceAsync()
        {
            try
            {
                string installUtilPath = @"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\InstallUtil.exe";
                string serviceExePath = Path.Combine(Application.StartupPath, "Servis", "AsyenVeriTabaniYedeklemeServis.exe");
                if (!File.Exists(serviceExePath))
                {
                    XtraMessageBox.Show("Servis EXE dosyası bulunamadı:\n" + serviceExePath, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = installUtilPath,
                    Arguments = $"\"{serviceExePath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                using (Process process = Process.Start(startInfo))
                {
                    string error = await process.StandardError.ReadToEndAsync();
                    process.WaitForExit();
                    if (!string.IsNullOrWhiteSpace(error))
                        TextLog.TextLogging("InstallUtil Error: " + error);
                    return process.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging("[InstallServiceAsync] " + ex);
                return false;
            }
        }
        private static void StartService(string serviceName)
        {
            try
            {
                using (ServiceController sc = new ServiceController(serviceName))
                {
                    if (sc.Status != ServiceControllerStatus.Running)
                    {
                        sc.Start();
                        sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(15));
                    }
                    else
                    {
                        XtraMessageBox.Show("Servis zaten çalışıyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging("[StartService] " + ex);
                XtraMessageBox.Show("Servis başlatılamadı:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}