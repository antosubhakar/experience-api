using System;

namespace Doctrina.ExperienceApi.Server.Exceptions
{
    public class BadRequestException : ServerException
    {
        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException()
        {
        }

        public BadRequestException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
