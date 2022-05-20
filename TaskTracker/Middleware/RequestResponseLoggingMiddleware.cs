using Microsoft.IO;

namespace TaskTracker.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly Random _random;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            _random = new Random();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            var reqNum = _random.Next();

            await LogRequest(reqNum, context);
            await LogResponse(reqNum, context);
        }

        private async Task LogRequest(int reqNum, HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            if (context.Request.Path == "/api/nomenclature/product")
            {
                _logger.LogInformation($"{reqNum} -> " +
                                       $"Path: {context.Request.Path} | " +
                                       $"Query: {context.Request.QueryString}");
            }
            else if (context.Request.Path != "/api/terminal/orders/get")
            {
                _logger.LogInformation($"{reqNum} -> " +
                                       $"Path: {context.Request.Path} | " +
                                       $"Query: {context.Request.QueryString} | " +
                                       $"Request: {await ReadStream(requestStream)}");
            }

            context.Request.Body.Position = 0;
        }

        private static async Task<string> ReadStream(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }

        private async Task LogResponse(int reqNum, HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();

            context.Response.Body.Seek(0, SeekOrigin.Begin);

            if (context.Request.Path != "/api/terminal/orders/get")
            {
                _logger.LogInformation($"{reqNum} <- " +
                                       $"Path: {context.Request.Path} | " +
                                       $"Query: {context.Request.QueryString} | " +
                                       $"Response: {text}");
            }

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
