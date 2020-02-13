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

        public void CreateT(User model, string i)
        {
            throw new NotImplementedException();
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
                throw new RecordNotCreatedException("We're sorry, something went wrong checking you in.");
            }
        }

        public void DeleteT(User model)
        {
            throw new NotImplementedException();
        }

        public List<User> ReadAllT()
        {
            throw new NotImplementedException();
        }
        public List<User> ReadAllT(DateTime date)
        {
            //Create temp model to store the data
            List<User> attendance = new List<User>();
            try
            {
                //Create the connection and command
                using (SqlConnection connection = _connectionData.GetConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    //write the sql script to the command
                    command.CommandText = @"Select [FULL_NAME], [EMAIL], [USER_NAME] FROM [gcuixt].[dbo].[checkedin] as c
	                                        left join [gcuixt].[dbo].[user] as u on c.[USER_ID] = u.[USER_ID]
	                                        where [CHECKED_IN] = FORMAT(@date, 'd')";

                    //add in parameters to the sql script
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = date;

                    //open connection
                    connection.Open();

                    //prepare the statement
                    command.Prepare();

                    //read the data recieved
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        //Default user to add data too
                        User temp;

                        //input data into model
                        if(reader.HasRows)
                        {

                            //read the next line
                            while (reader.Read())
                            {
                                temp = new User(
                                -1,
                                reader.GetString(0),
                                reader.GetString(1),
                                -1,
                                "",
                                new Credentials(reader.GetString(2), ""));

                                attendance.Add(temp);
                            }
                        }

                        //Close the reader
                        reader.Close();
                    }

                    //cose connection
                    connection.Close();
                }

                //return the model
                return attendance;
            }
            catch (Exception e)
            {
                throw new RecordNotFoundException("No Users to display");
            }
        }

        public User ReadT(User model)
        {
            try
            {
                User temp = new User();
                //Create connection and command
                using (SqlConnection connection = _connectionData.GetConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    //Genereate sql script into command
                    command.CommandText = @"select [USER_ID], [CHECKED_IN] from [gcuixt].[dbo].[checkedin] WHERE [USER_ID] = @userid 
                                            and [CHECKED_IN] = Format(GETDATE(), 'd');";

                    //Add parameters to the command string
                    command.Parameters.Add("@userid", SqlDbType.Int).Value = model._userId;

                    //Open the connection
                    connection.Open();

                    //Prepare the statement
                    command.Prepare();

                    //Execute the command
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            temp._userId = reader.GetInt32(0);
                        }
                        else
                        {
                            temp._userId = -1;
                        }
                        
                    }

                    //close the connection
                    connection.Close();

                    return temp;
                }
            }
            catch (Exception e)
            {
                throw new RecordNotFoundException("Technical error, please try again later. Contact support if the proplem persists.");
            }
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