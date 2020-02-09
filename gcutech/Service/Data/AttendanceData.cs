using gcutech.Models;
using gcutech.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gcutech.Service.Data
{
    public class AttendanceData : ICrud<User>
    {
        private ConnectionData _connectionData;

        public AttendanceData()
        {
            this._connectionData = new ConnectionData();
        }

        public void CreateT(User model)
        {
            try
            {
                //Create connection and command
                using (SqlConnection connection = _connectionData.GetConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    //Genereate sql script into command
                    command.CommandText = @"INSERT INTO [gcuixt].[dbo].[checkedin] ([USER_ID]) VALUES (@userid);";

                    //Add parameters to the command string
                    command.Parameters.Add("@userid", SqlDbType.Int).Value = model._userId;


                    //Open the connection
                    connection.Open();

                    //Prepare the statement
                    command.Prepare();

                    //Execute the command
                    command.ExecuteNonQuery();

                    //close the connection
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new RecordNotCreatedException(e.Message);
            }
        }

        public void DeleteT(User model)
        {
            throw new NotImplementedException();
        }

        public List<User> ReadAllT(User model)
        {
            throw new NotImplementedException();
        }

        public User ReadT(User model)
        {
            throw new NotImplementedException();
        }

        public User ReadT(Credentials model)
        {
            throw new NotImplementedException();
        }

        public void UpdateT(User model)
        {
            throw new NotImplementedException();
        }
    }
}