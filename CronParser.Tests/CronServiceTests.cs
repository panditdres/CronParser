using Xunit;
using System.Collections.Generic;
using System.Linq;
using CronParser.Services;

namespace CronParser.Tests
{
    public class CronServiceTests
    {
        [Theory]
        [InlineData("1-5","1 2 3 4 5")]
        [InlineData("1-2", "1 2")]
        [InlineData("7-10", "7 8 9 10")]
        [InlineData("22-30", "22 23 24 25 26 27 28 29 30")]
        public void GivenDayOfMonthRange_WhenRangeGiven_ThenWeWantToRetrieveTheCorrectOutput(string input, string expected)
        {
            IEnumerable<int> range = Enumerable.Range(1, 31);
            var result = CronService.GetRange(input, range);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GivenDayOfMonthRange_WhenRangeGivenOutsideOfMaximum_ThenThrowException()
        {
            IEnumerable<int> range = Enumerable.Range(1, 31);
            var result = CronService.GetRange("1-40", range);
            Assert.Equal("Input 1-40 is out of range with maximum being 31", result);
        }

        [Theory]
        [InlineData("*/5", "5 10 15 20 25 30")]
        [InlineData("*/2", "2 4 6 8 10 12 14 16 18 20 22 24 26 28 30")]
        [InlineData("*/12", "12 24")]
        public void GivenDayOfMonthRange_WhenFrequencyGiven_ThenWeWantToRetrieveTheCorrectOutput(string input, string expected)
        {
            IEnumerable<int> range = Enumerable.Range(1, 31);
            var result = CronService.GetFrequency(input, range);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("1,5", "1 5")]
        [InlineData("1,2", "1 2")]
        [InlineData("7,10", "7 10")]
        [InlineData("22,30", "22 30")]
        public void GivenInput_WhenCommaSeperated_ThenReturnTheValuesPresent(string input, string expected)
        {
            var result = CronService.GetValue(input);
            Assert.Equal(expected, result);
        }
    }
}
