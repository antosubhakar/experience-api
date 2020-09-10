using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class JsonWebSignatureException : Exception
    {
        public JsonWebSignatureException(string message) : base(message)
        {
        }

        public JsonWebSignatureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
