using Common.Enums;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ServerException : AppException
    {
        public ServerException()
            : base(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError)
        {
        }

        public ServerException(string message)
            : base(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, message)
        {
        }

        public ServerException(object additionalData)
            : base(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, additionalData)
        {
        }

        public ServerException(string message, object additionalData)
            : base(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, message, additionalData)
        {
        }

        public ServerException(string message, Exception exception)
            : base(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, message, exception)
        {
        }

        public ServerException(object additionalData, Exception exception)
            : base(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, additionalData, exception)
        {
        }

        public ServerException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.InternalServerError, HttpStatusCode.InternalServerError, message, exception, additionalData)
        {
        }
    }
}
