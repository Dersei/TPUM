using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPUM.Communication;

namespace TPUM.Server
{
    public static class WebSocketServer
    {
        #region API

        public static async Task Server(int p2pPort, Action<WebSocketConnection> onConnection)
        {
            Uri uri = new Uri($@"http://localhost:{p2pPort}/");
            await ServerLoop(uri, onConnection);
        }

        #endregion API

        #region private

        private static async Task ServerLoop(Uri uri, Action<WebSocketConnection> onConnection)
        {
            HttpListener server = new HttpListener();
            server.Prefixes.Add(uri.OriginalString);
            server.Start();
            while (true)
            {
                HttpListenerContext hc = await server.GetContextAsync();
                if (!hc.Request.IsWebSocketRequest)
                {
                    hc.Response.StatusCode = 400;
                    hc.Response.Close();
                }
                HttpListenerWebSocketContext context = await hc.AcceptWebSocketAsync(null);
                WebSocketConnection ws = new ServerWebSocketConnection(context.WebSocket, hc.Request.RemoteEndPoint);
                onConnection?.Invoke(ws);
            }
        }

        private class ServerWebSocketConnection : WebSocketConnection
        {
            public ServerWebSocketConnection(WebSocket webSocket, IPEndPoint remoteEndPoint)
            {
                _webSocket = webSocket;
                _remoteEndPoint = remoteEndPoint;
                Task.Factory.StartNew(async () => await ServerMessageLoop(webSocket));
            }

            #region WebSocketConnection

            protected override Task SendTask(string message)
            {
                return _webSocket.SendAsync(message.GetArraySegment(), WebSocketMessageType.Text, true, CancellationToken.None);
            }

            public override bool IsClosed => _webSocket.State == WebSocketState.Closed;

            public override Task DisconnectAsync()
            {
                return _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Shutdown procedure started", CancellationToken.None);
            }

            #endregion WebSocketConnection

            #region Object

            public override string ToString()
            {
                return _remoteEndPoint.ToString();
            }

            #endregion Object

            private readonly WebSocket _webSocket;
            private readonly IPEndPoint _remoteEndPoint;

            private async Task ServerMessageLoop(WebSocket ws)
            {
                byte[] buffer = new byte[4096];
                while (_webSocket.State == WebSocketState.Open)
                {
                    if (_webSocket.State == WebSocketState.Closed || _webSocket.State == WebSocketState.Aborted)
                    {
                        onError?.Invoke();
                        onClose?.Invoke();
                        return;
                    }
                    ArraySegment<byte> segments = new ArraySegment<byte>(buffer);
                    WebSocketReceiveResult receiveResult = await ws.ReceiveAsync(segments, CancellationToken.None);
                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        onClose?.Invoke();
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "I am closing", CancellationToken.None);
                        return;
                    }
                    int count = receiveResult.Count;
                    //while (!receiveResult.EndOfMessage)
                    //{
                    //    if (count >= buffer.Length)
                    //    {
                    //        onClose?.Invoke();
                    //        await ws.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None);
                    //        return;
                    //    }
                    //    segments = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                    //    receiveResult = await ws.ReceiveAsync(segments, CancellationToken.None);
                    //    count += receiveResult.Count;
                    //}
                    string message = Encoding.UTF8.GetString(buffer, 0, count);
                    onMessage?.Invoke(message);
                }
            }
        }

        #endregion private
    }
}