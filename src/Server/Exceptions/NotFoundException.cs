namespace Doctrina.ExperienceApi.Server.Exceptions
{
    public class NotFoundException : ServerException
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string entity, object identifier)
            : base($"\"{entity}\" ({identifier}) was not found.")
        {
        }
    }
}
