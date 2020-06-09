using System.Collections.Generic;

namespace TPUM.Communication.Responses
{
    public class ResponseLoggedInUsers : Response
    {
        public List<string>? LoggedInUsers { get; set; }
    }
}