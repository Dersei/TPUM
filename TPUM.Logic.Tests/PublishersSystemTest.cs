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
            PublishersSystem publisherSystem = PublishersSystem.Instance;
            Assert.True(publisherSystem.GetAllPublishers().Any());
        }
    }
}