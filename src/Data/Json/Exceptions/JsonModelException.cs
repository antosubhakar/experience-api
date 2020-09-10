using Doctrina.ExperienceApi.Data.Exceptions;
using System;

namespace Doctrina.ExperienceApi.Data.Json
{
    public class JsonModelException : ExperienceDataException
    {
        public JsonModelException() : base()
        {
        }

        public JsonModelException(string message)
            : base(message)
        {
        }

        public JsonModelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
