using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPUM.Communication;
using TPUM.Communication.DTO;

namespace TPUM.Client.Logic
{
    public interface IClientLogic
    {
        Action<bool, SessionToken>? OnLoginResponse { get; set; }
        Action<bool, GameDTO?>? OnCreateGameResponse { get; set; }
        Action<List<GameDTO>?>? OnGetAllGamesResponse { get; set; }
        Action<List<PublisherDTO>?>? OnGetAllPublishersResponse { get; set; }
        Action<List<string>?>? OnGetOtherUsersResponse { get; set; }
        Action<string>? Log { get; set; }

        Action? OnClosing { get; set; }
        Action? OnConnect { get; set; }
        Task TryLogin(UserDTO user);
        Task Logout(SessionToken token);
        Task CreateGame(GameDTO game);
        Task GetAllGames();
        Task GetOtherUsers();
        void Connect(Uri address);
        WebSocketConnection? GetWebSocketConnection();
    }
}