using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPUM.Communication.DTO;
using TPUM.Data;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;
using TPUM.Data.Repositories;
using TPUM.Logic.Interfaces;
using TPUM.Logic.Mapping;

namespace TPUM.Logic.Systems
{
    public class UsersSystem : IReportable
    {
        private readonly IRepository<User> _repository;
        private readonly object _syncObject = new object();

        public UsersSystem()
        {
            _repository = new UserRepository(DataContext.Instance.FillData(new TestDataFiller()));
        }

        public UsersSystem(IRepository<User> repository) => _repository = repository;

        public string CreateReport()
        {
            lock (_syncObject)
            {
                List<User> users = _repository.GetAll().ToList();
                StringBuilder sb = new StringBuilder(users.Count);
                foreach (User user in users)
                {
                    sb.Append($"User {user.Username} exists");
                    sb.Append(Environment.NewLine);
                }

                return sb.ToString();
            }
        }

        public UserDTO GetUser(Guid id)
        {
            lock (_syncObject)
                return _repository.Get(id).ToUserDTO();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            lock (_syncObject)
                return _repository.GetAll().ToUserDTOs();
        }

        public bool CheckIfExists(UserDTO user)
        {
            lock (_syncObject)
                return _repository.Exists(user.ToUser());
        }

    }
}