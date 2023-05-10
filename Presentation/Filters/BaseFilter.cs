using System;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Presentation.Filters
{
    public abstract class BaseFilter : ResultFilterAttribute
    {
        private readonly ILogger _logger;

        public BaseFilter(ILogger logger)
        {
            _logger = logger;
        }

        public void TrackException(HttpContext context,
                                   Exception ex)
        {
            _logger.LogError(ex?.HResult ?? 0, ex, "Unhandled exception. trace_id {TraceIdentifier}", context.TraceIdentifier);
        }

        public ObjectResult ExceptionResult(HttpContext context,
                                            Exception ex)
        {
            return new(new ServerError
            {
                Trace = ex.StackTrace,
                Message = ex.Message,
                TraceId = context.TraceIdentifier
            })
            {
                StatusCode = 500,
                DeclaredType = typeof(ServerError)
            };
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);

            if (!context.ModelState.IsValid)
            {
                var errors = new List<ApplicationError>();
                foreach (var modelStateValue in context.ModelState.Values)
                {
                    var entry = modelStateValue;
                    var message = entry.Errors.FirstOrDefault();
                    if (message != null)
                    {
                        errors.Add(new ApplicationError
                        {
                            ErrorMessage = message.ErrorMessage
                        });
                    }
                }

                if (errors.Any())
                {
                    context.Result = new ObjectResult(errors)
                    {
                        StatusCode = 400,
                        DeclaredType = typeof(ApplicationError[])
                    };
                }
            }
        }
    }
}