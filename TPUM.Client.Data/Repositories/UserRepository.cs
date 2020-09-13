using System;
using System.Collections.Generic;
using System.Linq;
using TPUM.Client.Data.Interfaces;
using TPUM.Communication.TransferModel;

namespace TPUM.Client.Data.Repositories
{
    public class UserRepository : IRepository<TransferUser>
    {
        private readonly DataContext _dataContext;
        private readonly object _lockObject = new object();

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(TransferUser user)
        {
            lock (_lockObject)
            {
                _dataContext.Users.Add(user);
            }
            return true;
        }

        public bool Remove(TransferUser user)
        {
            lock (_lockObject)
            {
                return _dataContext.Users.Remove(user);
            }
        }

        
        public IEnumerable<TransferUser> GetAll()
        {
            return _dataContext.Users;
        }

        public IEnumerable<TransferUser> GetAll(Func<TransferUser, bool> filter)
        {
            return _dataContext.Users.Where(filter);
        }
        
        public bool Exists(TransferUser user)
        {
            return _dataContext.Users.Contains(user);
        }

        public TransferUser Get(string username)
        {
            return _dataContext.Users.Find(g => g.Username == username);
        }

        public bool Remove(string username)
        {
            lock (_lockObject)
            {
                return _dataContext.Users.RemoveAll(g => g.Username.Equals(username)) > 0;
            }
        }
    }
}