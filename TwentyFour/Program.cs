using TwentyFour.Days;

namespace TwentyFour;

internal class Program
{
    private static void Main(string[] args)
    {
        _ = args;

        var day = new TenPartTwo();

        long result = day.Run();

        Console.WriteLine(result);
    }
}
