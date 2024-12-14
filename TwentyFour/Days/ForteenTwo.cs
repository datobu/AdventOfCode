namespace TwentyFour.Days;

// solution idea stolen by google / reddit
public class ForteenTwo
{
    public static class GameParameter
    {
        public const string Path = "../../../Common/Inputs/DayForteen.txt";
        public const int TimeInSecods = 100;
        public const int Height = 103;
        public const int Width = 101;
    }

    public static void PartTwo()
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

        int[,] copy = (int[,])map.Clone();

        foreach (var line in lines)
        {
            var lineParts = line[2..].Split(" v=");
            int[] start = lineParts[0].Split(',').Select(int.Parse).ToArray();
            int[] velocity = lineParts[1].Split(',').Select(int.Parse).ToArray();

            var robot = new Robot(start[0], start[1], velocity[0], velocity[1]);

            robots.Add(robot);
        }

        for (int i = 1; i < 2000000000; i++)
        {
            foreach (var robot in robots)
            {
                map[robot.GetYEnd(i), robot.GetXEnd(i)]++;
            }

            for (int y = 0; y < GameParameter.Height; y++)
            {
                int treesInRow = 0;
                for (int x = 0; x < GameParameter.Width; x++)
                {
                    if (map[y, x] >= 1)
                    {
                        treesInRow++;
                    }
                    else
                    {
                        treesInRow = 0;
                    }

                    if (treesInRow > 10)
                    {
                        Console.WriteLine($"Second: {i}");
                        PrintMap(map);
                    }
                }
            }

            map = (int[,])copy.Clone();
        }
    }

    private static void PrintMap(int[,] map)
    {
        for (int i = 0; i < GameParameter.Height; i++)
        {
            for (int j = 0; j < GameParameter.Width; j++)
            {
                string value = map[i, j] == 0 ? "." : map[i, j].ToString();
                Console.Write($"{value}");
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

        public int GetXEnd(int seconds)
        {
            int xEnd = (XStart + (XSpeed * seconds)) % GameParameter.Width;

            if (xEnd < 0)
            {
                xEnd += GameParameter.Width;
            }

            return xEnd;
        }

        public int GetYEnd(int seconds)
        {
            int yEnd = (YStart + (YSpeed * seconds)) % GameParameter.Height;

            if (yEnd < 0)
            {
                yEnd = yEnd + GameParameter.Height;
            }

            return yEnd;
        }
    }
}