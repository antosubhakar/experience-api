using Doctrina.ExperienceApi.Data.Exceptions;
using System.Collections.Generic;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class ValidationException : ExperienceDataException
    {
        public ValidationException() { }

        public IDictionary<string, string[]> Failures { get; }
    }
}
