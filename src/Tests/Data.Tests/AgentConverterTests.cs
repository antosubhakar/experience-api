using Doctrina.ExperienceApi.Data.Json;
using Shouldly;
using Xunit;

namespace Doctrina.ExperienceApi.Data.Tests
{
    public class AgentConverterTests
    {
        [Fact]
        public void DerserializeLongStatement()
        {
            string json = @"{
                ""name"": ""Team PB"",
                ""mbox"": ""mailto:teampb@example.com"",
                ""objectType"": ""Agent""
            }";

            var serializer = new JSerializer();
            var agent = serializer.Deserialize<Agent>(json);
            agent.Name.ShouldBe("Team PB");
            agent.Mbox.ToString().ShouldBe("mailto:teampb@example.com");
        }
    }
}
