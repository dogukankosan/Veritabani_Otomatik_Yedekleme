using DevExpress.XtraEditors;

namespace AsyenOtomatikYedekleme.Bussiness
{
    internal class EMailManager
    {
        internal static bool EmailControl(TextEdit senderEmail, TextEdit recipientEmail, TextEdit senderPassword, TextEdit server, TextEdit port)
        {   /* KULLANABİLİRİZDE EMAİL KONTROL TARAFI KALSIN
            #region SenderMailControl
            if (string.IsNullOrEmpty(senderEmail.Text))
            {
                XtraMessageBox.Show("E-Mail Kutusu Boş Geçilemez", "Hatalı E-Mail Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                senderEmail.Focus();
                return false;
            }
            else if (senderEmail.Text.Length < 5)
            {
                XtraMessageBox.Show("E-Mail Kutusu 5 Haneden Daha Az Olamaz", "Hatalı E-Mail Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                senderEmail.Focus();
                return false;
            }
            else if (senderEmail.Text.Length > 50)
            {
                XtraMessageBox.Show("E-Mail Kutusu 50 Haneden Fazla Olamaz", "Hatalı E-Mail Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                senderEmail.Focus();
                return false;
            }
            else if (!senderEmail.Text.Contains("@"))
            {
                XtraMessageBox.Show("E-Mail Kutusunun İçinde @ İşareti Bulunmalıdır", "Hatalı E-Mail Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                senderEmail.Focus();
                return false;
            }
            else if (!senderEmail.Text.Contains("."))
            {
                XtraMessageBox.Show("E-Mail Kutusunun İçinde . İşareti Bulunmalıdır", "Hatalı E-Mail Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                senderEmail.Focus();
                return false;
            }
            #endregion
            #region RecipientEmailControl
            if (string.IsNullOrEmpty(recipientEmail.Text))
            {
                XtraMessageBox.Show("E-Mail Kutusu Boş Geçilemez", "Hatalı E-Mail Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                recipientEmail.Focus();
                return false;
            }
            else if (recipientEmail.Text.Length < 5)
            {
                XtraMessageBox.Show("E-Mail Kutusu 5 Haneden Daha Az Olamaz", "Hatalı E-Mail Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                recipientEmail.Focus();
                return false;
            }
            else if (recipientEmail.Text.Length > 50)
            {
                XtraMessageBox.Show("E-Mail Kutusu 50 Haneden Fazla Olamaz", "Hatalı E-Mail Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                recipientEmail.Focus();
                return false;
            }
            else if (!recipientEmail.Text.Contains("@"))
            {
                XtraMessageBox.Show("E-Mail Kutusunun İçinde @ İşareti Bulunmalıdır", "Hatalı E-Mail Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                recipientEmail.Focus();
                return false;
            }
            else if (!recipientEmail.Text.Contains("."))
            {
                XtraMessageBox.Show("E-Mail Kutusunun İçinde . İşareti Bulunmalıdır", "Hatalı E-Mail Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                recipientEmail.Focus();
                return false;
            }
            #endregion
            #region PasswordControl
            if (string.IsNullOrEmpty(senderPassword.Text))
            {
                XtraMessageBox.Show("Şifre Kutusu Boş Geçilemez", "Hatalı Şifre Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                senderPassword.Focus();
                return false;
            }
            else if (senderPassword.Text.Length < 3)
            {
                XtraMessageBox.Show("Şifre Kutusu 3 Karakterden Daha Az Olamaz", "Hatalı Şifre Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                senderPassword.Focus();
                return false;
            }
            else if (senderPassword.Text.Length > 50)
            {
                XtraMessageBox.Show("Şifre Kutusu 50 Karakterden Fazla Olamaz", "Hatalı Şifre Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                senderPassword.Focus();
                return false;
            }
            else if (senderPassword.Text.Length > 50)
            {
                XtraMessageBox.Show("Şifre Kutusu 50 Karakterden Fazla Olamaz", "Hatalı Şifre Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                senderPassword.Focus();
                return false;
            }
            #endregion
            #region ServerControl
            if (string.IsNullOrEmpty(server.Text))
            {
                XtraMessageBox.Show("Server Kutusu Boş Geçilemez", "Hatalı Server Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.Focus();
                return false;
            }
            else if (server.Text.Length < 5)
            {
                XtraMessageBox.Show("Server Kutusu 5 Haneden Daha Az Olamaz", "Hatalı Server Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.Focus();
                return false;
            }
            else if (server.Text.Length > 30)
            {
                XtraMessageBox.Show("Server Kutusu 30 Haneden Fazla Olamaz", "Hatalı Server Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.Focus();
                return false;
            }
            else if (!server.Text.Contains("."))
            {
                XtraMessageBox.Show("Server Kutusu İçinde . İşareti İçermelidir", "Hatalı Server Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                server.Focus();
                return false;
            }
            #endregion
            #region PortControl
            if (string.IsNullOrEmpty(port.Text))
            {
                XtraMessageBox.Show("Port Kutusu Boş Geçilemez", "Hatalı Port Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                port.Focus();
                return false;
            }
            else if (port.Text.Length > 4)
            {
                XtraMessageBox.Show("Port Kutusu 4 Haneden Fazla Olamaz", "Hatalı Port Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                port.Focus();
                return false;
            }
            else if (port.Text.Length > 4)
            {
                XtraMessageBox.Show("Port Kutusu 4 Haneden Fazla Olamaz", "Hatalı Port Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                port.Focus();
                return false;
            }
            else if (!(int.TryParse(port.Text, out _)))
            {
                XtraMessageBox.Show("Port Kutusu Sadece Rakamlardan Oluşmalıdır", "Hatalı Port Girişi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                port.Focus();
                return false;
            }
            else
            {
                return true;
            }
            #endregion*/
            return true;
        }
    }
}