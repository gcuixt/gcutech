using gcutech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gcutech.Service.Business
{
    public interface IAccountBusiness<T>
    {
        T AuthenticateUser(Credentials model);
        void RegisterUser(T model);
        void ProcessDelete(T model);
        void ProcessUpdate(T model);
    }
}
