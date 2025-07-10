using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace VeriTabaniYedeklemeServis.Classes
{
    internal static class SQLServerConnection
    {
        internal  static async Task<string>ConnectionStringGet()
        {
            try
            {
                string connectionString = await SQLiteHelper.GetEncryptedConnectionAsync();
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    TextLog.TextLogging("SQLite bağlantı bilgisinden geçerli bir bağlantı cümlesi elde edilemedi.");
                    return null;
                }
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                   await sqlConnection.OpenAsync(); 
                    sqlConnection.Close();
                }
                return connectionString;
            }
            catch (Exception ex)
            {
                TextLog.TextLogging($"[ConnectionStringGet] SQL Server bağlantısı kurulamadı: {ex}");
                return null;
            }
        }
    }
}