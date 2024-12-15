namespace TwentyFour.Days;

// stark inspiriert bei https://github.com/MartinZikmund/advent-of-code/blob/main/src/AdventOfCode.Puzzles/2024/06/Part2/AoC2024Day6Part2.cs
internal class SixTwo
{
    private int _width;
    private int _height;
    private char[,] _map = null!;
    private Point _startingPoint;

    public int Run()
    {
        string[] lines = File.ReadAllLines("../../../Common/Inputs/DaySix.txt");

        _height = lines.Length;
        _width = lines[0].Length;

        _map = new char[_height, _width];

        // PrintMatrix(_wayMatrix);

        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                _map[x, y] = lines[y][x];
                if (_map[x, y] == '^')
                {
                    _startingPoint = new Point(x, y);
                }
            }
        }

        var potentialObstructions = GetPotentialObstructionPositions(_startingPoint);

        int obstructionCount = 0;
        foreach (var potentialObstruction in potentialObstructions.Except([_startingPoint]))
        {
            if (DoesGuardLoop(_startingPoint, potentialObstruction))
            {
                obstructionCount++;
            }
        }

        return obstructionCount;
    }

    private bool DoesGuardLoop(Point start, Point newObstruction)
    {
        HashSet<(Point Point, Point Direction)> visited = [];

        var currentDirection = new Point(0, -1);
        var currentPoint = start;

        while (true)
        {
            if (visited.Contains((currentPoint, currentDirection)))
            {
                return true;
            }

            visited.Add((currentPoint, currentDirection));
            var nextPosition = currentPoint + currentDirection;
            if (IsOutOfBounds(nextPosition))
            {
                break;
            }

            if (_map[nextPosition.X, nextPosition.Y] == '#' ||
                (nextPosition.X == newObstruction.X && nextPosition.Y == newObstruction.Y))
            {
                // Turn right
                currentDirection = new Point(-currentDirection.Y, currentDirection.X);
                nextPosition = currentPoint;
            }

            if (IsOutOfBounds(nextPosition))
            {
                break;
            }

            currentPoint = nextPosition;
        }

        return false;
    }

    private HashSet<Point> GetPotentialObstructionPositions(Point start)
    {
        HashSet<Point> visited = [];

        var currentDirection = new Point(0, -1);
        var currentPoint = start;
        while (true)
        {
            visited.Add(currentPoint);
            var nextPosition = currentPoint + currentDirection;
            if (IsOutOfBounds(nextPosition))
            {
                break;
            }

            if (_map[nextPosition.X, nextPosition.Y] == '#')
            {
                // Turn right
                currentDirection = new Point(-currentDirection.Y, currentDirection.X);
                nextPosition = currentPoint;
            }

            if (IsOutOfBounds(nextPosition))
            {
                break;
            }

            currentPoint = nextPosition;
        }

        return visited;
    }

    private bool IsOutOfBounds(Point position)
    {
        return position.X < 0 || position.Y < 0 || position.X >= _width || position.Y >= _height;
    }
}

public record struct Point(int X, int Y)
{
    public static Point operator +(Point a, Point b) => new(a.X + b.X, a.Y + b.Y);

    public static Point operator -(Point a, Point b) => new(a.X - b.X, a.Y - b.Y);

    public static Point operator *(Point point, int multiple) => new(point.X * multiple, point.Y * multiple);

    public readonly Point Normalize() => new(X != 0 ? X / Math.Abs(X) : 0, Y != 0 ? Y / Math.Abs(Y) : 0);

    public static implicit operator Point((int X, int Y) tuple) => new(tuple.X, tuple.Y);

    public readonly int ManhattanDistance(Point b) => Math.Abs(X - b.X) + Math.Abs(Y - b.Y);
}