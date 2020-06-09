using System;
using System.Collections.Generic;
using TPUM.Communication.DTO;
using TPUM.Logic.Systems;

namespace TPUM.Server.Logic
{
    public class ServerLogic : IServerLogic
    {
        private UsersSystem _usersSystem;
        private GamesSystem _gamesSystem;

        public ServerLogic(UsersSystem? usersSystem = null, GamesSystem? gamesSystem = null)
        {
            _usersSystem = usersSystem ?? new UsersSystem();
            _gamesSystem = gamesSystem ?? new GamesSystem();
        }

        public bool Login(string userName, string password)
        {
            throw new NotImplementedException();
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
