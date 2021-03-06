﻿using gcutech.Models;
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
            throw new NotImplementedException();
        }

        /**
         * <see cref="ICrud{T}"/>
         */
        public void CreateT(User model, string i)
        {
            try
            {
                //Create connection and command
                using(SqlConnection connection = _connectionData.GetConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    //Genereate sql script into command
                    command.CommandText = @"INSERT INTO [gcuixt].[dbo].[user] 
                    ([FULL_NAME], [EMAIL], [USER_NAME], [SALT], [PASSWORD]) 
                    VALUES (@fullname, @email, @username, @salt, @password)";

                    //Add parameters to the command string
                    command.Parameters.Add("@fullname", SqlDbType.NVarChar, 50).Value = model._fullName;
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = model._email;
                    command.Parameters.Add("@username", SqlDbType.NVarChar, 20).Value = model._credentials._userName;
                    command.Parameters.Add("@salt", SqlDbType.NVarChar, 64).Value = i;
                    command.Parameters.Add("@password", SqlDbType.NVarChar, 64).Value = model._credentials._password;

                    //Open the connection
                    connection.Open();

                    //Prepare the statement
                    command.Prepare();

                    //Execute the command
                    command.ExecuteNonQuery();

                    //close the connection
                    connection.Close();
                }

            } catch(Exception e)
            {
                Console.WriteLine("From AccountData@CreateT");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new RecordNotCreatedException("We're sorry, something went wrong creating your account.");
            }
        }


        /**
         * <see cref="ICrud{T}"/>
         */
        public void DeleteT(User model)
        {
            try
            {
                //Create the connection and command
                using(SqlConnection connection = _connectionData.GetConnection())
                    using(SqlCommand command= connection.CreateCommand())
                {
                    //Generate sql script
                    command.CommandText = @"DELETE FROM [gcuixt].[dbo].[user] where [USER_ID] = @userid";

                    //Add parameters to command
                    command.Parameters.Add("@userid", SqlDbType.Int).Value = model._userId;

                    //Open connection
                    connection.Open();

                    //Prepare the statement
                    command.Prepare();

                    //Execute the statement
                    command.ExecuteNonQuery();

                    //Close the connection
                    connection.Close();
                }

            }
            catch(Exception e)
            {
                throw new DeleteFailedException("There was a problem when deleting the account.");
            }
        }

        /**
        * <see cref="ICrud{T}"/>
        */
        public List<User> ReadAllT()
        {
            throw new NotImplementedException();
        }

        public User ReadT(User model)
        {
            throw new NotImplementedException();
        }

        /**
        * <see cref="ICrud{T}"/>
        */
        public User ReadT(Credentials model)
        {
            //Create temp model to store the data
            User temp = new User();
            try
            { 
                //Create the connection and command
                using (SqlConnection connection = _connectionData.GetConnection())
                using(SqlCommand command = connection.CreateCommand())
                {
                    //write the sql script to the command
                    command.CommandText = @"select u.[USER_ID], [FULL_NAME], [EMAIL], [USER_NAME], [PASSWORD], coalesce([ADMIN_LEVEL], -1), coalesce([ADMIN_TITLE], 'User')
	                from [gcuixt].[dbo].[user] AS u left join [gcuixt].[dbo].[admin] AS a ON u.[USER_ID] = a.[USER_ID]
	                where [USER_NAME] = @username";

                    //add in parameters to the sql script
                    command.Parameters.Add("@username", SqlDbType.NVarChar, 20).Value = model._userName;

                    //open connection
                    connection.Open();

                    //prepare the statement
                    command.Prepare();

                    //read the data recieved
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //read the next line
                        reader.Read();

                        //input data into model
                        temp._userId = reader.GetInt32(0);
                        temp._fullName = reader.GetString(1);
                        temp._email = reader.GetString(2);
                        temp._credentials = new Credentials(reader.GetString(3), reader.GetString(4));
                        temp._adminLevel = reader.GetInt32(5);
                        temp._adminTitle = reader.GetString(6);

                        //Close the reader
                        reader.Close();
                            
                        //throw error if nothing returned
                        if(!(temp._userId > 0))
                        {
                            throw new RecordNotFoundException();
                        }
                    }

                    //cose connection
                    connection.Close();
                }

                //return the model
                return temp;
            }
            catch(Exception e)
            {
                throw new RecordNotFoundException("One of you're credentials is wrong. Contact support if the issue persists.");
            }
        }

        /**
        * <see cref="ICrud{T}"/>
        */
        public void UpdateT(User model)
        {
            try
            {
                //Create the connection and command
                using (SqlConnection connection = _connectionData.GetConnection())
                using(SqlCommand command = connection.CreateCommand())
                {
                    //Generate the command sql statement
                    command.CommandText = @"UPDATE [gcuixt].[dbo].[user] SET
                        [FULL_NAME] = @fullname,
                        [EMAIL] = @email,
                        [USER_NAME] = @username,
                        [PASSWORD] = @password
                        WHERE [USER_ID] = @userid";

                    //Add parameters to the statement
                    command.Parameters.Add("@fullname", SqlDbType.NVarChar, 50).Value = model._fullName;
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = model._email;
                    command.Parameters.Add("@username", SqlDbType.NVarChar, 20).Value = model._credentials._userName;
                    command.Parameters.Add("@password", SqlDbType.NVarChar, 64).Value = model._credentials._password;
                    command.Parameters.Add("@userid", SqlDbType.Int).Value = model._userId;

                    //Open the connection
                    connection.Open();

                    //Prepare the statement
                    command.Prepare();

                    //Execute the query
                    command.ExecuteNonQuery();

                    //close the connection
                    connection.Close();
                }

            }
            catch(Exception e)
            {
                throw new UpdateFailedException("There was a problem when updating the account");
            }


        }
    }
}