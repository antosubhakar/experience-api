using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class InvalidObjectTypeException : ExperienceDataException
    {
        public InvalidObjectTypeException(string type)
            : base($"'{type}' is not a valid ObjectType.")
        {
        }
    }
}
