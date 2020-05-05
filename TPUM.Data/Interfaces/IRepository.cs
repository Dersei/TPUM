using System;
using System.Collections.Generic;
using System.ComponentModel;
using TPUM.Data.Model;

namespace TPUM.Data.Interfaces
{
    public interface IRepository<T> where T : IdItem
    {
        bool Add(T item);
        bool Remove(T item);
        bool Remove(Guid id);
        T Get(Guid id);
        bool Get(Guid id, out T item);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> filter);
        bool Update(Guid id, T newItem);
    }
}