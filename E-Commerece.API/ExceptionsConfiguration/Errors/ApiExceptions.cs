namespace E_Commerece.API.ExceptionsConfiguration.Errors
{
    public class ApiExceptions : ApiResponse
    {
        public ApiExceptions(int statusCode, string statusMessage = null, string stackTrace = null) : base(statusCode, statusMessage)
        {
            StackTrace = stackTrace;
        }

        public string StackTrace { get; set; }

    }
}
