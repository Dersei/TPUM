using TPUM.Communication.DTO;

namespace TPUM.Communication
{
    public class RequestLogIn : Interchange
    {
        public UserDTO Credentials { get; set; }
    }
}