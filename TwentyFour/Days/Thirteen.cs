namespace TwentyFour.Days;

internal class Thirteen
{
    private class Game(int aXValue, int aYValue, int bXValue, int bYValue, int xWin, int yWin)
    {
        public int AButtonPushCount { get; set; } = 0;

        public int BButtonPushCount { get; set; } = 0;

        public bool IsSolved { get; private set; } = false;

        public long CurrentCost { get; set; } = 0;

        private int XStart { get; set; } = 0;

        private int YStart { get; set; } = 0;

        private int XCurrent { get; set; } = 0;

        private int YCurrent { get; set; } = 0;

        private int AXValue { get; set; } = aXValue;

        private int AYValue { get; set; } = aYValue;

        private int BXValue { get; set; } = bXValue;

        private int BYValue { get; set; } = bYValue;

        private int XWin { get; set; } = xWin;

        private int YWin { get; set; } = yWin;

        internal void Reset()
        {
            XCurrent = XStart;
            YCurrent = YStart;
            AButtonPushCount = 0;
            BButtonPushCount = 0;
            CurrentCost = 0;
        }

        internal long PushA()
        {
            AButtonPushCount++;
            XCurrent += AXValue;
            YCurrent += AYValue;
            CurrentCost += 3;

            if (XCurrent == XWin && YCurrent == YWin)
            {
                IsSolved = true;
                return CurrentCost;
            }

            if (XCurrent > XWin || YCurrent > YWin || AButtonPushCount == 101)
            {
                return -2;
            }

            return -1;
        }

        internal long PushB()
        {
            BButtonPushCount++;
            XCurrent += BXValue;
            YCurrent += BYValue;
            CurrentCost += 1;

            if (XCurrent == XWin && YCurrent == YWin)
            {
                IsSolved = true;
                return CurrentCost;
            }

            if (XCurrent > XWin || YCurrent > YWin || BButtonPushCount == 101)
            {
                return -2;
            }

            return -1;
        }
    }

    public static long Run()
    {
        // CreateOperatorList(4);

        return TaskOne();
    }

    private static long TaskOne()
    {
        List<Game> games = CreateGames();

        long sum = 0;

        foreach (var game in games)
        {
            int output = -1;
            int runCount = 0;

            // finish not yet reached
            while (output == -1)
            {
                output = (int)game.PushB();
                runCount++;
            }

            // finish reached exactly
            if (output != -2)
            {
                sum += output;

                if (output < 0)
                {
                    throw new Exception();
                }
            }

            // ran too far:
            else
            {
                int i = 1;

                while (i <= runCount)
                {
                    game.Reset();
                    int runCountTarget = runCount - i;

                    for (int j = 0; j < runCountTarget; j++)
                    {
                        game.PushB();
                    }

                    int innerOutPut = -1;

                    while (innerOutPut == -1)
                    {
                        innerOutPut = (int)game.PushA();
                    }

                    // finish reached exactly
                    if (innerOutPut != -2)
                    {
                        sum += innerOutPut;

                        if (innerOutPut < 0)
                        {
                            throw new Exception();
                        }

                        // exit while loop:
                        i = runCount + 1;
                    }

                    i++;
                }
            }
        }

        int count = 1;
        long mySum = 0;
        foreach (var game in games)
        {
            if (game.IsSolved)
            {
                mySum += game.CurrentCost;
                Console.WriteLine($"{count++} - Solution / {mySum}: a = {game.AButtonPushCount}, b = {game.BButtonPushCount}");
            }
            else
            {
                Console.WriteLine($"{count++} - No Solution: a = {game.AButtonPushCount}, b = {game.BButtonPushCount}");
            }
        }

        return sum;
    }

    private static List<Game> CreateGames()
    {
        var lines = File.ReadAllLines("../../../Common/Inputs/DayThirteen.txt");

        int lineNumber = 0;
        int ax = 0;
        int ay = 0;
        int bx = 0;
        int by = 0;
        int px = 0;
        int py = 0;

        List<Game> games = [];
        foreach (var line in lines)
        {
            string xValue, yValue;
            switch (lineNumber % 4)
            {
                case 0:
                    GetValues(line, "X+", "Y+", out xValue, out yValue);

                    ax = int.Parse(xValue);
                    ay = int.Parse(yValue);
                    break;
                case 1:
                    GetValues(line, "X+", "Y+", out xValue, out yValue);

                    bx = int.Parse(xValue);
                    by = int.Parse(yValue);
                    break;
                case 2:
                    GetValues(line, "X=", "Y=", out xValue, out yValue);

                    px = int.Parse(xValue);
                    py = int.Parse(yValue);
                    break;
                case 3:
                    games.Add(new Game(ax, ay, bx, by, px, py));
                    break;
                default:
                    throw new Exception();
            }

            lineNumber++;
        }

        games.Add(new Game(ax, ay, bx, by, px, py));

        return games;
    }

    private static void GetValues(string line, string xSeperator, string ySeperator, out string xValue, out string yValue)
    {
        int xIndex = line.IndexOf(xSeperator) + 2;
        int yIndex = line.IndexOf(ySeperator) + 2;

        xValue = line.Substring(xIndex, line.IndexOf(",", xIndex) - xIndex);
        yValue = line.Substring(yIndex);
    }
}