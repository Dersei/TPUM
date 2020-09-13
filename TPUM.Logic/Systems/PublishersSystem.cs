using System;
using System.Collections.Generic;
using TPUM.Communication.TransferModel;
using TPUM.Data;
using TPUM.Data.Interfaces;
using TPUM.Data.Model;
using TPUM.Data.Repositories;
using TPUM.Logic.Interfaces;
using TPUM.Logic.Mapping;

namespace TPUM.Logic.Systems
{
    public class PublishersSystem : IReportable
    {
        
        private readonly IRepository<Publisher> _repository;
        private readonly object _syncObject = new object();

        public PublishersSystem()
        {
            _repository = new PublisherRepository(DataContext.Instance.FillData(new TestDataFiller()));
        }

        public PublishersSystem(IRepository<Publisher> repository)
        {
            _repository = repository;
        }

        public IEnumerable<TransferPublisher> GetAllPublishers()
        {
            lock (_syncObject)
                return _repository.GetAll().ToTransferPublishers();
        }

        public string CreateReport()
        {
            return $"{Environment.NewLine}";
        }
    }
}
