using Graft.Infrastructure.Models;
using System;

namespace Graft.Infrastructure
{
    public class ApiException : Exception
    {
        public ApiError Error { get; private set; }
        public override string Message => Error.Message;

        public ApiException(ErrorCode errorCode, object param = null) 
        {
            Error = new ApiError(errorCode, param);
        }

        public ApiException(ApiError apiError)
        {
            Error = apiError;
        }
    }
}
