using System.Collections.Generic;
using TPUM.Communication.TransferModel;
using TPUM.Data.Model;

namespace TPUM.Logic.Mapping
{
    public static class MappingFromTransfer
    {
        public static Publisher MapPublisher(TransferPublisher publisher)
        {
            return new Publisher(publisher.Name, publisher.Country);
        }

        public static Publisher ToPublisher(this TransferPublisher publisher) => MapPublisher(publisher);
        
        public static Game MapGame(TransferGame game)
        {
            return new Game(game.Title, game.Publisher.ToPublisher(), game.Rating, game.Premiere, (Data.Model.Genre)game.Genres);
        }

        //public static TPUM.Data.Model.Genre[]? ToGenres(TPUM.Communication.TransferModel.Genre[]? genres)
        //{
        //    if (genres is null) return null;
        //    TPUM.Data.Model.Genre[] targets = new TPUM.Data.Model.Genre[genres.Length];
        //    for (int i = 0; i < targets.Length; i++)
        //    {
        //        targets[i] = (TPUM.Data.Model.Genre)genres[i];
        //    }

        //    return targets;
        //}

        public static Game ToGame(this TransferGame game) => MapGame(game);

        public static IEnumerable<Game> MapGameCollection(IEnumerable<TransferGame> games)
        {
            foreach (TransferGame game in games)
            {
                yield return game.ToGame();
            }
        }
        
        public static IEnumerable<User> MapUserCollection(IEnumerable<TransferUser> users)
        {
            foreach (TransferUser user in users)
            {
                yield return user.ToUser();
            }
        }

        public static IEnumerable<Publisher> MapPublisherCollection(IEnumerable<TransferPublisher> publishers)
        {
            foreach (TransferPublisher publisher in publishers)
            {
                yield return publisher.ToPublisher();
            }
        }

        public static IEnumerable<Game> ToGames(this IEnumerable<TransferGame> games) => MapGameCollection(games);

        public static User MapUser(TransferUser user)
        {
            return new User(user.Username, user.Password);
        }

        public static User ToUser(this TransferUser user) => MapUser(user);
        
        public static IEnumerable<User> ToUsers(this IEnumerable<TransferUser> users) => MapUserCollection(users);
        public static IEnumerable<Publisher> ToPublishers(this IEnumerable<TransferPublisher> publishers) => MapPublisherCollection(publishers);

    }
}