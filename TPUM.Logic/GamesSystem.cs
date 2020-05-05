using System;
using System.Collections.Generic;
using TPUM.Data;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;
using TPUM.Data.Repositories;
using TPUM.Logic.DTO;

namespace TPUM.Logic
{
    public class GamesSystem
    {
        private readonly IRepository<Game> _repository;
        
        public GamesSystem()
        {
            _repository  = new GameRepository(DataContext.Instance.FillData(new TestDataFiller()));
        }

        public GamesSystem(IRepository<Game> repository) => _repository = repository;

        public GameDTO GetGame(Guid id)
        {
            return _repository.Get(id).ToGameDTO();
        }

        public IEnumerable<GameDTO> GetAllGames()
        {
            return _repository.GetAll().ToGameDTOs();
        }

        public bool AddGame(GameDTO game)
        {
            return _repository.Add(MappingFromDTO.MapGameDTO(game));
        }

    }
}
