using gcutech.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gcutech.ViewModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Full Name")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Size must be between 5 and 50 characters.")]
        public string _fullName { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "This must be an email")]
        [StringLength(100, ErrorMessage = "Email is too long.")]
        public string _email { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "User Name")]
        [StringLength(20, ErrorMessage = "Username is too long.")]
        public string _username { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name = "Password")]
        public string _password { get; set; }

        [Required(ErrorMessage = "This is a required field.")]
        [Display(Name ="Confirm Password")]
        [Compare("_password", ErrorMessage = "Your passwords did not match")]
        public string _confirmPassword { get; set; }

        public RegisterModel()
        {
        }
        public RegisterModel(string FullName, string Email, string username, string password, string ConfirmPassword)
        {
            
            this._fullName = FullName;
            this._email = Email;
            this._username = username;
            this._password = password;
            this._confirmPassword = ConfirmPassword;
        }
    }
}