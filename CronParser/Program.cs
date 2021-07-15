using System;
using System.Collections.Generic;
using System.Linq;
using CronParser.Services;
using CronParser.Enum;

namespace CronParser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var output = CronExpressionParser(args[0]);
            Console.WriteLine(output);
        }
        
        private static readonly Dictionary<string, TokenType> TokenTypes = new Dictionary<string, TokenType>
        {
            [" "] = TokenType.FieldSeparator,
            [","] = TokenType.ListItemSeparator,
            ["-"] = TokenType.RangeSeparator,
            ["/"] = TokenType.StepSeparator,
            ["*"] = TokenType.Blank,
            ["?"] = TokenType.Blank,
            ["#"] = TokenType.Extension,
        };

        public static string CronExpressionParser(string input)
        {
            var splitArray = input.Split(" ");

            if(splitArray.Length != 6)
            {
                return "I'm sorry, the input is invalid";
            }

            var resultDictionary = new Dictionary<string, string>(){
                { CronValues.minute.ToString(), string.Empty },
                { CronValues.hour.ToString(), string.Empty },
                { CronValues.day.ToString(), string.Empty },
                { CronValues.month.ToString(), string.Empty },
                { CronValues.week.ToString(), string.Empty },
            }; ;

            var layout = new string[] {
                CronValues.minute.ToString(),
                CronValues.hour.ToString(),
                CronValues.day.ToString(),
                CronValues.month.ToString(),
                CronValues.week.ToString()
            };

            var range = new Dictionary<string, IEnumerable<int>>()
            {
                { CronValues.minute.ToString(), Enumerable.Range(0,60) },
                { CronValues.hour.ToString(), Enumerable.Range(0,24) },
                { CronValues.day.ToString(), Enumerable.Range(1,31) },
                { CronValues.month.ToString(), Enumerable.Range(1,12) },
                { CronValues.week.ToString(), Enumerable.Range(1,7) },
            };

            var valueDictionary = new Dictionary<string, string>();

            for (var i = 0; i < splitArray.Length - 1; i++)
            {
                if(splitArray[i].Length == 1)
                {
                    if (!TokenTypes.ContainsKey(splitArray[i]))
                    {
                        resultDictionary[layout[i]] = splitArray[i];
                    }
                    else
                    {
                        var allValues = CronService.GetAllValues(range[layout[i]]);
                        resultDictionary[layout[i]] = allValues.TrimEnd();
                    }
                }

                valueDictionary.Add(layout[i], splitArray[i]);
            }

            foreach(var value in valueDictionary)
            {
                if (value.Value.Contains(","))
                {
                    var requiredValue = CronService.GetValue(value.Value);
                    resultDictionary[value.Key] = requiredValue;
                }
                else if (value.Value.Contains("/"))
                {
                    var requiredFrequency = CronService.GetFrequency(value.Value, range[value.Key]);
                    resultDictionary[value.Key] = requiredFrequency;
                }
                else if (value.Value.Contains("-"))
                {
                    var requiredRange = CronService.GetRange(value.Value, range[value.Key]);
                    resultDictionary[value.Key] = requiredRange;
                }
                else
                {
                    resultDictionary[value.Key] = value.Value;
                }
            }

            resultDictionary.Add("command", splitArray[splitArray.Length - 1]);

            var outputNaming = new Dictionary<string, string>()
            {
                { CronValues.minute.ToString(), "minute" },
                { CronValues.hour.ToString(), "hour" },
                { CronValues.day.ToString(), "day of month" },
                { CronValues.month.ToString(), "month" },
                { CronValues.week.ToString(), "day of week" },
                { "command", "command" },
            };

            var resultString = string.Empty;
            foreach(var result in resultDictionary)
            {
                resultString += $"{outputNaming[result.Key].PadRight(14, ' ')}{resultDictionary[result.Key]}\n";
            }

            return resultString;
        }
    }
}