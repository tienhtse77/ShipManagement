using System.Diagnostics;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var watcher = new Stopwatch();
            var httpContext = _httpContextAccessor.HttpContext;
            try
            {
                _logger.LogInformation("Handling {MethodName}", typeof(TRequest).Name);
                _logger.LogInformation("Request {Method} {Path} Headers: {Headers}",
                    httpContext.Request.Method, httpContext.Request.Path, httpContext.Request.Headers);
                watcher.Start();
                var response = await next();
                watcher.Stop();
                _logger.LogTrace("response:{response}", JsonSerializer.Serialize(response));
                _logger.LogInformation("Handled {MethodName}, Time consumed: {processTime}ms", typeof(TRequest).Name,
                    watcher.ElapsedMilliseconds);
                return response;
            }
            catch (Exception)
            {
                _logger.LogInformation("[ERROR]Handled {MethodName}, Time consumed: {processTime}ms",
                    typeof(TRequest).Name,
                    watcher.ElapsedMilliseconds);
                throw;
            }
        }
    }
}