using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsyenYedekleme.Classes;

namespace AsyenOtomatikYedekleme.Classes
{
    public static class SQLiteHelper
    {
        private static readonly string connectionString = $"Data Source={Application.StartupPath}\\Database\\Settings.db;Version=3;";
        public static async Task<string> ExecuteNonQueryAsync(string query)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return "Başarılı";
            }
            catch (Exception ex)
            {
                ShowError("Hatalı Veri Ekleme İşlemi", ex);
                return ex.Message;
            }
        }
        public static async Task<string> GetEncryptedConnectionAsync(string query = "SELECT ConnectionName FROM SQLConnectionString LIMIT 1")
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                string connStr = Convert.ToString(reader["ConnectionName"]);
                                return EncryptionHelper.Decrypt(connStr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Hatalı Veri Çekme İşlemi", ex);
            }
            return null;
        }
        public static async Task<DataTable> GetDataTableAsync(string query)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Hatalı Veri Çekme İşlemi", ex);
                return null;
            }
        }
        private static void ShowError(string title, Exception ex)
        {
            XtraMessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            TextLog.TextLogging(ex.Message);
        }
        public static async Task<string> GetScalarAsync(string query)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        object result = await cmd.ExecuteScalarAsync();
                        return result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[GetScalarAsync] {ex}");
                return null;
            }
        }
        public static async Task ExecuteNonQueryAsync(string query, params SQLiteParameter[] parameters)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[ExecuteNonQueryAsync] {ex}");
            }
        }
    }
}