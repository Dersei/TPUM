using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TPUM.Client.Data;
using TPUM.Client.Logic.DTO;
using TPUM.Client.Logic.Mapping;
using TPUM.Communication;
using TPUM.Communication.Requests;
using TPUM.Communication.Responses;
using TPUM.Serialization;

namespace TPUM.Client.Logic
{
    public class ClientLogic : IClientLogic
    {

        private static ClientLogic? _instance;

        private static readonly object InstanceLock = new object();

        public static ClientLogic Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    _instance ??= new ClientLogic();
                }
                return _instance;
            }
        }

        private ClientLogic()
        {

        }

        private WebSocketConnection? _webSocket;

        public Action<bool, SessionToken>? OnLoginResponse { get; set; }

        public Action<bool, GameDTO?>? OnCreateGameResponse { get; set; }

        public Action<List<GameDTO>?>? OnGetAllGamesResponse { get; set; }
        public Action<List<PublisherDTO>?>? OnGetAllPublishersResponse { get; set; }
        public Action<List<string>?>? OnGetOtherUsersResponse { get; set; }

        public Action<string>? Log { get; set; }

        public Action? OnClosing { get; set; }
        public Action? OnConnect { get; set; }

        public void Connect(Uri address)
        {
            _webSocketClient = new WebSocketClient();
            Task<WebSocketConnection> task;
            do
            {
                task = _webSocketClient.Connect(address, Log);
            } while (task.Result == null);
            //task.Wait();
            Log?.Invoke("Connecting...");
            _webSocket = task.Result;
            _webSocket.OnClose = () => Debug.WriteLine("Closing...");
            _webSocket.OnMessage = OnResponse;
            OnConnect?.Invoke();
        }

        private WebSocketClient? _webSocketClient;

        private ResponseLogIn? _currentLogInInfo;

        private void OnResponse(string message)
        {
            Interchange response = Serializer.Deserialize<Interchange>(message);
            switch (response)
            {
                case ResponseLogIn rli:
                    _currentLogInInfo = rli;
                    //OnLoginResponse?.Invoke(rli.Success, rli.Token);
                    break;
                case ResponseCreateGame r:
                    OnCreateGameResponse?.Invoke(r.Success, r.CreatedGame?.ToGameDTO());
                    break;
                case ResponseLoggedInUsers rliu:
                    OnGetOtherUsersResponse?.Invoke(rliu.LoggedInUsers);
                    break;
                case ResponseGetAllGames rgag:
                    OnGetAllGamesResponse?.Invoke(rgag.Games?.ToGameDTOs()?.ToList());
                    break;
                case ResponseGetAllPublishers rgap:
                    OnGetAllPublishersResponse?.Invoke(rgap.Publishers?.ToPublisherDTOs()?.ToList());
                    break;
            }
        }

        //public Task TryLogin(UserDTO user)
        //{
        //    Interchange interchange = new RequestLogIn
        //    {
        //        Credentials = user.ToUser()
        //    };
        //    return _webSocket?.SendAsync(Serializer.Serialize(interchange)) ?? Task.CompletedTask;
        //}

        public Task TryLogin(UserDTO user)
        {
            Interchange interchange = new RequestLogIn
            {
                Credentials = user.ToUser()
            };
            Task sendingTask = _webSocket?.SendAsync(Serializer.Serialize(interchange)) ?? Task.CompletedTask;
            sendingTask.Wait();
            while (_currentLogInInfo == null)
            {
                
            }
            OnLoginResponse?.Invoke(_currentLogInInfo.Success, _currentLogInInfo.Token);
            _currentLogInInfo = null;
            return Task.CompletedTask;
        }

        public Task Logout(SessionToken token)
        {
            Interchange interchange = new RequestLogOut
            {
                Token = token
            };
            return _webSocket?.SendAsync(Serializer.Serialize(interchange)) ?? Task.CompletedTask;
        }

        public Task CreateGame(GameDTO game)
        {
            Interchange interchange = new RequestCreateGame()
            {
                Game = game.ToGame()
            };
            return _webSocket?.SendAsync(Serializer.Serialize(interchange)) ?? Task.CompletedTask;
        }

        public Task GetOtherUsers()
        {
            Interchange interchange = new RequestLoggedInUsers();
            return _webSocket?.SendAsync(Serializer.Serialize(interchange)) ?? Task.CompletedTask;
        }

        public Task GetAllGames()
        {
            Interchange interchange = new RequestGetAllGames();
            return _webSocket?.SendAsync(Serializer.Serialize(interchange)) ?? Task.CompletedTask;
        }

        public Task GetAllPublishers()
        {
            Interchange interchange = new RequestGetAllPublishers();
            return _webSocket?.SendAsync(Serializer.Serialize(interchange)) ?? Task.CompletedTask;
        }

        public WebSocketConnection? GetWebSocketConnection() => _webSocket;
    }
}
