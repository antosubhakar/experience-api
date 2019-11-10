using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class IriFormatException : Exception
    {
        public IriFormatException(string message) : base(message)
        {
        }
    }
}
