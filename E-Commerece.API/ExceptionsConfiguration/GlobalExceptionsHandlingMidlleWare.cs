using E_Commerece.API.ExceptionsConfiguration.Errors;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace E_Commerece.API.ExceptionsConfiguration
{
    public class GlobalExceptionsHandlingMidlleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionsHandlingMidlleWare> _logger;
        private readonly IHostEnvironment _environment;

        public GlobalExceptionsHandlingMidlleWare(RequestDelegate next, ILogger<GlobalExceptionsHandlingMidlleWare> logger)
                                                  
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }

        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode Status;
            var message = string.Empty;
            var stackTrace = string.Empty;

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(Exceptions.NotFoundException))
            {
                Status = HttpStatusCode.NotFound;
                message = ex.Message;
                stackTrace = ex.StackTrace;
            }

            else if (exceptionType == typeof(Exceptions.BadRequestException))
            {
                message = ex.Message;
                Status = HttpStatusCode.BadRequest;
                stackTrace = ex.StackTrace;
            }

            else if (exceptionType == typeof(Exceptions.NotImplementedException))
            {
                message = ex.Message;
                Status = HttpStatusCode.NotImplemented;
                stackTrace = ex.StackTrace;
            }
            else if (exceptionType == typeof(Exceptions.KeyNotFoundException))
            {
                message = ex.Message;
                Status = HttpStatusCode.NotFound;
                stackTrace = ex.StackTrace;
            }

            else if (exceptionType == typeof(Exceptions.UnauthorizedAccessException))
            {
                message = ex.Message;
                Status = HttpStatusCode.Unauthorized;
                stackTrace = ex.StackTrace;
            }
            else
            {
                message = ex.Message;
                Status = HttpStatusCode.InternalServerError;
                stackTrace = ex.StackTrace;
            }


            

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)Status;

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var exceptionResult = JsonSerializer.Serialize(new { error = Status ,  message, stackTrace }, jsonOptions);

            return context.Response.WriteAsync(exceptionResult);

        }


    }
}
