using TPUM.Communication.TransferModel;

namespace TPUM.Communication.Responses
{
    public class ResponseCreateGame : Response
    {
        public TransferGame? CreatedGame { get; set; }
    }
}
