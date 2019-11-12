using Doctrina.ExperienceApi.Data.Exceptions;
using System;

namespace Doctrina.ExperienceApi.Data.Consumer.Exceptions
{
    public class MultipartSectionException : ExperienceDataException
    {
        public MultipartSectionException(string message)
            : base(message)
        {
        }
    }
}
