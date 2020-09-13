using System;
using TPUM.Communication.TransferModel;
using TPUM.Data.Model;
using TPUM.Logic.Mapping;
using Xunit;
using Genre = TPUM.Data.Model.Genre;

namespace TPUM.Logic.Tests
{
    public class MappingTest
    {
        [Fact]
        public void GameMapTest()
        {
            Game game = new Game("Life is Strange", new Publisher("Dontnod", "FR"), 10, new DateTime(2015, 1, 30),
                new[] {Genre.Adventure});
            TransferGame gameDTO = game.ToTransferGame();
            Assert.True(game.Title == gameDTO.Title);
        }

        [Fact]
        public void PublisherMapTest()
        {
            Publisher publisher = new Publisher("Dontnod", "FR");
            TransferPublisher publisherDTO = publisher.ToTransferPublisher();
            Assert.True(publisher.Name == publisherDTO.Name);
        }

        [Fact]
        public void PublisherDTOMapTest()
        {
            TransferPublisher publisherDTO = new TransferPublisher( "Dontnod", "FR");
            Publisher publisher = publisherDTO.ToPublisher();
            Assert.True(publisher.Name == publisherDTO.Name);
            Assert.True(publisher.ID != Guid.Empty);
        }
    }
}