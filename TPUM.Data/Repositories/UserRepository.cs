using System;
using System.Collections.Generic;
using System.Linq;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;

namespace TPUM.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly DataContext _dataContext;
        private readonly object _lockObject = new object();

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(User user)
        {
            lock (_lockObject)
            {
                _dataContext.Users.Add(user);
            }
            
            return true;
        }

        public bool Remove(int id)
        {
            lock (_lockObject)
            {
                User? user = _dataContext.Users.FirstOrDefault(u => u.ID == id);
                return _dataContext.Users.Remove(user);
            }
        }

        
        public IEnumerable<User> GetAll()
        {
            lock (_lockObject)
                return _dataContext.Users;
        }

        public IEnumerable<User> GetAll(Func<User, bool> filter)
        {
            lock (_lockObject)
                return _dataContext.Users.Where(filter);
        }
        
        public bool Exists(User user)
        {
            lock (_lockObject)
                return _dataContext.Users.Contains(user);
        }

        public User Get(string username)
        {
            lock (_lockObject)
                return _dataContext.Users.Find(g => g.Username == username);
        }

        public bool Remove(string username)
        {
            lock (_lockObject)
                return _dataContext.Users.RemoveAll(g => g.Username.Equals(username)) > 0;
        }

        public bool Update(int id, User user)
        {
            lock (_lockObject)
            {
                User old = _dataContext.Users.Find(g => g.ID == id);
                old.Username = user.Username;
                old.Password = user.Password;
            }

            return true;
        }
    }
}