namespace LoginSys.Server
{
    public class ErrorHandler
    {
        public ErrorHandler(int code, string error, string message)
        {
            Code = code;
            Error = error;
            Message = message;
        }
        public int Code { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}
