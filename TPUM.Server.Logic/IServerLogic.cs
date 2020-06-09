using System;
using System.Collections.Generic;
using TPUM.Communication;
using TPUM.Communication.DTO;

namespace TPUM.Server.Logic
{
    public interface IServerLogic
    {
        bool Login(UserDTO user);
        void Logout(SessionToken token);
        bool CreateGame(GameDTO game);
        List<GameDTO> GetAllGames();
        List<PublisherDTO> GetAllPublishers();
        void AddMessageAction(EventHandler<string> messageAction);
        void AddSendRecordToLoggedAction(Action<List<int>, string> sendRecordToLoggedAction);
    }
}