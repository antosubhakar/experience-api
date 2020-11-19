using Doctrina.ExperienceApi.Data.Exceptions;

namespace Doctrina.ExperienceApi.Server.Exceptions
{
    public class NotFoundException : StatusCodeException
    {
        public NotFoundException(string message) : base(404, message) { }

        public NotFoundException(string entity, object identifier)
            : base(404, $"\"{entity}\" ({identifier}) was not found.")
        {
        }
    }
}
