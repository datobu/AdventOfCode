namespace TwentyFour.Days;

internal class Ten
{
    private readonly List<Trailhead> _trailheads = [];
    private int _numberOfRows = 0;
    private int _numberOfColumns = 0;
    private int[,] _matrix = null!;

    public class Coordinate(int row, int col)
    {
        public int Row { get; private set; } = row;

        public int Column { get; private set; } = col;
    }

    public class Trailhead(Coordinate ownPlace)
    {
        public Coordinate OwnPlace { get; set; } = ownPlace;

        public List<Coordinate> StartPoints { get; private set; } = [];
    }

    public int Run()
    {
        return PartOne();

        // PartTwo();
    }

    private int PartOne()
    {
        var input = File.ReadAllLines("../../../Common/Inputs/DayTen.txt");

        _numberOfRows = input.Length;
        _numberOfColumns = input[0].Length;

        _matrix = new int[_numberOfRows, _numberOfColumns];

        int x = 0;

        foreach (var row in input)
        {
            int y = 0;
            foreach (char c in row)
            {
                _matrix[x, y] = int.Parse(c.ToString());
                y++;
            }

            x++;
        }

        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                if (_matrix[row, col] == 0)
                {
                    CountTrailheads(new Coordinate(row, col), new Coordinate(row, col), 1);
                }
            }
        }

        int sum = 0;

        foreach (var trailhead in _trailheads)
        {
            sum += trailhead.StartPoints.Count;
        }

        return sum;
    }

    private void CountTrailheads(Coordinate start, Coordinate current, int nextStep)
    {
        Coordinate next = new Coordinate(current.Row + 1, current.Column);

        if (next.Row < _numberOfRows && next.Row >= 0)
        {
            if (_matrix[next.Row, current.Column] == nextStep)
            {
                if (nextStep == 9)
                {
                    AddTrailheadAndStartPoints(start, next);

                    return;
                }

                CountTrailheads(start, new Coordinate(next.Row, current.Column), nextStep + 1);
            }
        }

        next = new Coordinate(current.Row - 1, current.Column);

        if (next.Row < _numberOfRows && next.Row >= 0)
        {
            if (_matrix[next.Row, current.Column] == nextStep)
            {
                if (nextStep == 9)
                {
                    AddTrailheadAndStartPoints(start, next);

                    return;
                }

                CountTrailheads(start, new Coordinate(next.Row, current.Column), nextStep + 1);
            }
        }

        next = new Coordinate(current.Row, current.Column + 1);

        if (next.Column < _numberOfColumns && next.Column >= 0)
        {
            if (_matrix[current.Row, next.Column] == nextStep)
            {
                if (nextStep == 9)
                {
                    AddTrailheadAndStartPoints(start, next);

                    return;
                }

                CountTrailheads(start, new Coordinate(current.Row, next.Column), nextStep + 1);
            }
        }

        next = new Coordinate(current.Row, current.Column - 1);

        if (next.Column < _numberOfColumns && next.Column >= 0)
        {
            if (_matrix[current.Row, next.Column] == nextStep)
            {
                if (nextStep == 9)
                {
                    AddTrailheadAndStartPoints(start, next);

                    return;
                }

                CountTrailheads(start, new Coordinate(current.Row, next.Column), nextStep + 1);
            }
        }
    }

    private void AddTrailheadAndStartPoints(Coordinate start, Coordinate current)
    {
        Trailhead? trailhead = _trailheads.SingleOrDefault(x => x.OwnPlace.Row == current.Row && x.OwnPlace.Column == current.Column);
        if (trailhead == null)
        {
            AddTrailheadWithStart(start, current);
        }
        else if (!trailhead.StartPoints.Any(x => x.Row == start.Row && x.Column == start.Column))
        {
            trailhead.StartPoints.Add(start);
        }
    }

    private void AddTrailheadWithStart(Coordinate start, Coordinate current)
    {
        var trailhead = new Trailhead(current);
        trailhead.StartPoints.Add(start);
        _trailheads.Add(trailhead);
    }
}