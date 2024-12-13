using TwentyFour.Days;

namespace TwentyFour;

internal class Program
{
    private static void Main(string[] args)
    {
        _ = args;

        // var day = new Twelve();

        long result = Twelve.Run();

        Console.WriteLine(result);
    }
}
