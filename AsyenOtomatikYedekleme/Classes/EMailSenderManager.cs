using DevExpress.XtraEditors;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Classes
{
    internal static class EMailSenderManager
    {
        internal static async Task MailSendForm(string senderEmail, string recipientEmail, string senderPassword, string server, int port, bool ssl)
        {
            const string subject = "Asyen Yedekleme Programından Test E-posta Başlığı";
            const string body = "Bu bir test e-posta içeriğidir.";
            try
            {
                using (SmtpClient client = new SmtpClient(server, port))
                using (MailMessage mail = new MailMessage(senderEmail, recipientEmail, subject, body))
                {
                    client.EnableSsl = ssl;
                    client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    await client.SendMailAsync(mail);
                    XtraMessageBox.Show("E-Mail başarıyla gönderildi.", "Başarılı Mail Gönderimi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging("[MailSendForm Error] " + ex);
                XtraMessageBox.Show("E-Mail gönderilirken bir hata oluştu: " + ex.Message, "Mail Gönderim Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}