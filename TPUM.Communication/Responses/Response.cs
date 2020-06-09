namespace TPUM.Communication.Responses
{
    public class Response : Interchange
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}