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

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(User user)
        {
            _dataContext.Users.Add(user);
            return true;
        }

        public bool Remove(User user)
        {
            return _dataContext.Users.Remove(user);
        }

        public bool Remove(Guid id)
        {
            return _dataContext.Users.RemoveAll(g => g.ID.Equals(id)) > 0;
        }

        public User Get(Guid id)
        {
            return _dataContext.Users.Find(g => g.ID.Equals(id));
        }

        public bool Get(Guid id, out User user)
        {
            user = _dataContext.Users.Find(g => g.ID.Equals(id));
            return user != null;
        }

        public IEnumerable<User> GetAll()
        {
            return _dataContext.Users;
        }

        public IEnumerable<User> GetAll(Func<User, bool> filter)
        {
            return _dataContext.Users.Where(filter);
        }

        public bool Update(Guid id, User newUser)
        {
            User user = _dataContext.Users.FirstOrDefault(c => c.ID.Equals(id));
            if (user is null) return false;
            user.Username = newUser.Username;
            user.Password = newUser.Password;
            return true;
        }

        public bool Exists(Guid id)
        {
            return _dataContext.Users.Any(u => u.ID == id);
        }

        public bool Exists(User user)
        {
            return _dataContext.Users.Contains(user);
        }

        public User Get(string username)
        {
            return _dataContext.Users.Find(g => g.Username == username);
        }

        public bool Remove(string username)
        {
            return _dataContext.Users.RemoveAll(g => g.Username.Equals(username)) > 0;
        }
    }
}