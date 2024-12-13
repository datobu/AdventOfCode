namespace TwentyFour.Days;

internal class ThirteenTwo
{
    private const int _part2 = 0;

    // int part2 = 10000000000000;

    private class Game(long a1, long a2, long b1, long b2, long z1, long z2)
    {
        public long A1 { get; set; } = a1;
        public long A2 { get; set; } = a2;
        public long B1 { get; set; } = b1;
        public long B2 { get; set; } = b2;
        public long Z1 { get; set; } = z1;
        public long Z2 { get; set; } = z2;
    }

    public static long Run()
    {
        // CreateOperatorList(4);

        return TaskOne();
    }

    private static long TaskOne()
    {
        var games = CreateGames();

        long sum = 0;
        int count = 1;
        foreach (var matrix in games)
        {
            long factor1 = matrix[1, 1];
            long factor2 = matrix[0, 1];

            long newA1 = matrix[0, 0] * factor1;
            long newC1 = matrix[0, 2] * factor1;

            long newA2 = matrix[1, 0] * factor2;
            long newC2 = matrix[1, 2] * factor2;

            long buttonA = (newC2 - newC1) / (newA2 - newA1);

            long buttonB = (matrix[0, 2] - (matrix[0, 0] * buttonA)) / matrix[0, 1];

            // Output the solution
            if (buttonA >= 0 && buttonB >= 0)
            {
                if (_part2 > 0 || (buttonA <= 100 && buttonB <= 100))
                {
                    Console.WriteLine($"{count++} - Solution: a = {buttonA}, b = {buttonB}");
                    sum += (buttonA * 3) + buttonB;
                }
                else
                {
                    Console.WriteLine($"{count++} - no solution for this problem");
                }
            }
            else
            {
                Console.WriteLine($"{count++} - no solution for this problem");
            }
        }

        return sum;
    }

    private static List<long[,]> CreateGames()
    {
        var lines = File.ReadAllLines("../../../Common/Inputs/DayThirteen.txt");

        int lineNumber = 0;
        long a1 = 0;
        long b1 = 0;
        long a2 = 0;
        long b2 = 0;
        long c1 = 0;
        long c2 = 0;

        List<long[,]> arrays = [];

        foreach (var line in lines)
        {
            string xValue, yValue;
            switch (lineNumber % 4)
            {
                case 0:
                    GetValues(line, "X+", "Y+", out xValue, out yValue);

                    a1 = long.Parse(xValue);
                    b1 = long.Parse(yValue);
                    break;
                case 1:
                    GetValues(line, "X+", "Y+", out xValue, out yValue);

                    a2 = long.Parse(xValue);
                    b2 = long.Parse(yValue);
                    break;
                case 2:
                    GetValues(line, "X=", "Y=", out xValue, out yValue);

                    c1 = long.Parse(xValue);
                    c2 = long.Parse(yValue);

                    Console.WriteLine($"{a1} {a2} {c1 + _part2}");
                    Console.WriteLine($"{b1} {b2} {c2 + _part2}");
                    Console.WriteLine();

                    long[,] a = new long[2, 3]
                    {
                        { a1, a2, c1 + _part2 },
                        { b1, b2, c2 + _part2 }
                    };

                    arrays.Add(a);
                    break;
                case 3:
                    break;
                default:
                    throw new Exception();
            }

            lineNumber++;
        }

        return arrays;
    }

    private static void GetValues(string line, string xSeperator, string ySeperator, out string xValue, out string yValue)
    {
        int xIndex = line.IndexOf(xSeperator) + 2;
        int yIndex = line.IndexOf(ySeperator) + 2;

        xValue = line.Substring(xIndex, line.IndexOf(",", xIndex) - xIndex);
        yValue = line.Substring(yIndex);
    }
}