using AsyenOtomatikYedekleme.Bussiness;
using AsyenOtomatikYedekleme.Classes;
using AsyenYedekleme.Classes;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Forms
{
    public partial class MailSettingsForm : XtraForm
    {
        public MailSettingsForm()
        {
            InitializeComponent();
        }
        private bool status = false;
        private async void btn_Update_Click(object sender, EventArgs e)
        {
            //if (EMailManager.EmailControl(txt_Email, txt_recipientEmail, txt_Password, txt_Server, txt_Port))
           // {
                if (status == true)
                {
                    string message = await SQLiteHelper.ExecuteNonQueryAsync("UPDATE EMailSetting SET EMail='" + txt_Email.Text + "',RecipientEmail='"+txt_recipientEmail.Text+"',Password='" + EncryptionHelper.Encrypt(txt_Password.Text) + "',Server='" + txt_Server.Text + "', Port='" + txt_Port.Text + "',SSL=" + chk_SSL.Checked + "");
                    if (message == "Başarılı")
                        XtraMessageBox.Show("Mail Güncelleme İşlemi Başarılı", "Başarılı Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        XtraMessageBox.Show(message, "Hatalı Güncelleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TextLog.TextLogging(message);
                    }
                }
                else
                {
                    string message= await SQLiteHelper.ExecuteNonQueryAsync("INSERT INTO EMailSetting (EMail,RecipientEmail,Password,Server,Port,SSL) VALUES ('" + txt_Email.Text + "','"+txt_recipientEmail.Text+"','" + EncryptionHelper.Encrypt(txt_Password.Text) + "','" + txt_Server.Text + "','" + txt_Port.Text + "'," + (chk_SSL.Checked == true ? 1 : 0) + ")");
                    if (message=="Başarılı")
                        XtraMessageBox.Show("Mail Ekleme İşlemi Başarılı","Başarılı Ekleme İşlemi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    else
                    {
                        XtraMessageBox.Show(message, "Hatalı Ekleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TextLog.TextLogging(message);
                    }    
                    status = true;
                }
           // }
        }
        private async void MailSettingsForm_Load(object sender, EventArgs e)
        {
           txt_Port.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txt_Port.Properties.Mask.EditMask = "d";
            txt_Port.Properties.Mask.UseMaskAsDisplayFormat = true;
            btn_NotEye.Visible = false;
            try
            {
                DataTable table = await SQLiteHelper.GetDataTableAsync("SELECT EMail,RecipientEmail,Password,Server,Port,SSL FROM EMailSetting LIMIT 1");
                if (table.Rows.Count > 0)
                {
                    txt_Email.Text = table.Rows[0][0].ToString();
                    txt_recipientEmail.Text = table.Rows[0][1].ToString();
                    txt_Password.Text = EncryptionHelper.Decrypt(table.Rows[0][2].ToString());
                    txt_Server.Text = table.Rows[0][3].ToString();
                    txt_Port.Text = table.Rows[0][4].ToString();
                    chk_SSL.Checked = table.Rows[0][5].ToString() == "1" ? true : false;
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "E-Mail Bilgileri Okunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging(ex.Message);
                status = false;
            }
        }
        private void btn_SendMail_Click(object sender, EventArgs e)
        {
            if (EMailManager.EmailControl(txt_Email, txt_recipientEmail, txt_Password, txt_Server, txt_Port))
            {
                _ = EMailSenderManager.MailSendForm(txt_Email.Text, txt_recipientEmail.Text, txt_Password.Text, txt_Server
                   .Text, int.Parse(txt_Port.Text), chk_SSL.Checked);
            }
        }
        private void btn_NotEye_Click(object sender, EventArgs e)
        {
            txt_Password.Focus();
            btn_Eye.Visible = true;
            btn_NotEye.Visible = false;
            txt_Password.Properties.PasswordChar = '*';
        }
        private void btn_Eye_Click(object sender, EventArgs e)
        {
            txt_Password.Focus();
            btn_Eye.Visible = false;
            btn_NotEye.Visible = true;
            txt_Password.Properties.PasswordChar = '\0';
        }
    }
}