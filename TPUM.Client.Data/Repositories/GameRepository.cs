using System;
using System.Collections.Generic;
using System.Linq;
using TPUM.Client.Data.Interfaces;
using TPUM.Communication.TransferModel;

namespace TPUM.Client.Data.Repositories
{
    public class GameRepository : IRepository<TransferGame>
    {
        private readonly DataContext _dataContext;
        private readonly object _lockObject = new object();

        public GameRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool Add(TransferGame game)
        {
            lock (_lockObject)
            {
                _dataContext.Games.Add(game);
            }
            return true;
        }

        public bool Remove(TransferGame game)
        {
            lock (_lockObject)
            {
                return _dataContext.Games.Remove(game);
            }
        }


        public IEnumerable<TransferGame> GetAll()
        {
            return _dataContext.Games;
        }

        public IEnumerable<TransferGame> GetAll(Func<TransferGame, bool> filter)
        {
            return _dataContext.Games.Where(filter);
        }

        
        public bool Exists(TransferGame game)
        {
            return _dataContext.Games.Contains(game);
        }
    }
}