using System.Linq;
using TPUM.Logic.Systems;
using Xunit;

namespace TPUM.Logic.Tests
{
    public class PublishersSystemTest
    {
        [Fact]
        public void GetGameTest()
        {
            PublishersSystem publisherSystem = new PublishersSystem();
            Assert.True(publisherSystem.GetAllPublishers().Any());
        }
    }
}