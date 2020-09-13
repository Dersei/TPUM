using System;
using System.Collections.Generic;
using System.Linq;
using TPUM.Data.Model;
using TPUM.Data.Repositories;
using Xunit;

namespace TPUM.Data.Tests
{
    public class UserRepositoryTest
    {
        private readonly UserRepository _userRepository;

        public UserRepositoryTest()
        {
            DataContext dataContext = DataContext.Instance;
            _userRepository = new UserRepository(dataContext);
        }

        [Theory]
        [InlineData("T1", "P1")]
        [InlineData("T2", "P2")]
        public void AddUser(string name, string password)
        {
            Assert.True(_userRepository.Add(new User(name, password)));
        }

        [Theory]
        [InlineData("T1")]
        [InlineData("T2")]
        public void FindUser(string name)
        {
            _userRepository.Add(new User(name, string.Empty));
            Assert.True(_userRepository.Get(name) != null);
        }

        [Fact]
        public void RemoveUser()
        {
            _userRepository.Add(new User("T1", string.Empty));
            Assert.True(_userRepository.Remove("T1"));
        }

        [Fact]
        public void UpdateUser()
        {
            User user = new User("T1", string.Empty);
            _userRepository.Add(user);
            Assert.True(_userRepository.Exists(user));
            User user2 = _userRepository.GetAll().Last();
            Assert.True(user2.Username == "T1");
        }
    }
}