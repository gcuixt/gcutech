using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gcutech.Models
{
    public class Credentials
    {
        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "UserName")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be between 5 and 20 characters.")]
        public string _userName { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Password")]
        public string _password { get; set; }

        public Credentials()
        {
        }
        public Credentials(string Email, string Password)
        {
            this._userName = Email;
            this._password = Password;
        }
    }
}