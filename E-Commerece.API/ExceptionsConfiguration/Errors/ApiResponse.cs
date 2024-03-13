using Microsoft.AspNetCore.Http;

namespace E_Commerece.API.ExceptionsConfiguration.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string statusMessage = null)
        {
            StatusCode = statusCode;
            StatusMessage = statusMessage ?? GetDefaultStatusCodeMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string StatusMessage { get; set; } = string.Empty;


        private string GetDefaultStatusCodeMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => $"A Bad Rquestest",
                401 => $"You aren't Authorized",
                404 => $"Resource Isn't Found",
                500 => $"Internal Server Error.The Positive Thing itsn't your fault! YaY You But not working anyway!",

                _ => null

            };
        }

    }
}
