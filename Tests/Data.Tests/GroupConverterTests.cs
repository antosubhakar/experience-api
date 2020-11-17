using Doctrina.ExperienceApi.Data.Json;
using Shouldly;
using Xunit;

namespace Doctrina.ExperienceApi.Data.Tests
{
    public class GroupConverterTests
    {
        [Fact]
        public void DerserializeIdentifiedGroup()
        {
            string json = @"{
                ""name"": ""Team PB"",
        ""mbox"": ""mailto:teampb@example.com"",
        ""member"": [
            {
                    ""name"": ""Andrew Downes"",
                ""account"": {
                        ""homePage"": ""http://www.example.com"",
                    ""name"": ""13936749""
                },
                ""objectType"": ""Agent""
            },
            {
                    ""name"": ""Toby Nichols"",
                ""openid"": ""http://toby.openid.example.org/"",
                ""objectType"": ""Agent""
            },
            {
                    ""name"": ""Ena Hills"",
                ""mbox_sha1sum"": ""ebd31e95054c018b10727ccffd2ef2ec3a016ee9"",
                ""objectType"": ""Agent""
            }
        ],
        ""objectType"": ""Group""
    }";

            var serializer = new JSerializer();
            var agent = serializer.Deserialize<Group>(json);
            agent.Name.ShouldBe("Team PB");
            agent.Mbox.ToString().ShouldBe("mailto:teampb@example.com");
        }
    }
}
