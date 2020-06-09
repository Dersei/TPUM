using System;
using System.Threading.Tasks;
using TPUM.Communication;
using TPUM.Communication.DTO;
using TPUM.Logic;
using TPUM.Server.Logic;

namespace TPUM.Server.GUI
{
    internal class Program
    {
        private static async Task Main()
        {
            bool exit = false;

            await WebSocketServer.Server(8081,
                connection =>
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
                        exit = true;
                    };
                    connection.OnClose = () => Console.WriteLine("Closing");
                }, new PeriodicTask<string>(TimeSpan.FromSeconds(5), () => $"Logging...{Environment.NewLine}")
                );
            do
            {
                string? command = Console.ReadLine();
                if (command?.ToLower() == "exit")
                    exit = true;
            } while (!exit);
            foreach ((UserDTO user, WebSocketConnection connection) value in ServerProcessing.LoggedInUsers.Values)
            {
                await value.connection.DisconnectAsync();
            }
        }


    }
}
