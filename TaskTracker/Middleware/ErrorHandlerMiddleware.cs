using System.Net;
using TaskTracker.Models.Exceptions;

namespace TaskTracker.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ServerException ex)
            {
                //_logger.LogError(ex, ex.Message);
                var response = context.Response;
                response.ContentType = "text/plain";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync(ex.Message);
            }
            catch (ObjectNotFoundException ex)
            {
                //_logger.LogError(ex, ex.Message);
                var response = context.Response;
                response.ContentType = "text/plain";
                response.StatusCode = (int)HttpStatusCode.NotFound;
                await response.WriteAsync(ex.Message);
            }
        }
    }
}
