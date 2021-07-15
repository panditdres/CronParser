
### Proposed solution

This repository showcases the logic and the code on how we can display the different cron expressions based on their parser and the output each of the fields gives apart from year. This includes minute, hour, day of month, month and day of the week.

We want to accept an input from the user and the output will be displayed in a table where the keys will be in a column of 14 spaces wide.

- The logic used follows the Cron format for multiple input types :

      - 5 : a fixed value
      - 5-10 : a range
      - */5 : a frequency
      - 5,7 : a list of values 

#### How to run it 

Dependencies :

- Install `dotnet` if not installed.
- Go for .NET 5.0
- You can find the link here [dotnet](https://dotnet.microsoft.com/download)
- You can then open your terminal and run the application through there once `dotnet` has been installed
- You can verify the version of dotnet install by running `dotnet --version`

After cloning the repo, go to the folder level i.e `your/path/to/CronParser/CronParser` - you should be able to the a .csproj in there

To run the application, run `dotnet run {cron expression}`

To run the tests attached to the project, run `dotnet test` but ensure you are in the level above in your folder path i.e `your/path/to/CronParser/CronParser` - you should be able to see the `CronParser.Tests` folder
 
 Example :
 
 `dotnet run */15 0 1,15 * 1-5 /usr/bin/find`
 
 yields :
 
 ```
 minutes       0 15 30 45
 hour          0
 day of month  1 15
 month         1 2 3 4 5 6 7 8 9 10 11 12
 day of week   1 2 3 4 5
 command       /usr/bin/find
```

#### Errors

If in one of the field, a range is specified that is above the maximum range allowed, the following Error message will show `Input {input} is out of range with maximum being {max}`.
- for example, 1-40 in the day of month field, the error message will be `Input 1-40 is out of range with maximum being 31`

If the cron expression input is not 6 elements long, an error message will show
- for example, input is `10 10 10 10 10 test test2` - error message will be `I'm sorry, the input is invalid`

