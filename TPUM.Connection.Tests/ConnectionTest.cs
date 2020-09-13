using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TPUM.Client.Logic;
using TPUM.Communication;
using TPUM.Communication.Requests;
using TPUM.Serialization;
using TPUM.Server.GUI;
using TPUM.Server.Logic;
using Xunit;
using Xunit.Sdk;

namespace TPUM.Connection.Tests
{
    public class ConnectionTest
    {
        private readonly Uri _clientAddress = new Uri("ws://localhost:8081/");
        private WebSocketServer _webSocketServer;

        [Fact]
        public void GetAllGamesTest()
        {
            _webSocketServer = new WebSocketServer();
            _webSocketServer.Start(new Uri($@"http://localhost:{8081}/"), OnConnection);
            ClientLogic webSocketClient = ClientLogic.Instance;
            webSocketClient.Connect(_clientAddress);
            webSocketClient.Logout(default);
        }

        private void OnConnection(WebSocketConnection connection)
        {
            Assert.NotNull(connection);
            connection.OnMessage = s =>
            {
                RequestLogOut value = Serializer.Deserialize<RequestLogOut>(s);
                Assert.NotNull(value);
                _webSocketServer.Stop();
            };
            connection.OnError = () =>
            {
                Console.WriteLine("Error occured");
            };
            connection.OnClose = () => Console.WriteLine("Closing");

        }
    }
}
