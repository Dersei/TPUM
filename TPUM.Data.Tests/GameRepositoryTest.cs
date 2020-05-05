using System;
using TPUM.Data.Model;
using TPUM.Data.Repositories;
using Xunit;

namespace TPUM.Data.Tests
{
    public class GameRepositoryTest
    {
        private readonly GameRepository _gameRepository;

        public GameRepositoryTest()
        {
            DataContext dataContext = new DataContext();
            _gameRepository = new GameRepository(dataContext);
        }

        [Fact]
        public void AddGame()
        {
            Assert.True(_gameRepository.Add(new Game("Life is Strange", new Publisher("Dontnod", "F"), 10, new DateTime(2015, 1, 30),
                new[] { Genre.Adventure })));
        }

        [Fact]
        public void FindGame()
        {
            Game game = new Game("Life is Strange", new Publisher("Dontnod", "F"), 10, new DateTime(2015, 1, 30),
                new[] {Genre.Adventure});
            _gameRepository.Add(game);
            Assert.True(_gameRepository.Get(game.ID) != null);
        }

        [Fact]
        public void RemoveGame()
        {
            Game game = new Game("Life is Strange", new Publisher("Dontnod", "F"), 10, new DateTime(2015, 1, 30),
                new[] {Genre.Adventure});
            _gameRepository.Add(game);
            Assert.True(_gameRepository.Remove(game.ID));
        }

        [Fact]
        public void UpdateGame()
        {
            Game game = new Game("Life is Strange", new Publisher("Dontnod", "F"), 10, new DateTime(2015, 1, 30),
                new[] { Genre.Adventure });
            Game game2 = new Game("Dragon Age 2", new Publisher("Bioware", "CA"), 10, new DateTime(2011, 3, 8),
                new[] { Genre.RPG });
            _gameRepository.Add(game);
            Assert.True(_gameRepository.Update(game.ID, game2));
            Game game3 = _gameRepository.Get(game.ID);
            Assert.True(game.Equals(game3));
        }
    }
}