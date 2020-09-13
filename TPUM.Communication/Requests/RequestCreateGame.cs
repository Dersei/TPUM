using TPUM.Communication.TransferModel;

namespace TPUM.Communication.Requests
{
    public class RequestCreateGame : Interchange
    {
        public TransferGame? Game { get; set; }
    }
}