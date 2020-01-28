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
        private ConnectionData _ConnectionData;

        public AccountData()
        {
            this._ConnectionData = new ConnectionData();
        }
        public void CreateT(User model)
        {
            try
            {
                string queryString = String.Format("INSERT INTO [dbo].[user] ([FULL_NAME], [EMAIL], [USER_NAME], [PASSWORD]) VALUES ('{0}', '{1}', '{2}', '{3}')",
                    model._fullName,
                    model._email,
                    model._credentials._userName,
                    model._credentials._password);
                string connectionString = "Server=localhost;Database=gcuixt;Trusted_Connection=True;";

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    
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
                    "[PASSWORD] " +
                    "FROM [gcuixt].[dbo].[user] " +
                    "WHERE [USER_NAME] = '{0}'",
                    model._userName);

                string connectionString = "Server=localhost;Database=gcuixt;Trusted_Connection=True;";

                using (SqlConnection connection = new SqlConnection(connectionString))
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
                            temp._credentials = new Credentials(reader.GetString(3), reader.GetString(4));

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