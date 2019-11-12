using System;

namespace Doctrina.ExperienceApi.Client
{
    public class JsonModelReaderException : Exception
    {
        public JsonModelReaderException()
        {
        }

        public JsonModelReaderException(string message) : base(message)
        {
        }

        public JsonModelReaderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
