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
    public class ChallengeCodeData : ICrud<ChallengeCode>
    {
        private ConnectionData _connectionData;

        public ChallengeCodeData()
        {
            this._connectionData = new ConnectionData();
        }

        /**
         * <see cref="ICrud{T}"/>
         */
        public void CreateT(ChallengeCode model)
        {
            try
            {
                //Create connection and command
                using (SqlConnection connection = _connectionData.GetConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    //Genereate sql script into command
                    command.CommandText = @"INSERT INTO [gcuixt].[dbo].[challengecode] ([CODE]) VALUES (@code);";

                    //Add parameters to the command string
                    command.Parameters.Add("@code", SqlDbType.NVarChar, 5).Value = model._code;


                    //Open the connection
                    connection.Open();

                    //Prepare the statement
                    command.Prepare();

                    //Execute the command
                    command.ExecuteNonQuery();

                    //close the connection
                    connection.Close();
                }
            }catch(Exception e)
            {
                throw new RecordNotCreatedException(e.Message);
            }
        }

        /**
         * <see cref="ICrud{T}"/>
         */
        public void DeleteT(ChallengeCode model)
        {
            throw new NotImplementedException();
        }

        /**
         * <see cref="ICrud{T}"/>
         */
        public List<ChallengeCode> ReadAllT(ChallengeCode model)
        {
            throw new NotImplementedException();
        }

        /**
         * <summary>Gets the challenge code for the date contained in the model param</summary>
         * <param name="model" type="ChallengeCode"></param>
         * <returns>ChallengeCode</returns>
         */
        public ChallengeCode ReadT(ChallengeCode model)
        {
            //Create temp model to store the data
            ChallengeCode temp = new ChallengeCode();
            try
            {
                //Create the connection and command
                using (SqlConnection connection = _connectionData.GetConnection())
                using (SqlCommand command = connection.CreateCommand())
                {
                    //write the sql script to the command
                    command.CommandText = @"SELECT * FROM [gcuixt].[dbo].[challengecode] WHERE convert(nvarchar(10), [DATE], 23) = @date";

                    //add in parameters to the sql script
                    command.Parameters.Add("@date", SqlDbType.NVarChar, 10).Value = model._date.ToString("yyyy-MM-dd");

                    //open connection
                    connection.Open();

                    //prepare the statement
                    command.Prepare();

                    //read the data recieved
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            //read the next line
                            reader.Read();

                            //input data into model
                            temp._code = reader.GetString(1);
                            temp._date = reader.GetDateTime(2);

                            //throw error if nothing returned
                        }
                        else 
                        {
                            throw new RecordNotFoundException("User name was not found.");
                        }

                        //Close the reader
                        reader.Close();
                    }

                    //cose connection
                    connection.Close();
                }

                //return the model
                return temp;
            }
            catch (Exception e)
            {
                throw new RecordNotFoundException(e.Message);
            }
        }

        public ChallengeCode ReadT(Credentials model)
        {
            throw new NotImplementedException();
        }

        /**
         * <see cref="ICrud{T}"/>
         */
        public void UpdateT(ChallengeCode model)
        {
            throw new NotImplementedException();
        }
    }
}