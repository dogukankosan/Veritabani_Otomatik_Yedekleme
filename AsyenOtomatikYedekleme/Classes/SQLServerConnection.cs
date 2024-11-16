using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Classes
{
    public class SQLServerConnection
    {
        public static string ConnectionStringAdd { get; set; }
        public static string ConnectionStringGet()
        {
            string connectionString = SQLLiteConnection.GetSqlConnectionFromSQLITE();
            SqlConnection sqLConnection = new SqlConnection(connectionString);
            try
            {
                sqLConnection.Open();
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message,"Hatalı SQLSERVER Bağlantısı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
            sqLConnection.Close();
            return connectionString;
        }
        public static string ConnectionStringControl(string serverName, string loginName, string password)
        {
            string connectionString = $"Server={serverName};Database=master;User Id ={loginName}; Password={password};Connection Timeout=10;TrustServerCertificate=True;";
            SqlConnection sqLConnection = new SqlConnection(connectionString);
            try
            {
                sqLConnection.Open();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            sqLConnection.Close();
            ConnectionStringAdd = connectionString;
            return "Başarılı";
        }
        public void LoadDataIntoGridView(GridControl gridControl, string query)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStringGet()))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            gridControl.DataSource = dt;
                        }
                    }
                }
                catch (Exception e)
                {
                    XtraMessageBox.Show(e.Message, "Hatalı Veri Çekme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}