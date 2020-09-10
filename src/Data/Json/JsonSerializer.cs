using Doctrina.ExperienceApi.Data.Json.Converters;
using System.Text.Json;

namespace Doctrina.ExperienceApi.Data.Json
{
    public class JSerializer
    {
        private readonly JsonSerializerOptions _options;

        public JSerializer()
        {
            _options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            _options.Converters.Add(new StatementConverter());
            _options.Converters.Add(new AgentConverter());
        }

        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, _options);
        }

        public TValue Deserialize<TValue>(string json)
        {
            return JsonSerializer.Deserialize<TValue>(json, _options);
        }
    }
}