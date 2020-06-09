using System;
using System.Collections.Generic;
using System.Linq;
using TPUM.Communication;
using TPUM.Communication.DTO;
using TPUM.Logic.Systems;

namespace TPUM.Server.Logic
{
    public class ServerLogic : IServerLogic
    {
        private readonly UsersSystem _usersSystem;
        private readonly GamesSystem _gamesSystem;
        private readonly PublishersSystem _publishersSystem;

        public ServerLogic(UsersSystem? usersSystem = null, GamesSystem? gamesSystem = null, PublishersSystem? publishersSystem = null)
        {
            _usersSystem = usersSystem ?? new UsersSystem();
            _gamesSystem = gamesSystem ?? new GamesSystem();
            _publishersSystem = publishersSystem ?? new PublishersSystem();
        }

        public bool Login(UserDTO user)
        {
            return _usersSystem.CheckIfExists(user);
        }

        public void Logout(SessionToken token)
        {
            throw new NotImplementedException();
        }

        public bool CreateGame(GameDTO game)
        {
            return _gamesSystem.AddGame(game);
        }

        public List<GameDTO> GetAllGames()
        {
            return _gamesSystem.GetAllGames().ToList();
        }

        public List<PublisherDTO> GetAllPublishers()
        {
            return _publishersSystem.GetAllPublishers().ToList();
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
