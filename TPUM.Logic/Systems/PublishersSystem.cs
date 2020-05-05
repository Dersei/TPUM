using System;
using System.Collections.Generic;
using TPUM.Data;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;
using TPUM.Data.Repositories;
using TPUM.Logic.DTO;
using TPUM.Logic.Interfaces;
using TPUM.Logic.Mapping;

namespace TPUM.Logic.Systems
{
    public class PublishersSystem : IReportable
    {
        private static PublishersSystem? _instance;

        private static readonly object InstanceLock = new object();

        public static PublishersSystem Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    _instance ??= new PublishersSystem();
                }
                return _instance;
            }
        }

        private readonly IRepository<Publisher> _repository;

        private PublishersSystem()
        {
            _repository = new PublisherRepository(DataContext.Instance.FillData(new TestDataFiller()));
        }

        public PublishersSystem(IRepository<Publisher> repository)
        {
            _repository = repository;
            _instance = this;
        }

        public PublisherDTO GetGame(Guid id)
        {
            return _repository.Get(id).ToPublisherDTO();
        }

        public IEnumerable<PublisherDTO> GetAllPublishers()
        {
            return _repository.GetAll().ToPublisherDTOs();
        }

        public string CreateReport()
        {
            return $"{Environment.NewLine}";
        }
    }
}
