using Doctrina.ExperienceApi.Data.Exceptions;
using Shouldly;
using Xunit;

namespace Doctrina.ExperienceApi.Data.Tests
{
    public class DurationTests
    {
        [Theory]
        [InlineData("P3Y6M4DT12H30M5S")]
        [InlineData("P0DT06H23M34S")]
        [InlineData("P1DT6H23M3.141593S")]
        [InlineData("P0Y")]
        [InlineData("P7Y")]
        [InlineData("P8M")]
        public void MustParseValid_Duration(string durationString)
        {
            Should.NotThrow(() =>
            {
                new Duration(durationString);
            });
        }

        [Theory]
        [InlineData("PW")]
        [InlineData("PH230DT06M34S")]
        [InlineData("PT61DH23M3.141593S")]
        [InlineData("0Y")]
        [InlineData("7Y")]
        [InlineData("8M")]
        public void ShouldNotParseInvalid_Duration(string durationString)
        {
            Should.Throw<DurationFormatException>(() =>
            {
                new Duration(durationString);
            });
        }
    }
}
