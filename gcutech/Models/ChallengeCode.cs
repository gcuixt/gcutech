using gcutech.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gcutech.Models
{
    public class ChallengeCode
    {
        public DateTime _date { get; set; }
        public string _code { get; set; }

        public ChallengeCode()
        {
            this._date = DateTime.UtcNow.Date;
            this._code = new RandomStringGenerator().GenerateString();
        }

        public ChallengeCode(DateTime date, string code)
        {
            this._date = date;
            this._code = code;
        }


    }
}