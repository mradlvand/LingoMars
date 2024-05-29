using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public ApiResultStatusCode ApiStatusCode { get; set; }
        public object AdditionalData { get; set; }

        public AppException()
            : this(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError)
        {
        }

        public AppException(ApiResultStatusCode statusCode, HttpStatusCode httpStatusCode)
            : this(statusCode, httpStatusCode, null)
        {
        }

        public AppException(string message)
            : this(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, message)
        {
        }

        public AppException(string message, object additionalData)
            : this(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, message, additionalData)
        {
        }

        public AppException(ApiResultStatusCode statusCode, HttpStatusCode httpStatusCode, object additionalData)
            : this(statusCode, httpStatusCode, null, additionalData)
        {
        }

        public AppException(ApiResultStatusCode statusCode, HttpStatusCode httpStatusCode, string message)
            : this(statusCode, httpStatusCode, message, null)
        {
        }

        public AppException(string message, Exception exception)
            : this(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, message, exception)
        {
        }

        public AppException(object additionalData, Exception exception)
            : this(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, additionalData, exception)
        {
        }

        public AppException(string message, Exception exception, object additionalData)
            : this(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, message, exception, additionalData)
        {
        }

        public AppException(ApiResultStatusCode statusCode, HttpStatusCode httpStatusCode, string message, object additionalData)
            : this(statusCode, httpStatusCode, message, null, additionalData)
        {
        }

        public AppException(ApiResultStatusCode statusCode, HttpStatusCode httpStatusCode, string message, Exception exception)
            : this(statusCode, httpStatusCode, message, exception, null)
        {
        }

        public AppException(ApiResultStatusCode statusCode, HttpStatusCode httpStatusCode, object additionalData, Exception exception)
            : this(statusCode, httpStatusCode, null, exception, additionalData)
        {
        }

        public AppException(ApiResultStatusCode statusCode, HttpStatusCode httpStatusCode, string message, Exception exception, object additionalData)
            : base(message, exception)
        {
            ApiStatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
            AdditionalData = additionalData;
        }
    }
}
