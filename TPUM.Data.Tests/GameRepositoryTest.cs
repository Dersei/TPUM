using System;
using System.Linq;
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
            DataContext dataContext = DataContext.Instance;
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
            Assert.True(_gameRepository.GetAll() != null);
        }

        [Fact]
        public void RemoveGame()
        {
            Game game = new Game("Life is Strange", new Publisher("Dontnod", "F"), 10, new DateTime(2015, 1, 30),
                new[] {Genre.Adventure});
            _gameRepository.Add(game);
            Assert.True(_gameRepository.Remove(game));
        }

        [Fact]
        public void UpdateGame()
        {
            Game game = new Game("Life is Strange", new Publisher("Dontnod", "F"), 10, new DateTime(2015, 1, 30),
                new[] { Genre.Adventure });
            _gameRepository.Add(game);
            Assert.True(_gameRepository.Exists(game));
            Game game3 = _gameRepository.GetAll().First();
            Assert.True(game.Equals(game3));
        }
    }
}