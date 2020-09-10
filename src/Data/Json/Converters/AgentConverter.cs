using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Doctrina.ExperienceApi.Data.Json.Converters
{
    public class AgentConverter : JsonConverter<Agent>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(Agent));
        }

        public override Agent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException();
            string rawText;
            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                if (!jsonDocument.RootElement.TryGetProperty("objectType", out JsonElement jsonElement))
                    throw new JsonException();

                if (jsonElement.ValueKind != JsonValueKind.String)
                    throw new JsonException();

                string discriminator = jsonElement.GetString();
                if (discriminator != "Agent"
                    && discriminator != "Group")
                    throw new JsonException();

                if (discriminator == "Group")
                {
                    return JsonSerializer.Deserialize<Group>(ref reader, options);
                }

                rawText = jsonDocument.RootElement.GetRawText();
            }

            var actor = new Agent();
            if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return actor;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException();

                string propertyName = reader.GetString();
                reader.Read();
                switch (propertyName)
                {
                    case "mbox":
                        actor.Mbox = JsonSerializer.Deserialize<Mbox>(ref reader, options);
                        break;
                    case "name":
                        actor.Name = JsonSerializer.Deserialize<string>(ref reader, options);
                        break;
                    case "account":
                        actor.Account = JsonSerializer.Deserialize<Account>(ref reader, options);
                        break;
                    default:
                        throw new JsonException();
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Agent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
