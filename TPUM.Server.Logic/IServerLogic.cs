using System;
using System.Collections.Generic;
using TPUM.Communication.DTO;

namespace TPUM.Server.Logic
{
    public interface IServerLogic
    {
        bool Login(string userName, string password);
        void Logout(int tokenID);
        bool CreateGame(GameDTO game);
        List<GameDTO> GetAllGames();
        void AddMessageAction(EventHandler<string> messageAction);
        void AddSendRecordToLoggedAction(Action<List<int>, string> sendRecordToLoggedAction);
    }
}