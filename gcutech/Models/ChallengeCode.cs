using gcutech.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gcutech.Models
{
    public class ChallengeCode
    {
        [Display(Name="Date")]
        public DateTime _date { get; set; }

        [Display(Name="Validation Code")]
        public string _code { get; set; }

        public ChallengeCode()
        {
            this._date = DateTime.UtcNow.Date;
            this._code = new RandomStringGenerator().GenerateString(5);
        }

        public ChallengeCode(DateTime date, string code)
        {
            this._date = date;
            this._code = code;
        }


    }
}