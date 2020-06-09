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

        public UsersSystem()
        {
            _repository = new UserRepository(DataContext.Instance.FillData(new TestDataFiller()));
        }

        public UsersSystem(IRepository<User> repository) => _repository = repository;

        public string CreateReport()
        {
            List<User> users = _repository.GetAll().ToList();
            StringBuilder sb = new StringBuilder(users.Count);
            foreach (User user in users)
            {
                sb.Append($"User {user.Username} has 0 favourite games");
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public UserDTO GetUser(Guid id)
        {
            return _repository.Get(id).ToUserDTO();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _repository.GetAll().ToUserDTOs();
        }

        public bool CheckIfExists(UserDTO user)
        {
            return _repository.Exists(user.ToUser());
        }

    }
}