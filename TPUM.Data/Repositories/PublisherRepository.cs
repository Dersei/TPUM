using System;
using System.Collections.Generic;
using System.Linq;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;

namespace TPUM.Data.Repositories
{
    public class PublisherRepository : IRepository<Publisher>
    {
        private readonly DataContext _dataContext;
        private readonly object _lockObject = new object();

        public PublisherRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(Publisher publisher)
        {
            lock (_lockObject)
            {
                _dataContext.Publishers.Add(publisher);
            }
            
            return true;
        }

        public bool Remove(int id)
        {
            lock (_lockObject)
            {
                Publisher? publisher = _dataContext.Publishers.FirstOrDefault(u => u.ID == id);
                return _dataContext.Publishers.Remove(publisher);
            }
        }


        public IEnumerable<Publisher> GetAll()
        {
            return _dataContext.Publishers;
        }

        public IEnumerable<Publisher> GetAll(Func<Publisher, bool> filter)
        {
            return _dataContext.Publishers.Where(filter);
        }

        public bool Exists(Publisher publisher)
        {
            return _dataContext.Publishers.Contains(publisher);
        }
    }
}