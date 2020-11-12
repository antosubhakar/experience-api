using System;
using System.Runtime.Serialization;

namespace Doctrina.ExperienceApi.Server.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException() { }

        public ServerException(string message)
            : base(message)
        {
        }

        public ServerException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected ServerException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
