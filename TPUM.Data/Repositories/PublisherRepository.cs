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

        public PublisherRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(Publisher publisher)
        {
            _dataContext.Publishers.Add(publisher);
            return true;
        }

        public bool Remove(Publisher publisher)
        {
            return _dataContext.Publishers.Remove(publisher);
        }

        public bool Remove(Guid id)
        {
            return _dataContext.Publishers.RemoveAll(g => g.ID.Equals(id)) > 0;
        }

        public Publisher Get(Guid id)
        {
            return _dataContext.Publishers.Find(g => g.ID.Equals(id));
        }

        public bool Get(Guid id, out Publisher publisher)
        {
            publisher = _dataContext.Publishers.Find(g => g.ID.Equals(id));
            return publisher != null;
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _dataContext.Publishers;
        }

        public IEnumerable<Publisher> GetAll(Func<Publisher, bool> filter)
        {
            return _dataContext.Publishers.Where(filter);
        }

        public bool Update(Guid id, Publisher newPublisher)
        {
            Publisher publisher = _dataContext.Publishers.FirstOrDefault(c => c.ID.Equals(id));
            if (publisher is null) return false;
            publisher.Name = newPublisher.Name;
            publisher.Country = newPublisher.Country;
            return true;
        }
    }
}