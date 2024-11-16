using System;
using System.Data.SqlClient;

namespace VeriTabaniYedeklemeServis.Classes
{
    internal class SQLServerConnection
    {
        internal static string ConnectionStringGet()
        {
            string connectionString = SQLLiteConnection.GetSqlConnectionFromSQLITE();
            SqlConnection sqLConnection = new SqlConnection(connectionString);
            try
            {
                sqLConnection.Open();
            }
            catch (Exception e)
            {
                TextLog.TextLogging(e.Message, "Hatalı");
                return null;
            }
            sqLConnection.Close();
            return connectionString;
        }
    }
}