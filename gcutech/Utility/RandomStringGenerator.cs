using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace gcutech.Utility
{
    public class RandomStringGenerator
    {
        /**
         * <summary>Generates a random 5 character string.</summary>
         * <returns>string</returns>
         */
        public string GenerateString()
        {
            RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            byte[] code = new byte[5];
            rngCsp.GetBytes(code, 0, 5);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < code.Length; i++)
            {
                builder.Append(code[i].ToString("x2"));
            }
            return builder.ToString().Substring(0, 5);
        }
    }
}