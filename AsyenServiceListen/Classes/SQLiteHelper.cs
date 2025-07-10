using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace AsyenServiceListen.Classes
{
    public static class SQLiteHelper
    {
        private static readonly string connectionString = BuildConnectionString();
        private static string BuildConnectionString()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string baseDir = Path.GetDirectoryName(exePath);
            string upperDir = Directory.GetParent(baseDir)?.FullName;
            string dbPath = Path.Combine(upperDir ?? "", "Database", "Settings.db");
            return $"Data Source={dbPath};Version=3;";
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
                TextLog.TextLogging(ex.Message);
                return null;
            }
        }
    }
}