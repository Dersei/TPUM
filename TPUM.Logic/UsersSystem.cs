using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPUM.Data;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;
using TPUM.Data.Repositories;
using TPUM.Logic.DTO;

namespace TPUM.Logic
{
    public class UsersSystem
    {
        private readonly IRepository<User> _repository;

        public UsersSystem()
        {
            _repository = new UserRepository(DataContext.Instance.FillData(new TestDataFiller()));
        }

        public UsersSystem(IRepository<User> repository) => _repository = repository;

        public UserDTO GetGame(Guid id)
        {
            return _repository.Get(id).ToUserDTO();
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _repository.GetAll().ToUserDTOs();
        }

        public async IAsyncEnumerable<string> Simulate()
        {
            List<User> users = _repository.GetAll().ToList();
            foreach (User user in users)
            {
                yield return $"User {user.Username} has logged in" + Environment.NewLine;
                await foreach (string s in await Task.Run(()=> SimulateUser(user)))
                {
                    yield return s;
                }

                
            }
        }

        public async IAsyncEnumerable<string> SimulateUser(User user)
        {
            for(int i = 0; i < 10; i++)
            {
                await Task.Delay(10);
                yield return "User" + user.Username + Environment.NewLine;
            }
        }
    }
}