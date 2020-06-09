using System;
using System.Collections.Generic;
using System.Linq;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;

namespace TPUM.Data.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private readonly DataContext _dataContext;

        public GameRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(Game game)
        {
            _dataContext.Games.Add(game);
            return true;
        }

        public bool Remove(Game game)
        {
            return _dataContext.Games.Remove(game);
        }

        public bool Remove(Guid id)
        {
            return _dataContext.Games.RemoveAll(g => g.ID.Equals(id)) > 0;
        }

        public Game Get(Guid id)
        {
            return _dataContext.Games.Find(g => g.ID.Equals(id));
        }

        public bool Get(Guid id, out Game game)
        {
            game = _dataContext.Games.Find(g => g.ID.Equals(id));
            return game != null;
        }

        public IEnumerable<Game> GetAll()
        {
            return _dataContext.Games;
        }

        public IEnumerable<Game> GetAll(Func<Game, bool> filter)
        {
            return _dataContext.Games.Where(filter);
        }

        public bool Update(Guid id, Game newGame)
        {
            Game game = _dataContext.Games.FirstOrDefault(c => c.ID.Equals(id));
            if (game is null) return false;
            game.Title = newGame.Title;
            game.Publisher = newGame.Publisher;
            game.Rating = newGame.Rating;
            game.Premiere = newGame.Premiere;
            game.Genres = newGame.Genres;
            return true;
        }

        public bool Exists(Guid id)
        {
            return _dataContext.Games.Any(u => u.ID == id);
        }

        public bool Exists(Game game)
        {
            return _dataContext.Games.Contains(game);
        }
    }
}