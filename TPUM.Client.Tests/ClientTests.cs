using System.Threading.Tasks;
using TPUM.Client.Logic;
using Xunit;

namespace TPUM.Client.Tests
{
    public class ClientTests
    {
        [Fact]
        public void ClientLogicGetOtherUsersTest()
        {
           ClientLogic clientLogic = ClientLogic.Instance;
           Task result = clientLogic.GetOtherUsers();
           Assert.True(result.IsCompleted);
        }

        [Fact]
        public void ClientLogicLogOutTest()
        {
            ClientLogic clientLogic = ClientLogic.Instance;
            Task result = clientLogic.Logout(default);
            Assert.True(result.IsCompleted);
        }
    }
}
