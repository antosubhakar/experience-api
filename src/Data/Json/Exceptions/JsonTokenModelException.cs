using Newtonsoft.Json.Linq;

namespace Doctrina.ExperienceApi.Data.Json
{
    public class JsonTokenModelException : JsonModelException
    {
        public JsonTokenModelException(JToken token, string message)
            : base(FormatMessage(message, token))
        {
            Token = token;
        }

        private static string FormatMessage(string message, JToken token)
        {
            message = message.TrimEnd().EnsureEndsWith(".");
            if (token != null)
            {
                message += $" Path: '{token.Path}'";
            }
            return message;
        }

        public JToken Token { get; }
    }
}
