namespace LoginSys.Server
{
    public class ErorrHandler
    {
        public ErorrHandler(int code, string error, string message)
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
