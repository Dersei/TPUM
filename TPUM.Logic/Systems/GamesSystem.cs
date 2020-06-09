using System;
using System.Collections.Generic;
using TPUM.Communication.DTO;
using TPUM.Data;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;
using TPUM.Data.Repositories;
using TPUM.Logic.Interfaces;
using TPUM.Logic.Mapping;

namespace TPUM.Logic.Systems
{
    public class GamesSystem : IReportable
    {
        private readonly IRepository<Game> _repository;
        private readonly object _syncObject = new object();
        private int _counter;

        public GamesSystem()
        {
            _repository = new GameRepository(DataContext.Instance.FillData(new TestDataFiller()));
        }

        public GamesSystem(IRepository<Game> repository) => _repository = repository;

        public GameDTO GetGame(Guid id)
        {
            lock (_syncObject)
                return _repository.Get(id).ToGameDTO();
        }

        public IEnumerable<GameDTO> GetAllGames()
        {
            lock (_syncObject)
                return _repository.GetAll().ToGameDTOs();
        }

        public bool AddGame(GameDTO game)
        {
            lock (_syncObject)
            {
                if (_repository.Add(MappingFromDTO.MapGameDTO(game)))
                {
                    _counter++;
                    return true;
                }

                return false;
            }
        }

        public string CreateReport()
        {
            lock (_syncObject)
                return $"Added {_counter} new games to database {Environment.NewLine}";
        }
    }
}
