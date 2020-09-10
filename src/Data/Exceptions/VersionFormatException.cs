using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class VersionFormatException : ExperienceDataException
    {
        public VersionFormatException(string message) : base(message)
        {
        }

        public VersionFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
