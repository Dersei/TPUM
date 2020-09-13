using System;
using System.Collections.Generic;
using System.Linq;
using TPUM.Client.Data.Interfaces;
using TPUM.Communication.TransferModel;

namespace TPUM.Client.Data.Repositories
{
    public class PublisherRepository : IRepository<TransferPublisher>
    {
        private readonly DataContext _dataContext;
        private readonly object _lockObject = new object();

        public PublisherRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(TransferPublisher publisher)
        {
            lock (_lockObject)
            {
                _dataContext.Publishers.Add(publisher);
            }
            return true;
        }

        public bool Remove(TransferPublisher publisher)
        {
            lock (_lockObject)
            {
                return _dataContext.Publishers.Remove(publisher);
            }
        }


        public IEnumerable<TransferPublisher> GetAll()
        {
            return _dataContext.Publishers;
        }

        public IEnumerable<TransferPublisher> GetAll(Func<TransferPublisher, bool> filter)
        {
            return _dataContext.Publishers.Where(filter);
        }

        public bool Exists(TransferPublisher publisher)
        {
            return _dataContext.Publishers.Contains(publisher);
        }
    }
}