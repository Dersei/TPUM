namespace TPUM.Communication
{
    public enum InterchangeStatus
    {
        Success,
        Fail
    }
    public class Interchange
    {
        public InterchangeStatus Status { get; set; }
    }
}
