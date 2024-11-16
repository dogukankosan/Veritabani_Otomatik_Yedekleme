using AsyenOtomatikYedekleme.Classes;
using DevExpress.XtraEditors;
using System;
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
        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (txt_LoginName.Text=="Asyen" && txt_Password.Text=="0212")
            {
                if (SQLLiteConnection.GetSqlConnectionFromSQLITE("select ConnectionName from  SQLConnectionString LIMIT 1") == "")
                {
                    ConnectionForm connectionForm = new ConnectionForm();
                    this.Hide();
                    connectionForm.ShowDialog();
                }
                else
                {
                    if (SQLServerConnection.ConnectionStringGet() != null)
                    {
                        Home hm = new Home();
                        this.Hide();
                        hm.ShowDialog();
                    }
                    else
                    {
                        ConnectionForm connectionForm = new ConnectionForm();
                        this.Hide();
                        connectionForm.ShowDialog();
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Giriş İşlemi Başarısız Tekrar Deneyiniz", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_LoginName.Focus();
            }
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            btn_Eye.Visible = false;
            if (!SQLLiteConnection.GetSqlConnectionControl("SELECT * FROM EMailSetting"))
            {
                XtraMessageBox.Show($"SQLITE Veritabanına Bağlantı Sağlanamadı Lütfen Yolu Kontrol Ediniz {Application.StartupPath}\\Database\\Settings.db", "Hatalı SQLITE Veritabanı Bağlantısı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging($"SQLITE Veritabanına Bağlantı Sağlanamadı Lütfen Yolu Kontrol Ediniz {Application.StartupPath}\\Database\\Settings.db");
                Application.Exit();
            }
        }
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}