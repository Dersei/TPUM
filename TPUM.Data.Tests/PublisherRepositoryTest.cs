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
            Assert.True(_publisherRepository.Get(publisher.ID) != null);
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
            Publisher publisher2 = new Publisher("Bioware", "CA");
            _publisherRepository.Add(publisher);
            Assert.True(_publisherRepository.Update(publisher.ID, publisher2));
            Publisher publisher3 = _publisherRepository.Get(publisher.ID);
            Assert.True(publisher.Equals(publisher3));
        }
    }
}