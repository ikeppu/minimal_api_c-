using System.Diagnostics;

namespace GameStore.Middleware
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            var stopWatch = new Stopwatch();
            try
            {
                stopWatch.Start();
                await _next(ctx);
            }
            finally
            {
                stopWatch.Stop();

                var elapsedMilliseconds = stopWatch.ElapsedMilliseconds;
                _logger.LogInformation($"{ctx.Request.Method} {ctx.Request.Path} request took {elapsedMilliseconds}ms to complete");
            }
        }
    }
}
