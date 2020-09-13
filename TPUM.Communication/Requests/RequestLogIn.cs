using TPUM.Communication.TransferModel;

namespace TPUM.Communication.Requests
{
    public class RequestLogIn : Interchange
    {
        public TransferUser? Credentials { get; set; }
    }
}