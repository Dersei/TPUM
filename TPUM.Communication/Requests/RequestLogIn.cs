using TPUM.Communication.DTO;

namespace TPUM.Communication.Requests
{
    public class RequestLogIn : Interchange
    {
        public UserDTO? Credentials { get; set; }
    }
}