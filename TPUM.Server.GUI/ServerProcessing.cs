using System;
using System.Collections.Generic;
using System.Linq;
using TPUM.Communication;
using TPUM.Communication.DTO;
using TPUM.Communication.Requests;
using TPUM.Communication.Responses;
using TPUM.Serialization;
using TPUM.Server.Logic;

namespace TPUM.Server.GUI
{
    public class ServerProcessing
    {
        private static readonly IServerLogic ServerLogic = new ServerLogic();
        public static readonly Dictionary<SessionToken, (UserDTO user, WebSocketConnection connection)> LoggedInUsers = new Dictionary<SessionToken, (UserDTO user, WebSocketConnection connection)>();
        public static string ProcessData(string data, WebSocketConnection connection)
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
                RequestLogIn rli => ProcessRequestLogIn(rli, connection),
                RequestLoggedInUsers rliu => ProcessRequestGetLoggedInUsers(rliu),
                RequestGetAllGames rgag => ProcessRequestGetAllGames(rgag),
                RequestLogOut rlo => ProcessRequestLogOut(rlo),
                RequestGetAllPublishers rgap => ProcessRequestGetAllPublishers(rgap),
                _ => GetStatusFail(),
            };
        }

        public static string ProcessRequestLogOut(RequestLogOut request)
        {
            LoggedInUsers.Remove(request.Token);
            Console.WriteLine($"Log out {request.Token}");
            return Serializer.Serialize(new ResponseLogOut() { Message = "Logged out", Success = true });
        }

        public static string ProcessRequestGetAllPublishers(RequestGetAllPublishers _)
        {
            List<PublisherDTO> publishers = ServerLogic.GetAllPublishers();
            return Serializer.Serialize(new ResponseGetAllPublishers() { Message = "All games", Publishers = publishers, Success = true });
        }

        public static string ProcessRequestGetAllGames(RequestGetAllGames _)
        {
            List<GameDTO> games = ServerLogic.GetAllGames();
            return Serializer.Serialize(new ResponseGetAllGames() { Message = "All games", Games = games, Success = true });
        }

        public static string ProcessRequestCreateGame(RequestCreateGame request)
        {
            if(request.Game is null) return Serializer.Serialize(new ResponseCreateGame { Message = "Game adding", CreatedGame = null, Success = false });
            bool result = ServerLogic?.CreateGame(request.Game) ?? false;
            Console.WriteLine(request.Game);
            return Serializer.Serialize(new ResponseCreateGame { Message = "Game adding", CreatedGame = request.Game, Success = result });
        }

        public static string ProcessRequestLogIn(RequestLogIn request, WebSocketConnection connection)
        {
            if(request.Credentials is null) return Serializer.Serialize(new ResponseLogIn { Message = "User logged in", Success = false, Token = default });
            bool result = ServerLogic?.Login(request.Credentials) ?? false;
            Console.WriteLine(request.Credentials);
            SessionToken token =
                new SessionToken(HashCode.Combine(request.Credentials.Username, request.Credentials.Password));
            LoggedInUsers.TryAdd(token, (request.Credentials, connection));
            Console.WriteLine(token);
            return Serializer.Serialize(new ResponseLogIn { Message = "User logged in", Success = result, Token = token });
        }

        public static string ProcessRequestGetLoggedInUsers(RequestLoggedInUsers _)
        {
            List<string> users = LoggedInUsers.Select(i => i.Value.user.Username).ToList();
            return Serializer.Serialize(new ResponseLoggedInUsers() { Message = "Logged in users", Success = true, LoggedInUsers = users });
        }

        public static string GetStatusFail()
        {
            return Serializer.Serialize(new Interchange { Status = InterchangeStatus.Fail });
        }
    }
}