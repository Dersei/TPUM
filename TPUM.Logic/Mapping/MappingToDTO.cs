using System.Collections.Generic;
using System.Linq;
using TPUM.Communication.DTO;
using TPUM.Data.Model;

namespace TPUM.Logic.Mapping
{
    public static class MappingToDTO
    {
        public static PublisherDTO MapPublisher(Publisher publisher)
        {
            return new PublisherDTO(publisher.ID, publisher.Name, publisher.Country);
        }

        public static PublisherDTO ToPublisherDTO(this Publisher publisher) => MapPublisher(publisher);
        
        public static GameDTO MapGame(Game game)
        {
            return new GameDTO(game.ID, game.Title, game.Publisher.ToPublisherDTO(), game.Rating, game.Premiere, game.Genres);
        }

        public static GameDTO ToGameDTO(this Game game) => MapGame(game);

        public static IEnumerable<GameDTO> MapGameCollection(IEnumerable<Game> games)
        {
            foreach (Game game in games)
            {
                yield return game.ToGameDTO();
            }
        }
        
        public static IEnumerable<UserDTO> MapUserCollection(IEnumerable<User> users)
        {
            foreach (User user in users)
            {
                yield return user.ToUserDTO();
            }
        }

        public static IEnumerable<PublisherDTO> MapPublisherCollection(IEnumerable<Publisher> publishers)
        {
            foreach (Publisher publisher in publishers)
            {
                yield return publisher.ToPublisherDTO();
            }
        }

        public static IEnumerable<GameDTO> ToGameDTOs(this IEnumerable<Game> games) => MapGameCollection(games);

        public static UserDTO MapUser(User user)
        {
            return new UserDTO(user.ID, user.Username, user.Password, MapGameCollection(user.FavouriteGames).ToList());
        }

        public static UserDTO ToUserDTO(this User user) => MapUser(user);
        
        public static IEnumerable<UserDTO> ToUserDTOs(this IEnumerable<User> users) => MapUserCollection(users);
        public static IEnumerable<PublisherDTO> ToPublisherDTOs(this IEnumerable<Publisher> publishers) => MapPublisherCollection(publishers);

    }
}