using AsyenVeriTabaniYedeklemeServis.Classes;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace VeriTabaniYedeklemeServis.Classes
{
    internal class SQLLiteConnection
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["SQLiteConnectionString"].ConnectionString;
        internal static DataTable GetDataFromSQLite(string query)
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
                    TextLog.TextLogging(ex.Message,"Hatalı");
                    return null;
                }
                return dataTable;
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
                    TextLog.TextLogging(ex.Message,"Hatalı");
                    return null;
                }
                conn.Close();
                return EncryptionHelper.Decrypt(sqlConnection);
            }
        }
    }
}