using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class DurationFormatException : ExperienceDataException
    {
        public DurationFormatException(string message) : base(message)
        {
        }

        public DurationFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
