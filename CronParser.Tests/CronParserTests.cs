using Xunit;
using System.Collections.Generic;
using System.Linq;
using CronParser.Services;

namespace CronParser.Tests
{
    public class CronParserTests
    {
        [Fact]
        public void GivenInputLengthIsNotTheExpectedLength_ThenWeWantToGiveAnErrorMessag_SoThatTheCorrectInputIsPassed()
        {
            var result = Program.CronExpressionParser("12 12 12 12 12 12 12 12");
            Assert.Equal("I'm sorry, the input is invalid", result);
        }

        [Fact]
        public void GivenInput_ThenWeWantToParseTheCron_SoThatWeObtainTheExpectedBehaviour()
        {
            var expected = 
                "minute        10\n" +
                "hour          10\n" +
                "day of month  10\n" +
                "month         10\n" +
                "day of week   10\n" +
                "command       test\n";
            var result = Program.CronExpressionParser("10 10 10 10 10 test");
            Assert.Equal(expected, result);
        }
    }
}
