using System.Runtime.Serialization;
using TPUM.Communication.DTO;

namespace TPUM.Communication
{
    public class RequestCreateGame : Interchange
    {
        public GameDTO Game { get; set; }
    }
}