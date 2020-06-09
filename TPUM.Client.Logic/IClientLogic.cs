using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPUM.Communication.DTO;

namespace TPUM.Client.Logic
{
    public interface IClientLogic
    {
        Action<bool>? OnLoginResponse { get; set; }
        public Action<bool, GameDTO>? OnCreateGameResponse { get; set; }
        Action<List<GameDTO>>? OnGetAllGamesResponse { get; set; }
        Action<string>? Log { get; set; }

        Action? OnClosing { set; }
        Action? OnConnect { get; set; }
        Task TryLogin(UserDTO user);
        Task Logout();
        Task CreateGame(GameDTO game);
        Task GetAllGames();
        void Connect();
    }
}