using gcutech.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using gcutech.Service.Exceptions;

namespace gcutech.Service.Data
{
    public class AccountData : ICrud<User>
    {
        private ConnectionData _connectionData;

        public AccountData()
        {
            this._connectionData = new ConnectionData();
        }

        public void CreateT(User model)
        {
            try
            {
                using(SqlConnection connection = _connectionData.GetConnection())
                using (SqlCommand command = connection.CreateCommand())
                {

                    command.CommandText = @"INSERT INTO [gcuixt].[dbo].[user] " +
                    "([FULL_NAME], [EMAIL], [STUDENT_ID], [USER_NAME], [PASSWORD]) " +
                    "VALUES (:fullname, :email, :studentid, :username, :password)";

                    command.Parameters.Add(":fullname", SqlDbType.NVarChar, 50).Value = model._fullName;
                    command.Parameters.Add(":email", SqlDbType.NVarChar, 100).Value = model._email;
                    command.Parameters.Add(":studentid", SqlDbType.Int).Value = model._studentID;
                    command.Parameters.Add(":username", SqlDbType.NVarChar, 20).Value = model._credentials._userName;
                    command.Parameters.Add(":password", SqlDbType.NVarChar, 64).Value = model._credentials._password;

                    connection.Open();

                    command.Prepare();

                    command.ExecuteNonQuery();
                }

            } catch(Exception e)
            {
                throw new AccountRegisrationFailedException(e.Message);
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

        public User ReadT(Credentials model)
        {
            User temp = new User();
            try
            {
                string queryString = String.Format("SELECT [USER_ID], " +
                    "[FULL_NAME], " +
                    "[EMAIL], " +
                    "[USER_NAME], " +
                    "[STUDENT_ID]" +
                    "[PASSWORD] " +
                    "FROM [gcuixt].[dbo].[user] " +
                    "WHERE [USER_NAME] = '{0}'",
                    model._userName);

                using (SqlConnection connection = _connectionData.GetConnection())
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    connection.Open();

                    command.Prepare();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        
                            reader.Read();
                            temp._userId = reader.GetInt32(0);
                            temp._fullName = reader.GetString(1);
                            temp._email = reader.GetString(2);
                            temp._studentID = reader.GetInt32(3);
                            temp._credentials = new Credentials(reader.GetString(4), reader.GetString(5));

                            reader.Close();
                            
                        if(!(temp._userId > 0))
                        {
                            throw new RecordNotFoundException("User name was not found.");
                        }
                    }

                    connection.Close();
                }

                return temp;
            }
            catch(Exception e)
            {
                throw new RecordNotFoundException(e.Message);
            }
        }

        public void UpdateT(User model)
        {
            throw new NotImplementedException();
        }
    }
}