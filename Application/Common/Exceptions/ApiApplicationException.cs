using System;
using System.Net;

namespace Application.Common.Exceptions
{
    public class ApiApplicationException : Exception
    {
        public ApiApplicationException(HttpStatusCode httpStatusCode,
            string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; }
    }
}