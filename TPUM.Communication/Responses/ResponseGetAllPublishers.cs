using System.Collections.Generic;
using TPUM.Communication.TransferModel;

namespace TPUM.Communication.Responses
{
    public class ResponseGetAllPublishers : Response
    {
        public List<TransferPublisher>? Publishers { get; set; }
    }
}