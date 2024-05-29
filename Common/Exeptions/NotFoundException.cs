using Common.Enums;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException()
            : base(ApiResultStatusCode.NotFound, HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message)
            : base(ApiResultStatusCode.NotFound, HttpStatusCode.NotFound, message)
        {
        }

        public NotFoundException(object additionalData)
            : base(ApiResultStatusCode.NotFound, HttpStatusCode.NotFound, additionalData)
        {
        }

        public NotFoundException(string message, object additionalData)
            : base(ApiResultStatusCode.NotFound, HttpStatusCode.NotFound, message, additionalData)
        {
        }

        public NotFoundException(string message, Exception exception)
            : base(ApiResultStatusCode.NotFound, HttpStatusCode.NotFound, message, exception)
        {
        }

        public NotFoundException(object additionalData, Exception exception)
            : base(ApiResultStatusCode.NotFound, HttpStatusCode.NotFound, additionalData, exception)
        {
        }

        public NotFoundException(string message, Exception exception, object additionalData)
            : base(ApiResultStatusCode.NotFound, HttpStatusCode.NotFound, message, exception, additionalData)
        {
        }
    }
}
