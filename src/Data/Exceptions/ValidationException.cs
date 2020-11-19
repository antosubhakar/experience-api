using System.Collections.Generic;
using System.Net;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class ValidationException : StatusCodeException
    {
        public ValidationException()
         : base((int)HttpStatusCode.BadRequest)
        {
            Failures = new Dictionary<string, string[]>();
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}
