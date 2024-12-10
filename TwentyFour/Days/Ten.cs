namespace TwentyFour.Days;

internal class Ten
{
    private int _numberOfRows = 0;
    private int _numberOfColumns = 0;
    private int[,] _matrix = null!;

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

        int sum = 0;

        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                if (_matrix[row, col] == 0)
                {
                    sum += FollowPath(row, col, 1, sum);
                }
            }
        }

        return sum;
    }

    private int FollowPath(int row, int col, int nextStep, int sum)
    {
        if (row - 1 >= 0)
        {
            if (_matrix[row - 1, col] == nextStep)
            {
                if (nextStep == 9)
                {
                    return 1;
                }

                sum += FollowPath(row - 1, col, nextStep + 1, sum);
            }
        }

        if (row + 1 < _numberOfRows)
        {
            if (_matrix[row + 1, col] == nextStep)
            {
                if (nextStep == 9)
                {
                    return 1;
                }

                sum += FollowPath(row + 1, col, nextStep + 1, sum);
            }
        }

        if (col - 1 >= 0)
        {
            if (_matrix[row, col - 1] == nextStep)
            {
                if (nextStep == 9)
                {
                    return 1;
                }

                sum += FollowPath(row, col - 1, nextStep + 1, sum);
            }
        }

        if (col + 1 < _numberOfColumns)
        {
            if (_matrix[row, col + 1] == nextStep)
            {
                if (nextStep == 9)
                {
                    return 1;
                }

                sum += FollowPath(row, col + 1, nextStep + 1, sum);
            }
        }

        return 0;
    }
}