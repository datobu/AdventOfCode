namespace TwentyFour.Days;

// idea stolen by google / reddit / implemented with the use of chatgpt
internal class ThirteenTwo
{
    private const long _part2 = 10000000000000;

    // private const long _part2 = 10000000000000;

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

        int count = 0;
        foreach (var matrix in games)
        {
            count++;

            // Multiply the first equation by the coefficient of y in the second equation (to eliminate y)
            // and multiply the second equation by the coefficient of y in the first equation.
            long factor1 = matrix[1, 1];  // Coefficient of y in the second equation
            long factor2 = matrix[0, 1];  // Coefficient of y in the first equation

            // Create new equations after multiplication
            long newA1 = matrix[0, 0] * factor1;  // New coefficient for x in the first equation
            long newC1 = matrix[0, 2] * factor1;  // New constant term in the first equation

            long newA2 = matrix[1, 0] * factor2;  // New coefficient for x in the second equation
            long newC2 = matrix[1, 2] * factor2;  // New constant term in the second equation

            // Now subtract the equations to eliminate y
            long denominator = newA2 - newA1;
            if (denominator == 0)
            {
                // Infinite solutions: choose x = 0 or y = 0 and solve for the other variable
                Console.WriteLine("Infinite solutions. Providing one where x or y is close to 0.");

                // Try x = 0
                long y1 = matrix[0, 2] / matrix[0, 1];  // Solve for y when x = 0

                // Alternatively, try y = 0
                long x1 = matrix[1, 2] / matrix[1, 0];  // Solve for x when y = 0

                if ((x1 <= 100 && y1 <= 100) || _part2 > 0)
                {
                    sum += (x1 * 3) + y1;
                    Console.WriteLine($"SolutionA / {sum}: A = {x1}, B = {y1}");
                }

                continue;
            }

            long a = (newC2 - newC1) / denominator;

            // Solve for y using the value of x in one of the original equations
            long b = (matrix[0, 2] - (matrix[0, 0] * a)) / matrix[0, 1];

            // Check if x and y are both non-negative
            if (a < 0 || b < 0)
            {
                Console.WriteLine($"No Solution: I No valid solution where x >= 0 and y >= 0.");
            }
            else
            {
                var xResult = (a * matrix[0, 0]) + (b * matrix[0, 1]);
                var xShall = matrix[0, 2];

                bool xIsCorrect = matrix[0, 2] == xResult;

                var yResult = (a * matrix[1, 0]) + (b * matrix[1, 1]);
                bool yIsCorrect = matrix[1, 2] == yResult;

                // bool xIsCorrect = true;

                // Output the valid solution
                if (((a <= 100 && b <= 100) || _part2 > 0) && xIsCorrect && yIsCorrect)
                {
                    sum += (a * 3) + b;
                    Console.WriteLine($"SolutionB / {sum}: a = {a}, b = {b}");
                }
                else
                {
                    Console.WriteLine($"No Solution II: ");
                }
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

                    long c1 = long.Parse(xValue);
                    long c2 = long.Parse(yValue);

                    /*Console.WriteLine($"{a1} {a2} {c1 + _part2}");
                    Console.WriteLine($"{b1} {b2} {c2 + _part2}");
                    Console.WriteLine();*/

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

        xValue = line[xIndex..line.IndexOf(',', xIndex)];
        yValue = line[yIndex..];
    }
}