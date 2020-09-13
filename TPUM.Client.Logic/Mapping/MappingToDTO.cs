using System.Collections.Generic;
using TPUM.Client.Logic.DTO;
using TPUM.Communication.TransferModel;
using Genre = TPUM.Client.Logic.DTO.Genre;

namespace TPUM.Client.Logic.Mapping
{
    public static class MappingToDTO
    {
        public static PublisherDTO MapPublisher(TransferPublisher publisher)
        {
            return new PublisherDTO(publisher.Name, publisher.Country);
        }

        public static PublisherDTO ToPublisherDTO(this TransferPublisher publisher) => MapPublisher(publisher);
        
        public static GameDTO MapGame(TransferGame game)
        {
            return new GameDTO(game.Title, game.Publisher.ToPublisherDTO(), game.Rating, game.Premiere, (Genre)game.Genres);
        }

        //public static Genre[]? ToDTOGenres(TPUM.Communication.TransferModel.Genre[]? genres)
        //{
        //    if (genres is null) return null;
        //    Genre[] targets = new Genre[genres.Length];
        //    for (int i = 0; i < targets.Length; i++)
        //    {
        //        targets[i] = (Genre)genres[i];
        //    }

        //    return targets;
        //}

        public static GameDTO ToGameDTO(this TransferGame game) => MapGame(game);

        public static IEnumerable<GameDTO> MapGameCollection(IEnumerable<TransferGame> games)
        {
            foreach (TransferGame game in games)
            {
                yield return game.ToGameDTO();
            }
        }
        
        public static IEnumerable<UserDTO> MapUserCollection(IEnumerable<TransferUser> users)
        {
            foreach (TransferUser user in users)
            {
                yield return user.ToUserDTO();
            }
        }

        public static IEnumerable<PublisherDTO> MapPublisherCollection(IEnumerable<TransferPublisher> publishers)
        {
            foreach (TransferPublisher publisher in publishers)
            {
                yield return publisher.ToPublisherDTO();
            }
        }

        public static IEnumerable<GameDTO> ToGameDTOs(this IEnumerable<TransferGame> games) => MapGameCollection(games);

        public static UserDTO MapUser(TransferUser user)
        {
            return new UserDTO(user.ID, user.Username, user.Password);
        }

        public static UserDTO ToUserDTO(this TransferUser user) => MapUser(user);
        
        public static IEnumerable<UserDTO> ToUserDTOs(this IEnumerable<TransferUser> users) => MapUserCollection(users);
        public static IEnumerable<PublisherDTO> ToPublisherDTOs(this IEnumerable<TransferPublisher> publishers) => MapPublisherCollection(publishers);

    }
}