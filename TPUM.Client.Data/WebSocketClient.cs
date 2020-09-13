using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using TPUM.Communication;

namespace TPUM.Client.Data
{
    public class WebSocketClient
    {
        #region API

        public async Task<WebSocketConnection> Connect(Uri peer, Action<string>? log)
        {
            ClientWebSocket clientWebSocket = new ClientWebSocket();
            await clientWebSocket.ConnectAsync(peer, CancellationToken.None);
            switch (clientWebSocket.State)
            {
                case WebSocketState.Open:
                    log?.Invoke($"Opening WebSocket connection to remote server {peer}");
                    WebSocketConnection socket = new ClientWebSocketConnection(clientWebSocket, peer, log);
                    return socket;

                default:
                    log?.Invoke($"Cannot connect to remote node status {clientWebSocket.State}");
                    throw new WebSocketException($"Cannot connect to remote node status {clientWebSocket.State}");
            }
        }

        #endregion API
    }
}