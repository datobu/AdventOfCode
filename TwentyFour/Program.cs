using TwentyFour.Days;

namespace TwentyFour;

internal class Program
{
    private static void Main(string[] args)
    {
        _ = args;

        // var day = new Seven();

        long result = Seven.Run();

        Console.WriteLine(result);
    }
}
