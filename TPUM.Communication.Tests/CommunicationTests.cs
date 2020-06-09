using System;
using TPUM.Communication.DTO;
using TPUM.Data.Model;
using Xunit;

namespace TPUM.Communication.Tests
{
    public class CommunicationTests
    {
        [Fact]
        public void SessionTokenTest()
        {
            SessionToken token = new SessionToken
            {
                Value = 0
            };

            SessionToken token2 = new SessionToken
            {
                Value = 0
            };

            Assert.Equal(token2, token);
        }

        [Fact]
        public void GameDTOTest()
        {
            GameDTO game = new GameDTO(Guid.Empty, "TGame", new PublisherDTO(Guid.Empty, "T", "US"), 10, DateTime.Now,
                new[]
                {
                    Genre.FPS
                });

            Assert.True(game.ToString().Contains(game.Title) && game.ToString().Contains("Publisher"));
        }

        [Fact]
        public void PublisherDTOTest()
        {
            PublisherDTO publisher = new PublisherDTO(Guid.Empty, "T", "US");

            Assert.Equal("T - US", publisher.ToString());
        }
    }
}
