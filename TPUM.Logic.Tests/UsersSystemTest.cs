using System.Linq;
using TPUM.Logic.Systems;
using Xunit;

namespace TPUM.Logic.Tests
{
    public class UsersSystemTest
    {
        [Fact]
        public void GetGameTest()
        {
            UsersSystem usersSystem = new UsersSystem();
            Assert.True(usersSystem.GetAllUsers().Any());
        }
    }
}
