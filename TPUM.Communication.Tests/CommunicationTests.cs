using System;
using TPUM.Communication.TransferModel;
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
            TransferGame game = new TransferGame("TGame", new TransferPublisher("T", "US"), 10, DateTime.Now,
                new[]
                {
                    Genre.FPS
                });

            Assert.True(game.ToString().Contains(game.Title) && game.ToString().Contains("Publisher"));
        }

        [Fact]
        public void PublisherDTOTest()
        {
            TransferPublisher publisher = new TransferPublisher( "T", "US");

            Assert.Equal("T - US", publisher.ToString());
        }
    }
}
