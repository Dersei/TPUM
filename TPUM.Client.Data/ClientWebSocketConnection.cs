﻿using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPUM.Communication;

namespace TPUM.Client.Data
{
    internal class ClientWebSocketConnection : WebSocketConnection
    {
        public ClientWebSocketConnection(ClientWebSocket clientWebSocket, Uri peer, Action<string>? log)
        {
            _clientWebSocket = clientWebSocket;
            _peer = peer;
            _log = log ?? (s => Debug.WriteLine(s));
            Task.Factory.StartNew(ClientMessageLoop);
        }
        public override bool IsClosed => _clientWebSocket.State == WebSocketState.Closed;
        #region WebSocketConnection

        protected override Task SendTask(string message)
        {
            return _clientWebSocket.SendAsync(message.GetArraySegment(), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public override Task DisconnectAsync()
        {
            return _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Shutdown procedure started", CancellationToken.None);
        }

        #endregion WebSocketConnection

        #region Object

        public override string ToString()
        {
            return _peer.ToString();
        }

        #endregion Object

        #region private

        private readonly ClientWebSocket _clientWebSocket;
        private readonly Uri _peer;
        private readonly Action<string> _log;

        private async Task ClientMessageLoop()
        {
            try
            {
                byte[] buffer = new byte[4096];
                while (true)
                {
                    ArraySegment<byte> segment = new ArraySegment<byte>(buffer);
                    WebSocketReceiveResult result = await _clientWebSocket.ReceiveAsync(segment, CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        OnClose?.Invoke();
                        await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "I am closing", CancellationToken.None);
                        return;
                    }
                    int count = result.Count;
                    //while (!result.EndOfMessage)
                    //{
                    //    if (count >= buffer.Length)
                    //    {
                    //        onClose?.Invoke();
                    //        await _clientWebSocket.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None);
                    //        return;
                    //    }
                    //    segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                    //    result = await _clientWebSocket.ReceiveAsync(segment, CancellationToken.None);
                    //    count += result.Count;
                    //}
                    string message = Encoding.UTF8.GetString(buffer, 0, count);
                    OnMessage?.Invoke(message);
                }
            }
            catch (Exception ex)
            {
                _log($"Connection has been broken because of an exception {ex}");
                await _clientWebSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Connection has been broken because of an exception", CancellationToken.None);
            }
        }

        #endregion private
    }
}