using System;
using System.Collections.Generic;

namespace TPUM.Client.Data.Interfaces
{
    public interface IRepository<T>
    {
        bool Add(T item);
        bool Remove(T item);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> filter);
        bool Exists(T item);
    }
}