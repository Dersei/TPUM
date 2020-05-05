using System;
using TPUM.Data.Model;
using TPUM.Logic.DTO;
using TPUM.Logic.Mapping;
using Xunit;

namespace TPUM.Logic.Tests
{
    public class MappingTest
    {
        [Fact]
        public void GameMapTest()
        {
            Game game = new Game("Life is Strange", new Publisher("Dontnod", "FR"), 10, new DateTime(2015, 1, 30),
                new[] {Genre.Adventure});
            GameDTO gameDTO = game.ToGameDTO();
            Assert.True(game.Title == gameDTO.Title);
            Assert.True(game.ID == gameDTO.ID);
        }

        [Fact]
        public void PublisherMapTest()
        {
            Publisher publisher = new Publisher("Dontnod", "FR");
            PublisherDTO publisherDTO = publisher.ToPublisherDTO();
            Assert.True(publisher.ID == publisherDTO.ID);
        }

        [Fact]
        public void PublisherDTOMapTest()
        {
            PublisherDTO publisherDTO = new PublisherDTO(Guid.Empty, "Dontnod", "FR");
            Publisher publisher = publisherDTO.ToPublisher();
            Assert.True(publisher.Name == publisherDTO.Name);
            Assert.True(publisher.ID != Guid.Empty);
        }
    }
}