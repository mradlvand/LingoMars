using Common.Enums;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class UnauthorizedException : AppException
    {
        public UnauthorizedException()
            : base(ApiResultStatusCode.Unauthorized, HttpStatusCode.Unauthorized)
        {
        }

        public UnauthorizedException(string message)
            : base(ApiResultStatusCode.Unauthorized, HttpStatusCode.Unauthorized, message)
        {
        }

        public UnauthorizedException(string message, ApiResultStatusCode apiResultStatusCode)
            : base(apiResultStatusCode, HttpStatusCode.Unauthorized, message)
        {
        }

        public UnauthorizedException(object additionalData)
            : base(ApiResultStatusCode.Unauthorized, HttpStatusCode.Unauthorized, additionalData)
        {
        }

        public UnauthorizedException(string message, object additionalData)
            : base(ApiResultStatusCode.Unauthorized, HttpStatusCode.Unauthorized, message, additionalData)
        {
        }

        public UnauthorizedException(string message, Exception exception)
            : base(ApiResultStatusCode.Unauthorized, HttpStatusCode.Unauthorized, message, exception)
        {
        }

        public UnauthorizedException(object additionalData, Exception exception)
            : base(ApiResultStatusCode.Unauthorized, HttpStatusCode.Unauthorized, additionalData, exception)
        {
        }

        public UnauthorizedException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.Unauthorized, HttpStatusCode.Unauthorized, message, exception, additionalData)
        {
        }
    }
}
