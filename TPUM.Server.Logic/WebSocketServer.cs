using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPUM.Communication;
using TPUM.Logic;

namespace TPUM.Server.Logic
{
    public class WebSocketServer
    {
        #region API

        public async Task Start(Uri address, Action<WebSocketConnection> onConnection, PeriodicTask<string>? periodic = null, CancellationTokenSource? tokenSource = null)
        {
            _periodic = periodic;
            _tokenSource = tokenSource;
            await Task.WhenAll(WorkPeriodic(), Loop(address, onConnection));
        }

        public void Stop()
        {
          
        }

        #endregion API

        #region private

        private PeriodicTask<string>? _periodic;
        private CancellationTokenSource? _tokenSource;
        private HttpListener? _server;
        private bool _shouldStop;

        private async Task Loop(Uri uri, Action<WebSocketConnection> onConnection)
        {
            Console.WriteLine("Starting...");
            _server = new HttpListener();
            _server.Prefixes.Add(uri.OriginalString);
            _server.Start();
            while (true)
            {
                if (!_server.IsListening || _shouldStop) return;
                HttpListenerContext hc = await _server.GetContextAsync();
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

        private async Task WorkPeriodic()
        {
            if (_periodic is null) return;
            await foreach (string s in _periodic.Start(_tokenSource?.Token ?? default))
            {
                Console.WriteLine(s);
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
                        OnError?.Invoke();
                        OnClose?.Invoke();
                        return;
                    }
                    ArraySegment<byte> segments = new ArraySegment<byte>(buffer);
                    WebSocketReceiveResult receiveResult = await ws.ReceiveAsync(segments, CancellationToken.None);
                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        OnClose?.Invoke();
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
                    OnMessage?.Invoke(message);
                }
            }
        }

        #endregion private
    }
}