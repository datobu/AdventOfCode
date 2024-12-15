namespace TwentyFour.Days;

internal class Six
{
    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    private int _numberOfRows = 0;
    private int _numberOfColumns = 0;
    private char[,] _matrix = null!;
    private char[,] _wayMatrix = null!;

    public int Run()
    {
        string[] rows = InitMatrix();

        FillMatrix(rows);

        _wayMatrix = (char[,])_matrix.Clone();

        // PrintMatrix(_wayMatrix);

        (int row, int col) = GetStartingPosition();

        return PartOne(row, col);
    }

    public int PartOne(int row, int col)
    {
        WalkThrough(row, col, Direction.Up);

        PrintMatrix(_wayMatrix);

        return GetXCount();
    }

    private void PrintMatrix(char[,] wayMatrix)
    {
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                Console.Write(wayMatrix[row, col]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private void WalkThrough(int row, int col, Direction direction)
    {
        int original_row = row;
        int original_col = col;

        switch (direction)
        {
            case Direction.Left:
                col--;
                break;
            case Direction.Right:
                col++;
                break;
            case Direction.Up:
                row--;
                break;
            case Direction.Down:
                row++;
                break;
        }

        if (row < 0 || col < 0 || row == _numberOfRows || col == _numberOfColumns)
        {
            return;
        }

        if (_matrix[row, col] == '#')
        {
            Direction nextDirection = direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new Exception()
            };

            WalkThrough(original_row, original_col, nextDirection);
        }
        else
        {
            _wayMatrix[original_row, original_col] = 'X';
            WalkThrough(row, col, direction);
        }
    }

    private (int X, int Y) GetStartingPosition()
    {
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                if (_matrix[row, col] == '^')
                {
                    return (row, col);
                }
            }
        }

        throw new Exception();
    }

    private int GetXCount()
    {
        int count = 1;

        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                if (_wayMatrix[row, col] == 'X')
                {
                    count++;
                }
            }
        }

        return count;
    }

    private string[] InitMatrix()
    {
        string[] rows = File.ReadAllLines("../../../Common/Inputs/DaySix-Example.txt");

        _numberOfRows = rows.Length;
        _numberOfColumns = rows[0].Length;

        _matrix = new char[_numberOfRows, _numberOfColumns];

        // _wayMatrix = new char[_numberOfRows, _numberOfColumns];

        return rows;
    }

    private void FillMatrix(string[] rows)
    {
        int rowNumber = 0;

        while (rowNumber < rows.Length)
        {
            char[] charArray = rows[rowNumber].ToCharArray();

            for (int col = 0; col < charArray.Length; col++)
            {
                _matrix[rowNumber, col] = charArray[col];
            }

            rowNumber++;
        }
    }

    private void PlaceObstacles(int row, int col, Direction direction)
    {
        int original_row = row;
        int original_col = col;

        PrintMatrix(_wayMatrix);

        switch (direction)
        {
            case Direction.Left:
                col--;
                break;
            case Direction.Right:
                col++;
                break;
            case Direction.Up:
                row--;
                break;
            case Direction.Down:
                row++;
                break;
        }

        if (row < 0 || col < 0 || row == _numberOfRows || col == _numberOfColumns)
        {
            return;
        }

        if (_wayMatrix[original_row, original_col] == 'X')
        {
            _wayMatrix[row, col] = 'O';
        }

        if (_matrix[row, col] == '#')
        {
            Direction nextDirection = direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new Exception()
            };

            PlaceObstacles(original_row, original_col, nextDirection);
        }
        else
        {
            if (_wayMatrix[original_row, original_col] != 'O')
            {
                _wayMatrix[original_row, original_col] = 'X';
            }

            PlaceObstacles(row, col, direction);
        }
    }
}