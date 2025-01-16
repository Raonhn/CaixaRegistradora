namespace CaixaRegistradora.Errors{
    public class HttpException:Exception{
        public int Code { get; set; }
        override public string Message { get; }

        public List<string>? Error { get; set; }

        public HttpException(int code, string message, List<string>? error) : base(message){
            Code = code;
            Message = message;
            Error = error;
        }
    }
}