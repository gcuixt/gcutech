using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gcutech.Service.Data
{
    internal class ConnectionData
    {
        private static string _SqlConnectionString;

        internal ConnectionData()
        {
            _SqlConnectionString = "Server=localhost;Database=master;Trusted_Connection=True;";
        }

        internal static SqlConnection GetConnection()
        {
            return new SqlConnection(_SqlConnectionString);
        }
    }
}