namespace TPUM.Communication.Requests
{
    public class RequestLogOut : Interchange
    {
        public SessionToken Token { get; set; }
    }
}