using System.Linq;
using TPUM.Data.Model;
using TPUM.Data.Repositories;
using Xunit;

namespace TPUM.Data.Tests
{
    public class PublisherRepositoryTest
    {
        private readonly PublisherRepository _publisherRepository;

        public PublisherRepositoryTest()
        {
            DataContext dataContext = DataContext.Instance;
            _publisherRepository = new PublisherRepository(dataContext);
        }

        [Fact]
        public void AddPublisher()
        {
            Assert.True(_publisherRepository.Add(new Publisher("Dontnod", "FR")));
        }

        [Fact]
        public void FindPublisher()
        {
            Publisher publisher = new Publisher("Dontnod", "FR");
            _publisherRepository.Add(publisher);
            Assert.True(_publisherRepository.GetAll() != null);
        }

        [Fact]
        public void RemovePublisher()
        {
            Publisher publisher = new Publisher("Dontnod", "FR");
            _publisherRepository.Add(publisher);
            Assert.True(_publisherRepository.Remove(publisher.ID));
        }

        [Fact]
        public void UpdatePublisher()
        {
            Publisher publisher = new Publisher("Dontnod", "FR");
            _publisherRepository.Add(publisher);
            Assert.True(_publisherRepository.Exists(publisher));
            Publisher publisher3 = _publisherRepository.GetAll().First();
            Assert.True(publisher.Equals(publisher3));
        }
    }
}