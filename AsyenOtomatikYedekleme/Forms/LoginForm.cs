using AsyenOtomatikYedekleme.Classes;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Forms
{
    public partial class LoginForm : XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();
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
        private async void btn_Login_Click(object sender, EventArgs e)
        {
            string username = txt_LoginName.Text?.Trim();
            string password = txt_Password.Text?.Trim();
            if (username.Equals("Asyen", StringComparison.OrdinalIgnoreCase) && password == "0212")
            {
                try
                {
                    string encryptedConn = await SQLiteHelper.GetEncryptedConnectionAsync("SELECT ConnectionName FROM SQLConnectionString LIMIT 1");
                    Form nextForm;
                    if (string.IsNullOrWhiteSpace(encryptedConn))
                        nextForm = new ConnectionForm();
                    else if (!string.IsNullOrEmpty(await SQLServerHelper.ConnectionStringGetAsync()))
                        nextForm = new Home();
                    else
                        nextForm = new ConnectionForm();
                    this.Hide();
                    nextForm.ShowDialog();
                    this.Close();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Bir hata oluştu:\n{ex.Message}", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextLog.TextLogging($"Giriş Hatası: {ex}");
                }
            }
            else
            {
                XtraMessageBox.Show("Giriş işlemi başarısız. Lütfen tekrar deneyin.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_LoginName.Focus();
            }
        }
        private async void LoginForm_Load(object sender, EventArgs e)
        {
            btn_Eye.Visible = false;
            DataTable dt = await SQLiteHelper.GetDataTableAsync("SELECT * FROM EMailSetting");
            if (dt == null || dt.Rows.Count == 0)
            {
                string path = $"{Application.StartupPath}\\Database\\Settings.db";
                XtraMessageBox.Show(
                    $"SQLITE Veritabanına Bağlantı Sağlanamadı.\nLütfen yolu kontrol ediniz:\n{path}",
                    "Hatalı SQLITE Veritabanı Bağlantısı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                TextLog.TextLogging($"Veritabanı bağlantısı sağlanamadı veya EMailSetting tablosu boş: {path}");
                Application.Exit();
                return;
            }
        }
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}