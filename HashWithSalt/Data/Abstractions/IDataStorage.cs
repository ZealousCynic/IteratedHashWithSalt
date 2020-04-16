using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashWithSalt
{
    interface IDataStorage<T> : IDisposable
    {
        void Create(T entity);
        T Read(int id);
        T[] GetAll();
        
    }
}
