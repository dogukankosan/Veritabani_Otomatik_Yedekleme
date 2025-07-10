using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Classes
{
    public static class SQLServerHelper
    {
        public static string ConnectionStringAdd { get; private set; }
        public static async Task<string> ConnectionStringGetAsync()
        {
            try
            {
                string connectionString =await SQLiteHelper.GetEncryptedConnectionAsync();
                using (SqlConnection connection = new SqlConnection(connectionString))
                    await connection.OpenAsync();
                return connectionString;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hatalı SQLSERVER Bağlantısı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging($"[ConnectionStringGetAsync] {ex}");
                return null;
            }
        }
        public static async Task<string> ConnectionStringControlAsync(string serverName, string loginName, string password,decimal port=1433)
        {
            string fullServer = $"{serverName},{port}";
            string connectionString = $"Server={fullServer};Database=master;User Id={loginName};Password={password};Connection Timeout=10;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                    await connection.OpenAsync();
                ConnectionStringAdd = connectionString;
                return "Başarılı";
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[ConnectionStringControlAsync] {ex}");
                return ex.Message;
            }
        }
        public static async Task LoadDataIntoGridViewAsync(GridControl gridControl, string query)
        {
            string connStr = await ConnectionStringGetAsync();
            if (string.IsNullOrWhiteSpace(connStr))
                return;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        gridControl.Invoke(new Action(() =>
                        {
                            gridControl.DataSource = dt;
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Hatalı Veri Çekme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextLog.TextLogging($"[LoadDataIntoGridViewAsync] {ex}");
            }
        }
    }
}