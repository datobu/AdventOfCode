namespace TwentyFour.Days;

public class FifteenTwo
{
    public static class GameParameter
    {
        public const string Path = "../../../Common/Inputs/DayFifteen-Example.txt";

        // public const string Path = "../../../Common/Inputs/DayFifteen.txt";
    }

    private int _width;
    private int _height;
    private string _moveOrders = string.Empty;
    private char[,] _map = null!;

    public int PartTwo()
    {
        Init();

        (int y, int x) = GetStartingPosition();

        Console.WriteLine("Init");
        PrintMap(_map);

        int i = 0;

        foreach (char c in _moveOrders)
        {
            (y, x) = MakeStep(c, y, x);
            Console.WriteLine();
            Console.WriteLine($"{++i} / {_moveOrders.Length}: {c}");
            PrintMap(_map);
        }

        int sum = 0;

        for (y = 0; y < _height; y++)
        {
            for (x = 0; x < _width; x++)
            {
                if (_map[y, x] == '[')
                {
                    sum += (y * 100) + x;
                }
            }
        }

        return sum;
    }

    private (int Y, int X) MakeStep(char stepChar, int y, int x)
    {
        return stepChar switch
        {
            '>' => MoveHorizontically(stepChar, y, x, +1),
            '<' => MoveHorizontically(stepChar, y, x, -1),
            '^' => MoveVertically(stepChar, y, x, -1, 0),
            'v' => MoveVertically(stepChar, y, x, +1, 0),
            _ => throw new Exception(),
        };
    }

    private (int Y, int X) MoveHorizontically(char stepChar, int y, int x, int xStep)
    {
        if (_map[y, x + xStep] == '#')
        {
            return (y, x);
        }
        else if (_map[y, x + xStep] == '.')
        {
            _map[y, x + xStep] = '@';
            _map[y, x] = '.';
            return (y, x + xStep);
        }
        else if (_map[y, x + xStep] == '[')
        {
            int lookStep = 3;
            char nextSign = _map[y, x + lookStep];
            while (nextSign != '#')
            {
                if (nextSign == '.')
                {
                    _map[y, x] = '.';
                    _map[y, x + xStep] = '@';
                    _map[y, x + lookStep - 1] = '[';
                    _map[y, x + lookStep] = ']';
                    return (y, x + xStep);
                }

                lookStep++;
                nextSign = _map[y, x + lookStep];
            }

            return (y, x);
        }
        else if (_map[y, x + xStep] == ']')
        {
            int lookStep = -3;
            char nextSign = _map[y, x + lookStep];
            while (nextSign != '#')
            {
                if (nextSign == '.')
                {
                    _map[y, x] = '.';
                    _map[y, x + xStep] = '@';
                    _map[y, x + lookStep + 1] = ']';
                    _map[y, x + lookStep] = '[';
                    return (y, x + xStep);
                }

                lookStep--;
                nextSign = _map[y, x + lookStep];
            }

            return (y, x);
        }

        throw new Exception();
    }

    private (int Y, int X) MoveVertically(char stepChar, int y, int x, int yStep, int xStep)
    {
        if (_map[y + yStep, x + xStep] == '#')
        {
            return (y, x);
        }
        else if (_map[y + yStep, x + xStep] == '.')
        {
            _map[y + yStep, x + xStep] = '@';
            _map[y, x] = '.';
            return (y + yStep, x + xStep);
        }

        return (y, x);
    }

    private (int YStart, int XStart) GetStartingPosition()
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_map[y, x] == '@')
                {
                    return (y, x);
                }
            }
        }

        throw new Exception();
    }

    private void Init()
    {
        var lines = File.ReadAllLines(GameParameter.Path);

        List<string> mapLines = [];

        bool isMapLine = true;
        foreach (var line in lines)
        {
            if (isMapLine && string.IsNullOrEmpty(line))
            {
                isMapLine = false;
            }

            if (isMapLine)
            {
                mapLines.Add(line);
            }
            else
            {
                _moveOrders += line;
            }
        }

        _width = mapLines[0].Length * 2;
        _height = mapLines.Count;

        _map = new char[_height, _width];

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < mapLines[0].Length; x++)
            {
                switch (mapLines[y][x])
                {
                    case '@':
                        _map[y, 2 * x] = '@';
                        _map[y, (2 * x) + 1] = '.';
                        break;
                    case 'O':
                        _map[y, 2 * x] = '[';
                        _map[y, (2 * x) + 1] = ']';
                        break;
                    case '#':
                        _map[y, 2 * x] = '#';
                        _map[y, (2 * x) + 1] = '#';
                        break;
                    case '.':
                        _map[y, 2 * x] = '.';
                        _map[y, (2 * x) + 1] = '.';
                        break;
                }
            }
        }
    }

    private void PrintMap(char[,] map)
    {
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                Console.Write($"{map[i, j]} ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}