using System;

namespace Doctrina.ExperienceApi.Client.Exceptions
{
    public class JsonModelReaderException : Exception
    {
        public JsonModelReaderException(string message) : base(message)
        {
        }

        public JsonModelReaderException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
