using System.Collections.Generic;
using System.Linq;
using TPUM.Communication.DTO;
using TPUM.Data.Model;

namespace TPUM.Logic.Mapping
{
    public static class MappingFromDTO
    {
        public static Publisher MapPublisherDTO(PublisherDTO publisher)
        {
            return new Publisher(publisher.Name, publisher.Country);
        }
        
        public static Game MapGameDTO(GameDTO game)
        {
            return new Game(game.Title, MapPublisherDTO(game.Publisher), game.Rating, game.Premiere, game.Genres);
        }



        public static Publisher ToPublisher(this PublisherDTO publisher) => MapPublisherDTO(publisher);

        public static Game ToGame(this GameDTO game) => MapGameDTO(game);

        public static IEnumerable<Game> MapGameCollection(IEnumerable<GameDTO> games)
        {
            if(games is null) yield break;
            foreach (GameDTO game in games)
            {
                yield return game.ToGame();
            }
        }

        public static IEnumerable<User> MapUserCollection(IEnumerable<UserDTO> users)
        {
            if (users is null) yield break;
            foreach (UserDTO user in users)
            {
                yield return user.ToUser();
            }
        }

        public static IEnumerable<Game> ToGames(this IEnumerable<GameDTO> games) => MapGameCollection(games);

        public static User MapUser(UserDTO user)
        {
            return new User(user.Username, user.Password);
        }

        public static User ToUser(this UserDTO user) => MapUser(user);

        public static IEnumerable<User> ToUsers(this IEnumerable<UserDTO> users) => MapUserCollection(users);
    }
}