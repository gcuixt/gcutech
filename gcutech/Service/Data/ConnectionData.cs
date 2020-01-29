using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gcutech.Service.Data
{
    public class ConnectionData
    {
        private string _sqlConnectionString;

        public ConnectionData()
        {
            this._sqlConnectionString = "Data Source=DESKTOP-98ADUJO;Database=gcuixt;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //this._sqlConnectionString = "Server=localhost;Database=gcuixt;Trusted_Connection=True;";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_sqlConnectionString);
        }
    }
}