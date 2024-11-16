using AsyenYedekleme.Classes;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace AsyenOtomatikYedekleme.Classes
{
    public class SQLLiteConnection
    {
        public static string connectionString = $"Data Source={Application.StartupPath}\\Database\\Settings.db;Version=3;";
        internal static string InserUpdateDelete(string query)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Hatalı Veri Ekleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ex.Message;
                }
                return "Başarılı";
            }
        }
        internal static bool GetSqlConnectionControl(string query)
        {
            bool status = false;
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                status = true;
                        }
                    }
                    conn.Close();
                    return status;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Hatalı Veri Çekme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextLog.TextLogging(ex.Message);
                    return false;
                }
            }
        }
        internal static string GetSqlConnectionFromSQLITE(string query = "select ConnectionName from  SQLConnectionString LIMIT 1")
        {
            string sqlConnection = "";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                sqlConnection = Convert.ToString(reader["ConnectionName"]);
                        }
                    }
              
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Hatalı Veri Çekme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextLog.TextLogging(ex.Message);
                }
                conn.Close();
                return EncryptionHelper.Decrypt(sqlConnection);
            }
        }
        public void LoadDataIntoGridView(GridControl gridControl, string query)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            gridControl.DataSource = dt;
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Hatalı Veri Çekme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextLog.TextLogging(ex.Message);
                }
                conn.Close();
            }
        }
        public static DataTable GetDataFromSQLite(string query)
        {
            DataTable dataTable = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "Hatalı Veri Çekme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextLog.TextLogging(ex.Message);
                    return null;
                }
                connection.Close();
            }
            return dataTable;
        }
    }
}