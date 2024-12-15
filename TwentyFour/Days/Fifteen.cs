namespace TwentyFour.Days;

public class Fifteen
{
    public static class GameParameter
    {
        public const string Path = "../../../Common/Inputs/DayFifteen.txt";
    }

    private int _width;
    private int _height;
    private string _moveOrders = string.Empty;
    private char[,] _map = null!;

    public int PartOne()
    {
        Init();

        (int y, int x) = GetStartingPosition();

        foreach (char c in _moveOrders)
        {
            (y, x) = MakeStep(c, y, x);
        }

        PrintMap(_map);

        int sum = 0;

        for (y = 0; y < _height; y++)
        {
            for (x = 0; x < _width; x++)
            {
                if (_map[y, x] == 'O')
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
            '>' => Move(stepChar, y, x, 0, +1),
            '<' => Move(stepChar, y, x, 0, -1),
            '^' => Move(stepChar, y, x, -1, 0),
            'v' => Move(stepChar, y, x, +1, 0),
            _ => throw new Exception(),
        };
    }

    private (int Y, int X) Move(char stepChar, int y, int x, int yStep, int xStep)
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
        else if (_map[y + yStep, x + xStep] == 'O')
        {
            int lookStep = 2;
            char nextSign = Search(stepChar, y, x, lookStep);
            while (nextSign != '#')
            {
                if (nextSign == '.')
                {
                    _map[y, x] = '.';
                    _map[y + yStep, x + xStep] = '@';
                    PlaceObstacle(stepChar, y, x, lookStep);
                    return (y + yStep, x + xStep);
                }

                lookStep++;
                nextSign = Search(stepChar, y, x, lookStep);
            }

            return (y, x);
        }

        throw new Exception();
    }

    private void PlaceObstacle(char stepChar, int y, int x, int lookStep)
    {
        switch (stepChar)
        {
            case '>':
                _map[y, x + lookStep] = 'O';
                break;
            case '<':
                _map[y, x - lookStep] = 'O';
                break;
            case '^':
                _map[y - lookStep, x] = 'O';
                break;
            case 'v':
                _map[y + lookStep, x] = 'O';
                break;
        }
    }

    private char Search(char stepChar, int y, int x, int lookStep)
    {
        return stepChar switch
        {
            '>' => _map[y, x + lookStep],
            '<' => _map[y, x - lookStep],
            '^' => _map[y - lookStep, x],
            'v' => _map[y + lookStep, x],
            _ => throw new Exception(),
        };
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

        _width = mapLines[0].Length;
        _height = mapLines.Count;

        _map = new char[_height, _width];

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                _map[y, x] = mapLines[y][x];
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