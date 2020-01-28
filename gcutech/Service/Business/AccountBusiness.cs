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
        public User AuthenticateUser(Credentials model)
        {
            User dbModel = _accountData.ReadT(model);

            model._password = HashPassword(model._password);

            if (!model._password.Equals(dbModel._credentials._password))
            {
                throw new LoginFailedException();
            }

            return dbModel;
        }

        public void ProcessDelete(User model)
        {
            throw new NotImplementedException();
        }

        public void ProcessUpdate(User model)
        {
            throw new NotImplementedException();
        }

        public void RegisterUser(User model)
        {
            model._credentials._password = HashPassword(model._credentials._password);
            _accountData.CreateT(model);
        }

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