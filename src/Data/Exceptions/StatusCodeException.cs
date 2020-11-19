using System;
using System.Runtime.Serialization;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class StatusCodeException : Exception
    {
        public StatusCodeException(int statusCode)
        {
            StatusCode = statusCode;
        }

        public StatusCodeException(int statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public StatusCodeException(int statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected StatusCodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public int StatusCode { get; }
    }
}
