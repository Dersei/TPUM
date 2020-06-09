using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TPUM.Communication;
using TPUM.Serialization;
using TPUM.Server.Logic;

namespace TPUM.Server
{
    internal class Program
    {
        private static IServerLogic _serverLogic;
        private static async Task Main()
        {
            _serverLogic = new ServerLogic();
            bool exit = false;
            Dictionary<int, WebSocketConnection> allConnections = new Dictionary<int, WebSocketConnection>();

            Task webSocketServer = WebSocketServer.Server(8081,
                connection =>
                {
                    connection.onMessage = async s =>
                    {
                        
                        string response = ProcessData(s);
                        if (response != null)
                        {
                            Console.WriteLine("Sending response");
                            await connection.SendAsync(response);
                        }
                    };
                    connection.onError = () =>
                    {
                        Console.WriteLine("Error occured");
                        exit = true;
                    };
                    connection.onClose = () => Console.WriteLine("Closing");
                });
            do
            {
                string? command = Console.ReadLine();
                if (command?.ToLower() == "exit")
                    exit = true;
            } while (!exit);
            foreach (WebSocketConnection connection in allConnections.Values)
            {
                await connection.DisconnectAsync();
            }
        }

        private static string ProcessData(string data)
        {
            Interchange? interchange = Serializer.Deserialize<Interchange>(data);
            if (interchange is null)
            {
                Console.WriteLine($"[{ DateTime.Now:HH: mm: ss.fff}] Nieprawidłowe zapytanie ");
                return GetStatusFail();
            }
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Serwer otrzymał zapytanie od klienta, status: {interchange.Status}");

            return interchange switch
            {
                RequestCreateGame rcg => ProcessRequestCreateGame(rcg),
                _ => GetStatusFail(),
            };
        }

        private static string ProcessRequestCreateGame(RequestCreateGame request)
        {
            bool result = _serverLogic?.CreateGame(request.Game) ?? false;
            Console.WriteLine(request.Game);
            return Serializer.Serialize(new Response { Message = "Game adding", Success = result });
        }

        private static string GetStatusFail()
        {
            return Serializer.Serialize(new Interchange { Status = InterchangeStatus.Fail });
        }
    }
}
