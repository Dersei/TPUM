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
        private readonly object _lockObject = new object();

        public GameRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(Game game)
        {
            lock (_lockObject)
            {
                _dataContext.Games.Add(game);
            }
            
            return true;
        }

        public bool Remove(int id)
        {
            lock (_lockObject)
            {
                Game? game = _dataContext.Games.FirstOrDefault(u => u.ID == id);
                return _dataContext.Games.Remove(game);
            }
        }


        public IEnumerable<Game> GetAll()
        {
            return _dataContext.Games;
        }

        public IEnumerable<Game> GetAll(Func<Game, bool> filter)
        {
            return _dataContext.Games.Where(filter);
        }

        
        public bool Exists(Game game)
        {
            return _dataContext.Games.Contains(game);
        }
    }
}