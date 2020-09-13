﻿using System;
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

        public bool Remove(User user)
        {
            lock (_lockObject)
            {
                return _dataContext.Users.Remove(user);
            }
            
        }

        
        public IEnumerable<User> GetAll()
        {
            return _dataContext.Users;
        }

        public IEnumerable<User> GetAll(Func<User, bool> filter)
        {
            return _dataContext.Users.Where(filter);
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
            lock (_lockObject)
            {
                return _dataContext.Users.RemoveAll(g => g.Username.Equals(username)) > 0;
            }
            
        }
    }
}