using AsyenOtomatikYedekleme.Classes;
using AsyenYedekleme.Classes;
using DevExpress.XtraEditors;
using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Forms
{
    public partial class ConnectionForm :XtraForm
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string message = await SQLServerHelper.ConnectionStringControlAsync(
                    txt_ServerName.Text.Trim(),
                    txt_LoginName.Text.Trim(),
                    txt_Password.Text,
                    nmr_Port.Value
                );

                if (message == "Başarılı")
                {
                    string encryptedConnection = EncryptionHelper.Encrypt(SQLServerHelper.ConnectionStringAdd);
                    string existing = await SQLiteHelper.GetScalarAsync("SELECT ConnectionName FROM SQLConnectionString LIMIT 1");
                    if (string.IsNullOrWhiteSpace(existing))
                    {
                        await SQLiteHelper.ExecuteNonQueryAsync("INSERT INTO SQLConnectionString (ConnectionName) VALUES (@conn)",
                            new SQLiteParameter("@conn", encryptedConnection));
                    }
                    else
                    {
                        await SQLiteHelper.ExecuteNonQueryAsync("UPDATE SQLConnectionString SET ConnectionName = @conn",
                            new SQLiteParameter("@conn", encryptedConnection));
                    }
                    Hide();
                    using (Home home = new Home())
                    {
                        home.ShowDialog();
                    }
                }
                else
                {
                    XtraMessageBox.Show("Veritabanı bağlantısı hatalı, lütfen tekrar deneyiniz.", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_ServerName.Focus();
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[btn_Save_Click] {ex}");
                XtraMessageBox.Show("Beklenmedik bir hata oluştu. Detaylar log dosyasına yazıldı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_NotEye_Click(object sender, EventArgs e)
        {
            txt_Password.Focus();
            btn_Eye.Visible = true;
            btn_NotEye.Visible = false;
            txt_Password.Properties.PasswordChar = '\0';
        }
        private void btn_Eye_Click(object sender, EventArgs e)
        {
            txt_Password.Focus();
            btn_Eye.Visible = false;
            btn_NotEye.Visible = true;
            txt_Password.Properties.PasswordChar = '*';
        }
        private void ConnectionForm_Load(object sender, EventArgs e)
        {
            btn_Eye.Visible = false;
        }
        private void ConnectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}