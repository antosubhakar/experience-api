using System;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class InvalidInteractionTypeException : ExperienceDataException
    {
        public InvalidInteractionTypeException(string type)
            : base($"'{type}' is not a valid InteractionType.")
        {
        }
    }
}
