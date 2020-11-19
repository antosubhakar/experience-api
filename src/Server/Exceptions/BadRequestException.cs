using System.Net;
using Doctrina.ExperienceApi.Data.Exceptions;

namespace Doctrina.ExperienceApi.Server.Exceptions
{
    public class BadRequestException : StatusCodeException
    {
        public BadRequestException(string message)
            : base((int)HttpStatusCode.BadRequest, message)
        {
        }

        public BadRequestException()
        : base((int)HttpStatusCode.BadRequest)
        {
        }
    }
}
