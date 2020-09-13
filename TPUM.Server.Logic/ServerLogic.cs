using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPUM.Communication;
using TPUM.Communication.TransferModel;
using TPUM.Logic;
using TPUM.Logic.Systems;

namespace TPUM.Server.Logic
{
    public class ServerLogic : IServerLogic
    {
        private readonly UsersSystem _usersSystem;
        private readonly GamesSystem _gamesSystem;
        private readonly PublishersSystem _publishersSystem;
        private readonly StringLogSender _gameSender;
        private readonly FileLogger _fileLogger;

        public ServerLogic(UsersSystem? usersSystem = null, GamesSystem? gamesSystem = null, PublishersSystem? publishersSystem = null)
        {
            _usersSystem = usersSystem ?? new UsersSystem();
            _gamesSystem = gamesSystem ?? new GamesSystem();
            _publishersSystem = publishersSystem ?? new PublishersSystem();

            _gameSender = new StringLogSender(_gamesSystem, TimeSpan.FromSeconds(10));
            Task.Run(() => _gameSender.SendReport());
            _fileLogger = new FileLogger();
            _fileLogger.Subscribe(_gameSender);
        }

        public bool Login(TransferUser user)
        {
            return _usersSystem.CheckIfExists(user);
        }

        public void Logout(SessionToken token)
        {
            throw new NotImplementedException();
        }

        public bool CreateGame(TransferGame game)
        {
            return _gamesSystem.AddGame(game);
        }

        public List<TransferGame> GetAllGames()
        {
            return _gamesSystem.GetAllGames().ToList();
        }

        public List<TransferPublisher> GetAllPublishers()
        {
            return _publishersSystem.GetAllPublishers().ToList();
        }
    }
}
