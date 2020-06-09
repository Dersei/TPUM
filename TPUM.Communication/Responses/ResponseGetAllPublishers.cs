using System.Collections.Generic;
using TPUM.Communication.DTO;

namespace TPUM.Communication.Responses
{
    public class ResponseGetAllPublishers : Response
    {
        public List<PublisherDTO>? Publishers { get; set; }
    }
}