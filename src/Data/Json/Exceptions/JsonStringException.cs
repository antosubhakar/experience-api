using Doctrina.ExperienceApi.Data.Exceptions;
using System;

namespace Doctrina.ExperienceApi.Data.Json.Exceptions
{
    public class JsonStringException : ExperienceDataException
    {
        public JsonStringException(string message) : base(message)
        {
        }

        public JsonStringException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
