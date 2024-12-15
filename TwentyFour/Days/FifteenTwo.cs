namespace TwentyFour.Days;

public class FifteenTwo
{
    public static class GameParameter
    {
        public const string Path = "../../../Common/Inputs/DayFifteen.txt";

        // public const string Path = "../../../Common/Inputs/DayFifteen.txt";
    }

    private int _width;
    private int _height;
    private string _moveOrders = string.Empty;
    private char[,] _map = null!;

    private int _i = 0;

    public int PartTwo()
    {
        Init();

        (int y, int x) = GetStartingPosition();

        Console.WriteLine("Init");
        PrintMap(_map);

        foreach (char c in _moveOrders)
        {
            (y, x) = MakeStep(c, y, x);
            _i++;

            if (_i == 4758 || _i == 4759 || _i == 4760)
            {
                PrintMap(_map, c);
            }
        }

        PrintMap(_map);

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
            '>' => MoveHorizontically(y, x, +1),
            '<' => MoveHorizontically(y, x, -1),
            '^' => MoveVertically(y, x, -1),
            'v' => MoveVertically(y, x, +1),
            _ => throw new Exception(),
        };
    }

    private (int Y, int X) MoveHorizontically(int y, int x, int xStep)
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
                    for (int i = 2; i <= lookStep; i++)
                    {
                        if (i % 2 == 0)
                        {
                            _map[y, x + i] = '[';
                        }
                        else
                        {
                            _map[y, x + i] = ']';
                        }
                    }

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

                    for (int i = -2; i >= lookStep; i--)
                    {
                        if (i % 2 == 0)
                        {
                            _map[y, x + i] = ']';
                        }
                        else
                        {
                            _map[y, x + i] = '[';
                        }
                    }

                    return (y, x + xStep);
                }

                lookStep--;
                nextSign = _map[y, x + lookStep];
            }

            return (y, x);
        }

        throw new Exception();
    }

    private (int Y, int X) MoveVertically(int y, int x, int yStep)
    {
        if (_map[y + yStep, x] == '#')
        {
            return (y, x);
        }
        else if (_map[y + yStep, x] == '.')
        {
            _map[y + yStep, x] = '@';
            _map[y, x] = '.';
            return (y + yStep, x);
        }
        else
        {
            List<Tuple<int, int, char>> boxParts = [];

            bool isLeft = true;
            if (_map[y + yStep, x] == '[')
            {
                boxParts.Add(new Tuple<int, int, char>(y + yStep, x, '['));
                boxParts.Add(new Tuple<int, int, char>(y + yStep, x + 1, ']'));
            }
            else if (_map[y + yStep, x] == ']')
            {
                isLeft = false;
                boxParts.Add(new Tuple<int, int, char>(y + yStep, x - 1, '['));
                boxParts.Add(new Tuple<int, int, char>(y + yStep, x, ']'));
            }

            bool movingIsPossible = MoveEverythingIfPossible(boxParts, y + yStep, yStep);

            if (movingIsPossible)
            {
                _map[y + yStep, x] = '@';
                _map[y, x] = '.';
                if (isLeft)
                {
                    _map[y + yStep, x + 1] = '.';
                }
                else
                {
                    _map[y + yStep, x - 1] = '.';
                }

                return (y + yStep, x);
            }
        }

        return (y, x);
    }

    private bool MoveEverythingIfPossible(List<Tuple<int, int, char>> boxParts, int y, int direction)
    {
        var currentYBoxParts = boxParts.Where(b => b.Item1 == y);
        bool isBlocked = currentYBoxParts.Any(x => _map[y + direction, x.Item2] == '#');
        if (isBlocked)
        {
            return false;
        }

        bool isFree = currentYBoxParts.All(x => _map[y + direction, x.Item2] == '.');
        if (isFree)
        {
            MoveAll(boxParts, direction);
            return true;
        }

        List<Tuple<int, int, char>> allBoxParts = new(boxParts);

        foreach (var boxPart in currentYBoxParts)
        {
            if (_map[y + direction, boxPart.Item2] == '[')
            {
                if (!allBoxParts.Any(x => x.Item1 == y + direction && x.Item2 == boxPart.Item2))
                {
                    allBoxParts.Add(new Tuple<int, int, char>(y + direction, boxPart.Item2, '['));
                    allBoxParts.Add(new Tuple<int, int, char>(y + direction, boxPart.Item2 + 1, ']'));
                }
            }
            else if (_map[y + direction, boxPart.Item2] == ']')
            {
                if (!allBoxParts.Any(x => x.Item1 == y + direction && x.Item2 == boxPart.Item2))
                {
                    allBoxParts.Add(new Tuple<int, int, char>(y + direction, boxPart.Item2 - 1, '['));
                    allBoxParts.Add(new Tuple<int, int, char>(y + direction, boxPart.Item2, ']'));
                }
            }
        }

        return MoveEverythingIfPossible(allBoxParts, y + direction, direction);
    }

    private void MoveAll(List<Tuple<int, int, char>> boxParts, int direction)
    {
        if (direction < 0)
        {
            foreach (var boxPart in boxParts.OrderBy(x => x.Item1))
            {
                _map[boxPart.Item1 + direction, boxPart.Item2] = boxPart.Item3;
                _map[boxPart.Item1, boxPart.Item2] = '.';
            }
        }
        else
        {
            foreach (var boxPart in boxParts.OrderByDescending(x => x.Item1))
            {
                _map[boxPart.Item1 + direction, boxPart.Item2] = boxPart.Item3;
                _map[boxPart.Item1, boxPart.Item2] = '.';
            }
        }
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

    private void PrintMap(char[,] map, char c = ' ')
    {
        Console.WriteLine();
        Console.WriteLine($"{_i} / {_moveOrders.Length}: {c}");

        for (int i = -1; i < _height; i++)
        {
            for (int j = -1; j < _width; j++)
            {
                if (i == -1)
                {
                    if (j == -1)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        string formattedNumber = j.ToString("D2");
                        Console.Write($"{formattedNumber}");
                    }
                }
                else
                {
                    if (j == -1)
                    {
                        string formattedNumber = i.ToString("D2");
                        Console.Write($"{formattedNumber} ");
                    }
                    else
                    {
                        if (map[i, j] == '@')
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"@ ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write($"{map[i, j]} ");
                        }
                    }
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}