using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TPUM.Communication;
using TPUM.Communication.DTO;
using TPUM.Serialization;

namespace TPUM.Client.Logic
{
    public class ClientLogic : IClientLogic
    {
        private WebSocketConnection _webSocket = null;

        public Action<bool>? OnLoginResponse { get; set; }

        public Action<bool>? OnCreateGameResponse { get; set; }

        public Action<List<GameDTO>>? OnGetAllGamesResponse { get; set; }

        public Action<string>? Log { get; set; }

        public Action? OnClosing { get; set; }
        public Action? OnConnect { get; set; }

        public void Connect()
        {
            Task<WebSocketConnection> task;
            do
            {
                task = WebSocketClient.Connect(new Uri("ws://localhost:8081/"), Log);
            } while (task.Result == null);
            task.Wait();
            Log.Invoke("Connecting...");
            _webSocket = task.Result;
            _webSocket.onClose = () => Debug.WriteLine("Closing...");
            _webSocket.onMessage = OnResponse;
            OnConnect?.Invoke();
        }

        private void OnResponse(string message)
        {
            Interchange response = Serializer.Deserialize<Interchange>(message);
            if (response is Response r)
            {
                OnCreateGameResponse?.Invoke(r.Success);
            }
        }

        public Task TryLogin(string name, string password)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task CreateGame(GameDTO game)
        {
            Interchange interchange = new RequestCreateGame()
            {
                Game = game
            };
            return _webSocket.SendAsync(Serializer.Serialize(interchange));
        }

        public Task GetAllGames()
        {
            throw new NotImplementedException();
        }
    }
}
