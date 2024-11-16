using AsyenOtomatikYedekleme.Classes;
using AsyenYedekleme.Classes;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Forms
{
    public partial class ConnectionForm :XtraForm
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            string message = SQLServerConnection.ConnectionStringControl(txt_ServerName.Text, txt_LoginName.Text, txt_Password.Text);
            if (message == "Başarılı")
            {
                if (SQLLiteConnection.GetSqlConnectionFromSQLITE("select ConnectionName from  SQLConnectionString LIMIT 1") == "")
                    SQLLiteConnection.InserUpdateDelete($"INSERT INTO  SQLConnectionString (ConnectionName) VALUES ('{ EncryptionHelper.Encrypt(SQLServerConnection.ConnectionStringAdd)}') ");
                else
                    SQLLiteConnection.InserUpdateDelete($"UPDATE SQLConnectionString SET ConnectionName='"+ EncryptionHelper.Encrypt(SQLServerConnection.ConnectionStringAdd) + "'");
                this.Hide();
                Home hm = new Home();
                hm.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("Veritabanı Bağlantısı Hatalı Tekrar Deneyiniz", "Hatalı Bağlantı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_ServerName.Focus();
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