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
            this._sqlConnectionString = "Server=localhost;Database=master;Trusted_Connection=True;";    //Nathan's Laptop
            //this._sqlConnectionString = "Server=localhost;Database=master;Trusted_Connection=True;";    //Nathan's desktop computer
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_sqlConnectionString);
        }
    }
}