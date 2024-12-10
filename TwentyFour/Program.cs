using TwentyFour.Days;

namespace TwentyFour;

internal class Program
{
    private static void Main(string[] args)
    {
        _ = args;

        var day = new Ten();

        long result = day.Run();

        Console.WriteLine(result);
    }
}
