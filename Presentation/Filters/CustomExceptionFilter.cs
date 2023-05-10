using System.Net;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Presentation.Filters
{
    public class CustomExceptionFilter : BaseFilter, IAsyncExceptionFilter
    {

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger) : base(logger)
        {
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            string errorMessage;
            var httpStatusCode = (int) HttpStatusCode.InternalServerError;

            if (context.Exception is ApiApplicationException)
            {
                var cqException = context.Exception as ApiApplicationException;
                errorMessage = cqException?.Message ?? Constants.ErrorMessages.GENERIC_ERROR;
                httpStatusCode = (int) (cqException?.HttpStatusCode ?? HttpStatusCode.InternalServerError);

                if (httpStatusCode == (int) HttpStatusCode.InternalServerError)
                {
                    TrackException(context.HttpContext, context.Exception);
                }
            }
            else
            {
                errorMessage = context.Exception?.Message;
                TrackException(context.HttpContext, context.Exception);
            }

            context.Result = new BadRequestObjectResult(new ApplicationError
            {
                ErrorMessage = errorMessage
            })
            {
                StatusCode = httpStatusCode,
                DeclaredType = typeof(ApplicationError)
            };

            return Task.CompletedTask;
        }
    }
}