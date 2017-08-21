namespace ElasticsearchNet.Web
{
    public class ExceptionResponse
    {
        public ExceptionResponse(string type, string message, string stackTrace)
        {
            StackTrace = stackTrace;
            Message = message;
            Type = type;
        }

		public string Type { get; }
		public string Message { get; }
		public string StackTrace { get; }
    }
}