using System.Collections.Generic;

namespace Doctrina.ExperienceApi.Server.Exceptions
{
    public class ValidationException : ServerException
    {
        public ValidationException() { }

        public IDictionary<string, string[]> Failures { get; }
    }
}
