﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gcutech.Models
{
    public class User
    {
        public int _userId { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name ="Full Name")]
        [StringLength(50, MinimumLength = 5,  ErrorMessage = "Size must be between 5 and 50 characters.")]
        public string _fullName { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "This must be an email")]
        [StringLength(100, ErrorMessage = "Email is too long.")]
        public string _email { get; set; }

        public Credentials _credentials { get; set; }

        public User()
        {

        }

        public User(Credentials credentials)
        {
            this._credentials = credentials;
        }
        public User(int UserId, string FullName, string Email, Credentials credentials)
        {
            this._userId = UserId;
            this._fullName = FullName;
            this._email = Email;
            this._credentials = credentials;
        }
    }
}