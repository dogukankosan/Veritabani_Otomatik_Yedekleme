using DevExpress.XtraEditors;
using System;
using System.IO;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Bussiness
{
    internal class BackUpSettingsManager
    {
        public static bool BackUpSettingControl(TextEdit folderBackup, TextEdit winrarFolder)
        {
            #region FolderBackUpControl
            if (string.IsNullOrEmpty(folderBackup.Text))
            {
                XtraMessageBox.Show("Yedek Alma Klasörü Boş Olamaz", "Hatalı Yedekleme Klasörü Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                folderBackup.Focus();
                return false;
            }
            else if (folderBackup.Text.Length < 5)
            {
                XtraMessageBox.Show("Yedek Alma Klasörü 5 Haneden Daha Az Olamaz", "Hatalı Yedekleme Klasörü Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                folderBackup.Focus();
                return false;
            }
            else if (folderBackup.Text.Length > 50)
            {
                XtraMessageBox.Show("Yedek Alma Klasörü 50 Haneden Fazla Olamaz", "Hatalı Yedekleme Klasörü Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                folderBackup.Focus();
                return false;
            }
            else if (!folderBackup.Text.Contains("\\"))
            {
                XtraMessageBox.Show("Yedek Alma Klasöründe \\ Karakteri Bulunmalıdır", "Hatalı Yedekleme Klasörü Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                folderBackup.Focus();
                return false;
            }
            else if (!folderBackup.Text.Contains(":"))
            {
                XtraMessageBox.Show("Yedek Alma Klasöründe : Karakteri Bulunmalıdır", "Hatalı Yedekleme Klasörü Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                folderBackup.Focus();
                return false;
            }
            else if (!folderBackup.Text.StartsWith("C:\\") && !folderBackup.Text.StartsWith("D:\\") && !folderBackup.Text.StartsWith("E:\\") && !folderBackup.Text.StartsWith("F:\\") && !folderBackup.Text.StartsWith("G:\\"))
            {
                XtraMessageBox.Show("Yedek Alma Klasöründe E:\\ D:\\ E:\\ F:\\ G:\\ Karakteri İle Başlamalıdır", "Hatalı Yedekleme Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                folderBackup.Focus();
                return false;
            }
            else
            {
                try
                {
                    if (!Directory.Exists(folderBackup.Text))
                        Directory.CreateDirectory(folderBackup.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Klasör Oluşturalamadı Formatı Kontrol Edin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    folderBackup.Focus();
                    return false;
                }
            }
            #endregion
            #region WinrarFolderControl
            if (string.IsNullOrEmpty(winrarFolder.Text))
            {
                XtraMessageBox.Show("Winrar Dosyası Klasörü Boş Olamaz", "Hatalı Winrar Dosya Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                winrarFolder.Focus();
                return false;
            }
            else if (winrarFolder.Text.Length < 5)
            {
                XtraMessageBox.Show("Winrar Alma Klasörü 5 Haneden Daha Az Olamaz", "Hatalı Winrar Dosya Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                winrarFolder.Focus();
                return false;
            }
            else if (winrarFolder.Text.Length > 100)
            {
                XtraMessageBox.Show("Winrar Alma Klasörü 100 Haneden Fazla Olamaz", "Hatalı Winrar Dosya Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                winrarFolder.Focus();
                return false;
            }
            else if (!winrarFolder.Text.Contains("\\"))
            {
                XtraMessageBox.Show("Winrar Dosyası \\ Karakteri Bulunmalıdır", "Hatalı Winrar Dosya Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                winrarFolder.Focus();
                return false;
            }
            else if (!winrarFolder.Text.Contains(":"))
            {
                XtraMessageBox.Show("Winrar Dosyasında : Karakteri Bulunmalıdır", "Hatalı Winrar Dosya Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                winrarFolder.Focus();
                return false;
            }
            else if (!winrarFolder.Text.StartsWith("C:\\") && !winrarFolder.Text.StartsWith("D:\\") && !winrarFolder.Text.StartsWith("E:\\") && !winrarFolder.Text.StartsWith("F:\\") && !winrarFolder.Text.StartsWith("G:\\"))
            {
                XtraMessageBox.Show("Winrar Dosyası Klasöründe E:\\ D:\\ E:\\ F:\\ G:\\ Karakteri İle Başlamalıdır", "Hatalı Winrar Dosya Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                winrarFolder.Focus();
                return false;
            }
            #endregion
            else
            {
                return true;
            }
        }
    }
}