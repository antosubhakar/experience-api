using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Doctrina.ExperienceApi.Data.Json.Converters
{
    public class StatementConverter : JsonConverter<Statement>
    {
        public override Statement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();

            var statement = new Statement();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return statement;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException();

                string propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "id":
                        statement.Id = JsonSerializer.Deserialize<Guid>(ref reader, options);
                        break;

                    case "actor":
                        statement.Actor = JsonSerializer.Deserialize<Agent>(ref reader, options);
                        break;

                    case "object":
                        statement.Object = JsonSerializer.Deserialize<IStatementObject>(ref reader, options);
                        break;

                    case "stored":
                        statement.Stored = JsonSerializer.Deserialize<DateTimeOffset?>(ref reader, options);
                        break;

                    case "authority":
                        statement.Authority = JsonSerializer.Deserialize<Agent>(ref reader, options);
                        break;

                    case "version":
                        break;

                    default:
                        throw new JsonException();
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Statement value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}