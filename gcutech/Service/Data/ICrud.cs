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
        void CreateT(T model);
        T ReadT(Credentials model);
        List<T> ReadAllT(T model);
        void UpdateT(T model);
        void DeleteT(T model);
    }
}
