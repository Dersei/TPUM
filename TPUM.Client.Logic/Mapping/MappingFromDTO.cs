using System.Collections.Generic;
using TPUM.Communication.DTO;
using TPUM.Communication.TransferModel;

namespace TPUM.Client.Logic.Mapping
{
    public static class MappingFromDTO
    {
        public static TransferPublisher MapPublisherDTO(PublisherDTO publisher)
        {
            return new TransferPublisher(publisher.Name, publisher.Country);
        }
        
        public static TransferGame MapGameDTO(GameDTO game)
        {
            return new TransferGame(game.Title, MapPublisherDTO(game.Publisher), game.Rating, game.Premiere, ToTransferGenres(game.Genres));
        }

        public static TPUM.Communication.TransferModel.Genre[]? ToTransferGenres(TPUM.Communication.DTO.Genre[]? genres)
        {
            if (genres is null) return null;
            TPUM.Communication.TransferModel.Genre[] targets = new TPUM.Communication.TransferModel.Genre[genres.Length];
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i] = (TPUM.Communication.TransferModel.Genre) genres[i];
            }

            return targets;
        }

        public static TransferPublisher ToPublisher(this PublisherDTO publisher) => MapPublisherDTO(publisher);

        public static TransferGame ToGame(this GameDTO game) => MapGameDTO(game);

        public static IEnumerable<TransferGame> MapGameCollection(IEnumerable<GameDTO> games)
        {
            if(games is null) yield break;
            foreach (GameDTO game in games)
            {
                yield return game.ToGame();
            }
        }

        public static IEnumerable<TransferUser> MapUserCollection(IEnumerable<UserDTO> users)
        {
            if (users is null) yield break;
            foreach (UserDTO user in users)
            {
                yield return user.ToUser();
            }
        }

        public static IEnumerable<TransferGame> ToGames(this IEnumerable<GameDTO> games) => MapGameCollection(games);

        public static TransferUser MapUser(UserDTO user)
        {
            return new TransferUser(user.ID, user.Username, user.Password);
        }

        public static TransferUser ToUser(this UserDTO user) => MapUser(user);

        public static IEnumerable<TransferUser> ToUsers(this IEnumerable<UserDTO> users) => MapUserCollection(users);
    }
}