using System.Collections.Generic;
using System.Linq;

namespace CronParser.Services
{
    public static class CronService
    {
        public static string GetRange(string input, IEnumerable<int> range)
        {
            var splitInput = input.Split("-");

            var min = int.Parse(splitInput[0]);
            var max = int.Parse(splitInput[1]); // validation required to do

            if(max > range.Max())
            {
                return $"Input {input} is out of range with maximum being {range.Max()}";
            }

            var rangeResult = Enumerable.Range(min, (max + 1) - min);

            var result = string.Empty;

            foreach (var integer in rangeResult)
            {
                result += $"{integer} ";
            }

            return result.TrimEnd();
        }

        public static string GetValue(string input)
        {
            var splitInput = input.Split(",");

            return $"{splitInput[0]} {splitInput[1]}";
        }

        public static string GetFrequency(string input, IEnumerable<int> range)
        {
            var stringBuilder = string.Empty;
            var splitInput = input.Split("/");
            var interval = int.Parse(splitInput[1]);

            var min = range.Min();
            var max = range.Max();

            var requiredRange = Enumerable.Range(min, max).Where(x => x % interval == 0);

            foreach (var integer in requiredRange)
            {
                stringBuilder += $"{integer} ";
            }

            return stringBuilder.TrimEnd();
        }

        public static string GetAllValues(IEnumerable<int> range)
        {
            var builder = string.Empty;
            foreach (var rangeValue in range)
            {
                builder += $"{rangeValue} ";
            }

            return builder.TrimEnd();
        }
    }
}
