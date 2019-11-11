using System;

namespace Doctrina.ExperienceApi.Data.Json
{
    public class JsonModelException : Exception
    {
        public JsonModelException()
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
