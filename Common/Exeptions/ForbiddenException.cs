using Common.Enums;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ForbiddenException : AppException
    {
        public ForbiddenException()
            : base(ApiResultStatusCode.Forbidden, HttpStatusCode.Forbidden)
        {
        }

        public ForbiddenException(string message)
            : base(ApiResultStatusCode.Forbidden, HttpStatusCode.Forbidden, message)
        {
        }

        public ForbiddenException(object additionalData)
            : base(ApiResultStatusCode.Forbidden, HttpStatusCode.Forbidden, additionalData)
        {
        }

        public ForbiddenException(string message, object additionalData)
            : base(ApiResultStatusCode.Forbidden, HttpStatusCode.Forbidden, message, additionalData)
        {
        }

        public ForbiddenException(string message, Exception exception)
            : base(ApiResultStatusCode.Forbidden, HttpStatusCode.Forbidden, message, exception)
        {
        }

        public ForbiddenException(object additionalData, Exception exception)
            : base(ApiResultStatusCode.Forbidden, HttpStatusCode.Forbidden, additionalData, exception)
        {
        }

        public ForbiddenException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.Forbidden, HttpStatusCode.Forbidden, message, exception, additionalData)
        {
        }
    }
}
