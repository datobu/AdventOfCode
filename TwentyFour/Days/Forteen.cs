namespace TwentyFour.Days;

// completely alone part one
public class Forteen
{
    public static class GameParameter
    {
        public const string Path = "../../../Common/Inputs/DayForteen.txt";
        public const int TimeInSecods = 100;
        public const int Height = 103;
        public const int Width = 101;
    }

    public static int PartOne()
    {
        var lines = File.ReadAllLines(GameParameter.Path);

        List<Robot> robots = [];

        int[,] map = new int[GameParameter.Height, GameParameter.Width];
        for (int i = 0; i < GameParameter.Height; i++)
        {
            for (int j = 0; j < GameParameter.Width; j++)
            {
                map[i, j] = 0;
            }
        }

        foreach (var line in lines)
        {
            var lineParts = line[2..].Split(" v=");
            int[] start = lineParts[0].Split(',').Select(int.Parse).ToArray();
            int[] velocity = lineParts[1].Split(',').Select(int.Parse).ToArray();

            var robot = new Robot(start[0], start[1], velocity[0], velocity[1]);

            robots.Add(robot);

            map[robot.YEnd, robot.XEnd]++;
        }

        PrintMap(map);

        int halfHeight = (int)Math.Floor((double)(GameParameter.Height / 2));
        int halfWidth = (int)Math.Floor((double)(GameParameter.Width / 2));

        int q1 = GetSumOfQuadrant(map, halfHeight, halfWidth, 0, 0);
        int q2 = GetSumOfQuadrant(map, halfHeight, GameParameter.Width, 0, halfWidth + 1);
        int q3 = GetSumOfQuadrant(map, GameParameter.Height, halfWidth, halfHeight + 1, 0);
        int q4 = GetSumOfQuadrant(map, GameParameter.Height, GameParameter.Width, halfHeight + 1, halfWidth + 1);

        return q1 * q2 * q3 * q4;
    }

    private static int GetSumOfQuadrant(int[,] map, int yEnd, int xEnd, int yStart, int xStart)
    {
        int qSum = 0;

        for (int y = yStart; y < yEnd; y++)
        {
            for (int x = xStart; x < xEnd; x++)
            {
                qSum += map[y, x];
            }
        }

        return qSum;
    }

    private static void PrintMap(int[,] map)
    {
        for (int i = 0; i < GameParameter.Height; i++)
        {
            for (int j = 0; j < GameParameter.Width; j++)
            {
                Console.Write($"{map[i, j]} ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private class Robot(int xStart, int yStart, int xSpeed, int ySpeed)
    {
        public int XStart { get; private set; } = xStart;

        public int YStart { get; private set; } = yStart;

        public int XSpeed { get; private set; } = xSpeed;

        public int YSpeed { get; private set; } = ySpeed;

        public int XEnd
        {
            get
            {
                int xEnd = (XStart + (XSpeed * GameParameter.TimeInSecods)) % GameParameter.Width;

                if (xEnd < 0)
                {
                    xEnd += GameParameter.Width;
                }

                return xEnd;
            }
        }

        public int YEnd
        {
            get
            {
                int yEnd = (YStart + (YSpeed * GameParameter.TimeInSecods)) % GameParameter.Height;

                if (yEnd < 0)
                {
                    yEnd += GameParameter.Height;
                }

                return yEnd;
            }
        }
    }
}