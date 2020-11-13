using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Doctrina.ExperienceApi.Data.Tests
{
    public class IriTests
    {
        [Fact]
        public void Iri_DefaultValue_MustBeNull()
        {
            // Arrange
            var iri = new Iri();

            // Act
            var json = JsonConvert.SerializeObject(iri);

            // Assert
            json.ShouldBe("null");
        }

        [Fact]
        public void Iri_ObjectDefaultValue_MustBeNull()
        {
            // Arrange
            var obj = new
            {
                value = new Iri()
            };

            // Act
            var json = JsonConvert.SerializeObject(obj);

            // Assert
            json.ShouldBe("{\"value\":null}");
        }

        [Theory]
        [InlineData("http://adlnet.gov/expapi/verbs/attended")]
        [InlineData("https://w3id.org/xapi/adl/verbs/abandoned")]
        [InlineData("http://adlnet.gov/expapi/verbs/experienced")]
        public void Iri_Should_ParseValid_String(string verb)
        {
            Should.NotThrow(() =>
            {
                var parsed = new Iri(verb);

                parsed.ToString().ShouldBe(verb);
            });
        }
    }
}
