using System.Net;
using TaskTracker.Models.Exceptions;

namespace TaskTracker.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ServerException ex)
            {
                var response = context.Response;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync(ex.Message);
            }
        }
    }
}
