namespace TwentyFour.Days;

// Neuer Ansatz: Lösungsbaum - funktioniert mit beiden beispielen... aber in der echten welt schon wieder infinite loop...
public class Sixteen
{
    public enum Direction
    {
        North,
        South,
        East,
        West,
    }

    public class PathNode(
        List<PathNode> precedessors,
        Direction direction,
        int y,
        int x,
        char[,] map,
        int score)
    {
        public List<PathNode> Precedessors { get; private set; } = precedessors;

        public Direction Direction { get; private set; } = direction;

        public int Y { get; private set; } = y;

        public int X { get; private set; } = x;

        public int Score { get; private set; } = score;

        public PathNode? North { get; private set; }

        public PathNode? South { get; private set; }

        public PathNode? East { get; private set; }

        public PathNode? West { get; private set; }

        private readonly char[,] _map = map;

        public bool IsFinalStep { get; private set; } = false;

        public void GoNextStep()
        {
            if (Direction != Direction.South)
            {
                if (Direction == Direction.North)
                {
                    GoNorth(1);
                }
                else
                {
                    GoNorth(1001);
                }
            }

            if (Direction != Direction.North)
            {
                if (Direction == Direction.South)
                {
                    GoSouth(1);
                }
                else
                {
                    GoSouth(1001);
                }
            }

            if (Direction != Direction.West)
            {
                if (Direction == Direction.East)
                {
                    GoEast(1);
                }
                else
                {
                    GoEast(1001);
                }
            }

            if (Direction != Direction.East)
            {
                if (Direction == Direction.West)
                {
                    GoWest(1);
                }
                else
                {
                    GoWest(1001);
                }
            }
        }

        private void GoNorth(int stepScore)
        {
            if (Precedessors.Any(pre => pre.X == X && pre.Y == Y - 1))
            {
                return;
            }

            switch (_map[Y - 1, X])
            {
                case 'E':
                    IsFinalStep = true;
                    Score += stepScore;
                    var newPrecedessors = new List<PathNode>(Precedessors)
                    {
                        this
                    };
                    North = new PathNode(newPrecedessors, Direction.North, Y - 1, X, _map, Score + stepScore);
                    break;

                case '.':
                    newPrecedessors = new List<PathNode>(Precedessors)
                    {
                        this
                    };
                    North = new PathNode(newPrecedessors, Direction.North, Y - 1, X, _map, Score + stepScore);
                    break;
            }
        }

        private void GoSouth(int stepScore)
        {
            if (Precedessors.Any(pre => pre.X == X && pre.Y == Y + 1))
            {
                return;
            }

            switch (_map[Y + 1, X])
            {
                case 'E':
                    IsFinalStep = true;
                    Score += stepScore;
                    var newPrecedessors = new List<PathNode>(Precedessors)
                    {
                        this
                    };
                    South = new PathNode(newPrecedessors, Direction.South, Y + 1, X, _map, Score + stepScore);
                    break;

                case '.':
                    newPrecedessors = new List<PathNode>(Precedessors)
                    {
                        this
                    };
                    South = new PathNode(newPrecedessors, Direction.South, Y + 1, X, _map, Score + stepScore);
                    break;
            }
        }

        private void GoEast(int stepScore)
        {
            if (Precedessors.Any(pre => pre.X == X + 1 && pre.Y == Y))
            {
                return;
            }

            switch (_map[Y, X + 1])
            {
                case 'E':
                    IsFinalStep = true;
                    Score += stepScore;
                    var newPrecedessors = new List<PathNode>(Precedessors)
                    {
                        this
                    };
                    East = new PathNode(newPrecedessors, Direction.East, Y, X + 1, _map, Score + stepScore);
                    break;

                case '.':
                    newPrecedessors = new List<PathNode>(Precedessors)
                    {
                        this
                    };
                    East = new PathNode(newPrecedessors, Direction.East, Y, X + 1, _map, Score + stepScore);
                    break;
            }
        }

        private void GoWest(int stepScore)
        {
            if (Precedessors.Any(pre => pre.X == X - 1 && pre.Y == Y))
            {
                return;
            }

            switch (_map[Y, X - 1])
            {
                case 'E':
                    IsFinalStep = true;
                    Score += stepScore;
                    var newPrecedessors = new List<PathNode>(Precedessors)
                    {
                        this
                    };
                    West = new PathNode(newPrecedessors, Direction.West, Y, X - 1, _map, Score + stepScore);
                    break;

                case '.':
                    newPrecedessors = new List<PathNode>(Precedessors)
                    {
                        this
                    };
                    West = new PathNode(newPrecedessors, Direction.West, Y, X - 1, _map, Score + stepScore);
                    break;
            }
        }
    }

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

        (int y, int x) = GetStartingPosition();

        var startNode = new PathNode(new List<PathNode>(), Direction.East, y, x, _map, 0);

        List<PathNode> currentNodeList = [];
        currentNodeList.Add(startNode);

        currentNodeList.ForEach(node => node.GoNextStep());

        int lowestScore = -1;

        while (currentNodeList.Any(node => node.North != null || node.South != null || node.East != null || node.West != null))
        {
            List<PathNode> innerCurrentNodeList = [];

            foreach (var node in currentNodeList)
            {
                if (node.North != null)
                {
                    if (node.IsFinalStep)
                    {
                        if (lowestScore == -1 || node.Score < lowestScore)
                        {
                            lowestScore = node.Score;
                        }
                    }

                    innerCurrentNodeList.Add(node.North);
                }

                if (node.South != null)
                {
                    if (node.IsFinalStep)
                    {
                        if (lowestScore == -1 || node.Score < lowestScore)
                        {
                            lowestScore = node.Score;
                        }
                    }

                    innerCurrentNodeList.Add(node.South);
                }

                if (node.East != null)
                {
                    if (node.IsFinalStep)
                    {
                        if (lowestScore == -1 || node.Score < lowestScore)
                        {
                            lowestScore = node.Score;
                        }
                    }

                    innerCurrentNodeList.Add(node.East);
                }

                if (node.West != null)
                {
                    if (node.IsFinalStep)
                    {
                        if (lowestScore == -1 || node.Score < lowestScore)
                        {
                            lowestScore = node.Score;
                        }
                    }

                    innerCurrentNodeList.Add(node.West);
                }
            }

            innerCurrentNodeList.ForEach(node => node.GoNextStep());
            currentNodeList = new List<PathNode>(innerCurrentNodeList);
        }

        foreach (var node in currentNodeList)
        {
            lowestScore = GetLowestScore(node, lowestScore);
        }

        return lowestScore;
    }

    private static int GetLowestScore(PathNode node, int lowestScore)
    {
        if (node != null && node.IsFinalStep)
        {
            if (lowestScore == -1 || node.Score < lowestScore)
            {
                lowestScore = node.Score;
            }
        }

        return lowestScore;
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
}