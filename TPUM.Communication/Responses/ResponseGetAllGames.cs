using System.Collections.Generic;
using TPUM.Communication.DTO;

namespace TPUM.Communication.Responses
{
    public class ResponseGetAllGames : Response
    {
        public List<GameDTO>? Games { get; set; }
    }
}