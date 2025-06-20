using System;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public class DBConnection
    {
        private static readonly string connectionString = "Data Source=DESKTOP-IIFHV77\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}