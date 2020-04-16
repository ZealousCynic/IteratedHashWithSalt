using System;

namespace HashWithSalt
{
    interface IDataStorage<T> : IDisposable
    {
        void Create(T entity);
        T Read(int id);
        T[] GetAll();
        
    }
}
