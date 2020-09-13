using System;
using System.Collections.Generic;
using TPUM.Communication;
using TPUM.Communication.TransferModel;

namespace TPUM.Server.Logic
{
    public interface IServerLogic
    {
        bool Login(TransferUser user);
        void Logout(SessionToken token);
        bool CreateGame(TransferGame game);
        List<TransferGame> GetAllGames();
        List<TransferPublisher> GetAllPublishers();
    }
}