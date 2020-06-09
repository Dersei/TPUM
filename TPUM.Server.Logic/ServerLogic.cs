using System;
using System.Collections.Generic;
using TPUM.Communication.DTO;
using TPUM.Logic.Systems;

namespace TPUM.Server.Logic
{
    public class ServerLogic : IServerLogic
    {
        private readonly UsersSystem _usersSystem;
        private readonly GamesSystem _gamesSystem;

        public ServerLogic(UsersSystem? usersSystem = null, GamesSystem? gamesSystem = null)
        {
            _usersSystem = usersSystem ?? new UsersSystem();
            _gamesSystem = gamesSystem ?? new GamesSystem();
        }

        public bool Login(UserDTO user)
        {
            return _usersSystem.CheckIfExists(user);
        }

        public void Logout(int tokenID)
        {
            throw new NotImplementedException();
        }

        public bool CreateGame(GameDTO game)
        {
            return _gamesSystem.AddGame(game);
        }

        public List<GameDTO> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public void AddMessageAction(EventHandler<string> messageAction)
        {
            throw new NotImplementedException();
        }

        public void AddSendRecordToLoggedAction(Action<List<int>, string> sendRecordToLoggedAction)
        {
            throw new NotImplementedException();
        }
    }
}
