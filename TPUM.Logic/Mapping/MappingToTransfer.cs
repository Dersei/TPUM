using System.Collections.Generic;
using TPUM.Communication.TransferModel;
using TPUM.Data.Model;

namespace TPUM.Logic.Mapping
{
    public static class MappingToTransfer
    {
        public static TransferPublisher MapPublisher(Publisher publisher)
        {
            return new TransferPublisher(publisher.Name, publisher.Country);
        }
        
        public static TransferGame MapGame(Game game)
        {
            return new TransferGame(game.Title, MapPublisher(game.Publisher), game.Rating, game.Premiere, (uint)game.Genres);
        }

        //public static TPUM.Communication.TransferModel.Genre[]? ToTransferGenres(TPUM.Data.Model.Genre[]? genres)
        //{
        //    if (genres is null) return null;
        //    TPUM.Communication.TransferModel.Genre[] targets = new TPUM.Communication.TransferModel.Genre[genres.Length];
        //    for (int i = 0; i < targets.Length; i++)
        //    {
        //        targets[i] = (TPUM.Communication.TransferModel.Genre) genres[i];
        //    }

        //    return targets;
        //}

        public static TransferPublisher ToTransferPublisher(this Publisher publisher) => MapPublisher(publisher);

        public static TransferGame ToTransferGame(this Game game) => MapGame(game);

        public static IEnumerable<TransferGame> MapGameCollection(IEnumerable<Game> games)
        {
            if(games is null) yield break;
            foreach (Game game in games)
            {
                yield return game.ToTransferGame();
            }
        }

        public static IEnumerable<TransferUser> MapUserCollection(IEnumerable<User> users)
        {
            if (users is null) yield break;
            foreach (User user in users)
            {
                yield return user.ToTransferUser();
            }
        }

        public static IEnumerable<TransferGame> ToTransferGames(this IEnumerable<Game> games) => MapGameCollection(games);

        public static TransferUser MapUser(User user)
        {
            return new TransferUser(user.ID, user.Username, user.Password);
        }

        public static TransferUser ToTransferUser(this User user) => MapUser(user);

        public static IEnumerable<TransferUser> ToTransferUsers(this IEnumerable<User> users) => MapUserCollection(users);

        public static IEnumerable<TransferPublisher> MapPublisherCollection(IEnumerable<Publisher> publishers)
        {
            foreach (Publisher publisher in publishers)
            {
                yield return publisher.ToTransferPublisher();
            }
        }

        public static IEnumerable<TransferPublisher> ToTransferPublishers(this IEnumerable<Publisher> publishers) => MapPublisherCollection(publishers);
    }
}