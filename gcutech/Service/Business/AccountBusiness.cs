using gcutech.Models;
using gcutech.Service.Data;
using gcutech.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace gcutech.Service.Business
{
    public class AccountBusiness : IAccountBusiness<User>
    {
        private ICrud<User> _accountData;

        public AccountBusiness(ICrud<User> accountData)
        {
            this._accountData = accountData;
        }
        /**
         * <see cref="IAccountBusiness{T}"/>
         */
        public User AuthenticateUser(Credentials model)
        {
            //Send credentials to database and get user
            User dbModel = this._accountData.ReadT(model);

            //Hash the credential password for comparison
            model._password = HashPassword(model._password);

            //Compare the passwords of credentials and user
            if (!model._password.Equals(dbModel._credentials._password))
            {
                //Throw exception on failed comparison
                throw new AuthenticationFailedException("One of your credentials is wrong. Please contact support if the issue continues.");
            }

            //Return the user on success
            return dbModel;
        }

        /**
         * <see cref="IAccountBusiness{T}"/>
         */
        public void ProcessDelete(User model)
        {
            //Send user to be deleted.
            _accountData.DeleteT(model);
        }

        /**
         * <see cref="IAccountBusiness{T}"/>
         */
        public void ProcessUpdate(User model)
        {
            //Hash user password
            model._credentials._password = HashPassword(model._credentials._password);

            //Send user to be updated
            _accountData.UpdateT(model);
        }

        /**
         * <see cref="IAccountBusiness{T}"/>
         */
        public void RegisterUser(User model)
        {
            //Hash password
            model._credentials._password = HashPassword(model._credentials._password);
            //Send user to be registered.
            _accountData.CreateT(model);
        }

        /**
         * <summary>Takes in a string and one way encrypts it to a 64 character string.</summary>
         * <param name="rawData">Type String</param>
         * <returns>String</returns>
         */
        private string HashPassword(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}