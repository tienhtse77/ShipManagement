namespace Application.Common.Exceptions
{
    public class ServerError
    {
        public string TraceId { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
    }
}