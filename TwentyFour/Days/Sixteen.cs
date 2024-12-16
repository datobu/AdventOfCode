namespace TwentyFour.Days;

// I was stuck so I asked chat gpt...
public class Sixteen
{
    private static readonly (int, int)[] Directions =
    {
        (0, 1), // Right
        (1, 0), // Down
        (0, -1), // Left
        (-1, 0) // Up
    };

    public static class GameParameter
    {
        public const string Path = "../../../Common/Inputs/DaySixteen.txt";
    }

    private int _width;
    private int _height;
    private char[,] _map = null!;

    public int PartOne()
    {
        Init();

        int score = SolveMaze(_map);

        return score;
    }

    public int SolveMaze(char[,] maze)
    {
        _height = maze.GetLength(0);
        _width = maze.GetLength(1);
        (int, int) start = (-1, -1), end = (-1, -1);

        // Locate start (S) and end (E) points
        for (int r = 0; r < _height; r++)
        {
            for (int c = 0; c < _width; c++)
            {
                if (maze[r, c] == 'S')
                {
                    start = (r, c);
                }

                if (maze[r, c] == 'E')
                {
                    end = (r, c);
                }
            }
        }

        if (start == (-1, -1) || end == (-1, -1))
        {
            throw new ArgumentException("Maze must contain start 'S' and end 'E'.");
        }

        var queue = new Queue<(int Row, int Col, int Dir, int Steps, int Score)>();
        var visited = new HashSet<(int, int, int)>();

        // Initialize BFS with all possible starting directions
        for (int i = 0; i < Directions.Length; i++)
        {
            var newRow = start.Item1 + Directions[i].Item1;
            var newCol = start.Item2 + Directions[i].Item2;

            if (maze[newRow, newCol] == '.')
            {
                if (i == 0)
                {
                    queue.Enqueue((newRow, newCol, i, 1, 1));
                }
                else
                {
                    queue.Enqueue((newRow, newCol, i, 1, 1001));
                }

                visited.Add((newRow, newCol, i));
            }
        }

        int minScore = int.MaxValue;

        while (queue.Count > 0)
        {
            var (row, col, dir, steps, score) = queue.Dequeue();

            if ((row, col) == end)
            {
                minScore = Math.Min(minScore, score);
                continue;
            }

            for (int i = 0; i < Directions.Length; i++)
            {
                var newRow = row + Directions[i].Item1;
                var newCol = col + Directions[i].Item2;
                int newScore = score + 1 + (i == dir ? 0 : 1000);

                if (maze[newRow, newCol] == '.' || maze[newRow, newCol] == 'E')
                {
                    if (!visited.Contains((newRow, newCol, i)))
                    {
                        queue.Enqueue((newRow, newCol, i, steps + 1, newScore));
                        visited.Add((newRow, newCol, i));
                    }
                }
            }
        }

        return minScore;
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

    /* private void PrintMap(char[,] map)
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
    }*/
}