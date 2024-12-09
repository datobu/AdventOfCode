using TwentyFour.Days;

namespace TwentyFour;

internal class Program
{
    private static void Main(string[] args)
    {
        _ = args;

        // Create an instance of your day-specific class
        var day = new NinePartTwo();

        // Run the method and get the result
        long result = day.Run();

        // Output the result to the console
        Console.WriteLine(result);
    }
}
