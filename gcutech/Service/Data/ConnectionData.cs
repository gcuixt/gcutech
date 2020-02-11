using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gcutech.Service.Data
{
    public class ConnectionData
    {
        private string _dbName;
        private string _username;
        private string _password;
        private string _hostName;
        private string _port;
        private string _sqlConnectionString;

        public ConnectionData()
        {
            //this._sqlConnectionString = "Server=localhost;Database=master;Trusted_Connection=True;";    //Nathan's Laptop
            //this._sqlConnectionString = "Server=localhost;Database=master;Trusted_Connection=True;";    //Nathan's desktop computer
            this._sqlConnectionString = "Server=gcuixt-db-1.ctarlibtti3q.us-west-1.rds.amazonaws.com;Database=master;User ID=GCUIXTAdmin;Password=%02dXkGxq3OjnsTg;";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_sqlConnectionString);
        }
    }
}