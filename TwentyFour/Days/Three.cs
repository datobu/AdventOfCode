using System.Text.RegularExpressions;

namespace TwentyFour.Days;

// idea from gilles for part 2
internal static class Three
{
    public static void Run()
    {
        // TaskOne();
        TaskTwo();
    }

    public static void TaskOne()
    {
        var input = File.ReadAllLines("../../../Common/Inputs/DayThree.txt");
        var pattern = @"mul\([0-9]*,[0-9]*\)";

        int sum = 0;

        foreach (var line in input)
        {
            // get all substrings out of line that match the pattern regex
            var matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                // cut the first 4 characters off the match
                var substr = match.Value[4..];

                // split the substring by the comma
                var split = substr.Split(',');

                int first = int.Parse(split[0]);

                // remove the last character of split[1]
                int second = int.Parse(split[1].Remove(split[1].Length - 1));

                sum += first * second;
            }
        }

        Console.WriteLine(sum);
    }

    public static void TaskTwo()
    {
        var input = File.ReadAllLines("C:\\Users\\ButDa793\\Desktop\\advent\\day_3.txt");
        var pattern = @"(mul\([0-9]*,[0-9]*\))|(do\(\))|(don't\(\))";

        int sum = 0;
        bool enabled = true;

        foreach (var line in input)
        {
            // get all substrings out of line that match the pattern regex
            var matches = Regex.Matches(line, pattern);

            foreach (Match match in matches)
            {
                if (match.ToString() == "do()")
                {
                    enabled = true;
                }
                else if (match.ToString() == "don't()")
                {
                    enabled = false;
                }
                else if (enabled)
                {
                    // cut the first 4 characters off the match
                    var substr = match.Value[4..];

                    // split the substring by the comma
                    var split = substr.Split(',');
                    int first = int.Parse(split[0]);

                    // remove the last character of split[1]
                    int second = int.Parse(split[1].Remove(split[1].Length - 1));
                    sum += first * second;
                }
            }
        }

        Console.WriteLine(sum);
    }
}