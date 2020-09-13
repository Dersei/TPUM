using System.Collections.Generic;
using TPUM.Communication.TransferModel;

namespace TPUM.Communication.Responses
{
    public class ResponseGetAllGames : Response
    {
        public List<TransferGame>? Games { get; set; }
    }
}