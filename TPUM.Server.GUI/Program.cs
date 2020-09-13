using System;
using System.Threading.Tasks;
using TPUM.Communication;
using TPUM.Communication.TransferModel;
using TPUM.Logic;
using TPUM.Server.Logic;

namespace TPUM.Server.GUI
{
    public class Program
    {
        public static async Task Main()
        {
            WebSocketServer server = new WebSocketServer();
            Uri address = new Uri($@"http://localhost:{8081}/");
            await server.Start(address, OnConnection, new PeriodicTask<string>(TimeSpan.FromSeconds(5), () => $"Logging...{Environment.NewLine}"));

            do
            {
                _shouldExit = Console.ReadLine()?.ToLower() == "exit";
            } while (!_shouldExit);

            foreach ((TransferUser user, WebSocketConnection connection) value in ServerProcessing.LoggedInUsers.Values)
            {
                await value.connection.DisconnectAsync();
            }
        }

        private static bool _shouldExit;

        private static void OnConnection(WebSocketConnection connection)
        {
            connection.OnMessage = async s =>
                {
                    string response = ServerProcessing.ProcessData(s, connection);
                    if (response != null)
                    {
                        Console.WriteLine("Sending response");
                        await connection.SendAsync(response);
                    }
                };
            connection.OnError = () =>
            {
                Console.WriteLine("Error occured");
                _shouldExit = true;
            };
            connection.OnClose = () => Console.WriteLine("Closing");
        }
    }
}
