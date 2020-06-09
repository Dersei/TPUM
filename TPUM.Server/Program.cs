using System;
using System.Threading.Tasks;
using TPUM.Communication;
using TPUM.Communication.DTO;

namespace TPUM.Server
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
                });
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
