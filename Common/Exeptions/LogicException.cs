using Common.Enums;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class LogicException : AppException
    {
        public LogicException()
            : base(ApiResultStatusCode.LogicError, HttpStatusCode.InternalServerError)
        {
        }

        public LogicException(string message)
            : base(ApiResultStatusCode.LogicError, HttpStatusCode.InternalServerError, message)
        {
        }

        public LogicException(object additionalData)
            : base(ApiResultStatusCode.LogicError, HttpStatusCode.InternalServerError, additionalData)
        {
        }

        public LogicException(string message, object additionalData)
            : base(ApiResultStatusCode.LogicError, HttpStatusCode.InternalServerError, message, additionalData)
        {
        }

        public LogicException(string message, Exception exception)
            : base(ApiResultStatusCode.LogicError, HttpStatusCode.InternalServerError, message, exception)
        {
        }

        public LogicException(object additionalData, Exception exception)
            : base(ApiResultStatusCode.LogicError, HttpStatusCode.InternalServerError, additionalData, exception)
        {
        }

        public LogicException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.LogicError, HttpStatusCode.InternalServerError, message, exception, additionalData)
        {
        }
    }
}
