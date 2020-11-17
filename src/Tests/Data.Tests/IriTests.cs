using Doctrina.ExperienceApi.Data.Exceptions;
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

        [Fact]
        public void Valid_Iri_Should_ParseValid_String()
        {
            // Arrange
            string verb = "http://adlnet.gov/expapi/verbs/attended";

            // Act
            // Assert
            Should.NotThrow(() =>
            {
                var parsed = new Iri(verb);
            });
        }

        [Fact]
        public void Invalid_Iri_Should_Throw()
        {
            // Arrange
            string verb = "adlnet.gov/expapi/verbs/attended";

            // Act
            // Assert
            Should.Throw<IriFormatException>(() =>
            {
                var parsed = new Iri(verb);
            });
        }

        [Theory]
        [InlineData("http://adlnet.gov/expapi/verbs/attended", "http://adlnet.gov/expapi/verbs/attended")]
        [InlineData("http://adlnet.gov/expapi/verbs/completed", "http://adlnet.gov/expapi/verbs/completed")]
        public void Computed_Hash_Same_Iri_Must_Not_Change(string strVerb1, string strVerb2)
        {
            // Arrange
            var parsed1 = new Iri(strVerb1);
            var parsed2 = new Iri(strVerb2);

            // Act
            string hash1 = parsed1.ComputeHash();
            string hash2 = parsed2.ComputeHash();

            // Assert
            hash1.ShouldBe(hash2);
            hash2.ShouldBe(hash1);
        }

        [Theory]
        [InlineData("http://adlnet.gov/expapi/verbs/attended", "http://adlnet.gov/expapi/verbs/attended/")]
        [InlineData("http://adlnet.gov/expapi/verbs/completed", "http://adlnet.gov/expapi/verbs/completed/")]
        [InlineData("http://adlnet.gov/expapi/verbs/completed", "http://adlnet.gov/expapi/verbs/attended")]
        [InlineData("https://adlnet.gov/expapi/verbs/attended", "http://adlnet.gov/expapi/verbs/attended")]
        public void Computed_Hash_For_Different_Iri_Must_Not_Match(string strVerb, string strVerb2)
        {
            // Arrange
            var parsed = new Iri(strVerb);
            var parsed2 = new Iri(strVerb2);

            // Act
            string hash1 = parsed.ComputeHash();
            string hash2 = parsed2.ComputeHash();

            // Assert
            hash1.ShouldNotBe(hash2);
        }
    }
}
