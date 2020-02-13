using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gcutech.Models
{
    public class User
    {
        public int _userId { get; set; }


        public string _fullName { get; set; }

        public string _email { get; set; }

        public int _adminLevel { get; set; }

        public string _adminTitle { get; set; }

        public Credentials _credentials { get; set; }

        public User()
        {

        }

        public User(Credentials credentials)
        {
            this._credentials = credentials;
        }
        public User(int UserId, string FullName, string Email, int adminLevel, string adminTitle,  Credentials credentials)
        {
            this._userId = UserId;
            this._fullName = FullName;
            this._email = Email;
            this._adminLevel = adminLevel;
            this._adminTitle = adminTitle;
            this._credentials = credentials;
        }
    }
}