using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;

namespace Doctrina.ExperienceApi.Data.Tests
{
    public class LanguageMapTests
    {
        [Theory]
        [InlineData("en-US")]
        [InlineData("da-DK")]
        public void LanguageMap_Canonical(string lang)
        {
            // Arrange
            var languageMap = new LanguageMap()
            {
                {"en-US", "Description"},
                {"da-DK", "Beskrivelse"},
            };
            CultureInfo.CurrentCulture = new CultureInfo(lang);

            // Act
            var token = (JObject)languageMap.ToJToken(ApiVersion.GetLatest(), ResultFormat.Canonical);

            // Assert
            token.Properties().Count().ShouldBe(1);
            token[lang].ShouldNotBeNull();
        }
    }
}
