using TPUM.Communication.DTO;

namespace TPUM.Communication.Requests
{
    public class RequestCreateGame : Interchange
    {
        public GameDTO? Game { get; set; }
    }
}