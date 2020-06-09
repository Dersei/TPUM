using TPUM.Communication.DTO;

namespace TPUM.Communication.Responses
{
    public class ResponseCreateGame : Response
    {
        public GameDTO CreatedGame { get; set; }
    }
}
