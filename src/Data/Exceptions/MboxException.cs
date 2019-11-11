using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Data.Exceptions
{
    public class MboxFormatException : Exception
    {
        public MboxFormatException(string message) : base(message)
        {
        }

        public MboxFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
