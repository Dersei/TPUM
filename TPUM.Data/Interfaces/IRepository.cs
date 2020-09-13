using System;
using System.Collections.Generic;

namespace TPUM.Data.Interfaces
{
    public interface IRepository<T>
    {
        bool Add(T item);
        bool Remove(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> filter);
        bool Exists(T item);
        bool Update(int id, T item) => false;
    }
}