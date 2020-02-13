using gcutech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gcutech.Service.Data
{
    public interface ICrud<T>
    {
        /**
         * <summary>Pass in a user/principal model to register the user to the database. 
         * Ensure password encryption is a 64 character encryption</summary>
         * <param name="model">User</param>
         * <exception cref="RegisrationFailedException"></exception>
         */
        void CreateT(T model);
        void CreateT(T model, string i);
        /**
         * <summary></summary>
         */
        T ReadT(T model);

        T ReadT(Credentials model);
        List<T> ReadAllT();
        void UpdateT(T model);
        void DeleteT(T model);
    }
}
