using System.Linq;
using TPUM.Logic.Systems;
using Xunit;

namespace TPUM.Logic.Tests
{
    public class GamesSystemTest
    {
        [Fact]
        public void GetGameTest()
        {
            GamesSystem gamesSystem = new GamesSystem();
            Assert.True(gamesSystem.GetAllGames().Any());
        }
    }
}
