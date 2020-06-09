using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TPUM.Communication;
using TPUM.Communication.Requests;
using TPUM.Communication.Responses;
using TPUM.Serialization;
using TPUM.Server.Logic;

namespace TPUM.Server
{
    internal class Program
    {
        private static readonly IServerLogic ServerLogic = new ServerLogic();
        private static async Task Main()
        {
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
                RequestLogIn rli => ProcessRequestLogIn(rli),
                _ => GetStatusFail(),
            };
        }

        private static string ProcessRequestCreateGame(RequestCreateGame request)
        {
            bool result = ServerLogic?.CreateGame(request.Game) ?? false;
            Console.WriteLine(request.Game);
            return Serializer.Serialize(new ResponseCreateGame { Message = "Game adding", CreatedGame = request.Game, Success = result });
        }

        private static string ProcessRequestLogIn(RequestLogIn request)
        {
            bool result = ServerLogic?.Login(request.Credentials) ?? false;
            Console.WriteLine(request.Credentials);
            return Serializer.Serialize(new ResponseLogIn { Message = "User logged in", Success = result });
        }

        private static string GetStatusFail()
        {
            return Serializer.Serialize(new Interchange { Status = InterchangeStatus.Fail });
        }
    }
}
