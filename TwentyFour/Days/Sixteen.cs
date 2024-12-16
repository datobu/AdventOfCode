namespace TwentyFour.Days;

public class Sixteen
{
    public enum Direction
    {
        North,
        South,
        West,
        East
    }

    public static class GameParameter
    {
        public const string Path = "../../../Common/Inputs/DaySixteen-Example.txt";
    }

    private int _width;
    private int _height;
    private char[,] _map = null!;

    public int PartOne()
    {
        Init();

        (int y, int x) = GetStartingPosition();

        int score = GetCheapestPathScore(y, x, Direction.East, 0, new List<Tuple<int, int>>());

        return score;
    }

    private int GetCheapestPathScore(int y, int x, Direction direction, int score, List<Tuple<int, int>> pathCoordinates)
    {
        if (pathCoordinates.Any(c => c.Item1 == y && c.Item2 == x))
        {
            return -1;
        }

        pathCoordinates.Add(Tuple.Create(y, x));

        switch (direction)
        {
            case Direction.North:
                int scoreNorth = ContinuePath(y - 1, x, score, _map[y - 1, x], Direction.North, 1, pathCoordinates);

                int scoreWest = ContinuePath(y, x - 1, score, _map[y, x - 1], Direction.West, 1001, pathCoordinates);

                int scoreEast = ContinuePath(y, x + 1, score, _map[y, x + 1], Direction.East, 1001, pathCoordinates);

                return LowestSore(scoreNorth, scoreWest, scoreEast);

            case Direction.South:
                int scoreSouth = ContinuePath(y + 1, x, score, _map[y + 1, x], Direction.South, 1, pathCoordinates);

                scoreWest = ContinuePath(y, x - 1, score, _map[y, x - 1], Direction.West, 1001, pathCoordinates);

                scoreEast = ContinuePath(y, x + 1, score, _map[y, x + 1], Direction.East, 1001, pathCoordinates);

                return LowestSore(scoreSouth, scoreWest, scoreEast);

            case Direction.West:
                scoreWest = ContinuePath(y, x - 1, score, _map[y, x - 1], Direction.West, 1, pathCoordinates);

                scoreNorth = ContinuePath(y - 1, x, score, _map[y - 1, x], Direction.North, 1001, pathCoordinates);

                scoreSouth = ContinuePath(y + 1, x, score, _map[y + 1, x], Direction.South, 1001, pathCoordinates);

                return LowestSore(scoreSouth, scoreWest, scoreNorth);

            case Direction.East:
                scoreEast = ContinuePath(y, x + 1, score, _map[y, x + 1], Direction.East, 1, pathCoordinates);

                scoreNorth = ContinuePath(y - 1, x, score, _map[y - 1, x], Direction.North, 1001, pathCoordinates);

                scoreSouth = ContinuePath(y + 1, x, score, _map[y + 1, x], Direction.South, 1001, pathCoordinates);

                return LowestSore(scoreSouth, scoreEast, scoreNorth);

            default:
                throw new Exception();
        }
    }

    private int ContinuePath(int nextY, int nextX, int score, char directionValue, Direction direction, int scorePoints, List<Tuple<int, int>> pathCoordinates)
    {
        if (directionValue == 'S')
        {
            pathCoordinates.Clear();
            return -1;
        }
        else if (directionValue == 'E')
        {
            pathCoordinates.Clear();
            return score + scorePoints;
        }
        else if (directionValue == '#')
        {
            return -1;
        }
        else if (directionValue == '.')
        {
            return GetCheapestPathScore(nextY, nextX, direction, score + scorePoints, pathCoordinates);
        }

        throw new Exception();
    }

    private static int LowestSore(int scoreNorth, int scoreWest, int scoreEast)
    {
        List<int> scores = new List<int>() { scoreEast, scoreNorth, scoreWest };

        var possibleScores = scores.Where(num => num != -1).ToList();

        if (!possibleScores.Any())
        {
            return -1;
        }

        return possibleScores.Min();
    }

    private (int YStart, int XStart) GetStartingPosition()
    {
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (_map[y, x] == 'S')
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

        foreach (var line in lines)
        {
            mapLines.Add(line);
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