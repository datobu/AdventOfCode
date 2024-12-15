namespace TwentyFour.Days;

// completely alone
internal class Eight
{
    private int _numberOfRows = 0;
    private int _numberOfColumns = 0;
    private char[,] _matrix = null!;
    private char[,] _solutionMatrix = null!;

    public int Run()
    {
        InitMatrix();

        // PartOne();

        return PartTwo();
    }

    public int PartOne()
    {
        // Run through Matrix, until one char != . and char != '#' is reached
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                if (_matrix[row, col] != '.' && _matrix[row, col] != '#')
                {
                    // scan the map for all other coordinates with this same char in a list
                    List<Tuple<int, int>> coordinates = FindAllCoordinates(row, col, _matrix[row, col]);

                    // based on the coordinates calculate where all antinodes are to be placed
                    PlaceAntiNodes(coordinates);
                }
            }
        }

        PrintMatrix(_solutionMatrix);

        return CountAntiNodes();
    }

    public int PartTwo()
    {
        // Run through Matrix, until one char != . and char != '#' is reached
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                if (_matrix[row, col] != '.' && _matrix[row, col] != '#')
                {
                    // scan the map for all other coordinates with this same char in a list
                    List<Tuple<int, int>> coordinates = FindAllCoordinates(row, col, _matrix[row, col]);

                    // based on the coordinates calculate where all antinodes are to be placed
                    PlaceEndlessAntiNodes(coordinates);
                }
            }
        }

        PrintMatrix(_solutionMatrix);

        return CountAllNodes();
    }

    private int CountAllNodes()
    {
        int count = 0;
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                if (_solutionMatrix[row, col] != '.')
                {
                    count++;
                }
            }
        }

        return count;
    }

    private int CountAntiNodes()
    {
        int count = 0;
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                if (_solutionMatrix[row, col] == '#')
                {
                    count++;
                }
            }
        }

        return count;
    }

    private void PlaceAntiNodes(List<Tuple<int, int>> coordinates)
    {
        // place the anti nodes, only when they are on a '.' field, mark them with '#' in the solution Matrix
        for (int i = 0; i < coordinates.Count; i++)
        {
            Tuple<int, int> cursorOne = coordinates[i];

            for (int j = i + 1; j < coordinates.Count; j++)
            {
                Tuple<int, int> cursorTwo = coordinates[j];

                int rowDistance = cursorTwo.Item1 - cursorOne.Item1;
                int colDistance = cursorTwo.Item2 - cursorOne.Item2;

                int antiNodeRow = cursorOne.Item1 - rowDistance;
                int antiNodeCol = cursorOne.Item2 - colDistance;
                PlaceAntiNodeIfValid(antiNodeRow, antiNodeCol);

                antiNodeRow = cursorTwo.Item1 + rowDistance;
                antiNodeCol = cursorTwo.Item2 + colDistance;
                PlaceAntiNodeIfValid(antiNodeRow, antiNodeCol);
            }
        }
    }

    private void PlaceEndlessAntiNodes(List<Tuple<int, int>> coordinates)
    {
        // place the anti nodes, only when they are on a '.' field, mark them with '#' in the solution Matrix
        for (int i = 0; i < coordinates.Count; i++)
        {
            Tuple<int, int> cursorOne = coordinates[i];

            for (int j = i + 1; j < coordinates.Count; j++)
            {
                Tuple<int, int> cursorTwo = coordinates[j];

                int rowDistance = cursorTwo.Item1 - cursorOne.Item1;
                int colDistance = cursorTwo.Item2 - cursorOne.Item2;

                int antiNodeRow = cursorOne.Item1 - rowDistance;
                int antiNodeCol = cursorOne.Item2 - colDistance;

                while (antiNodeRow >= 0 && antiNodeCol >= 0)
                {
                    PlaceAntiNodeIfValid(antiNodeRow, antiNodeCol);

                    antiNodeRow -= rowDistance;
                    antiNodeCol -= colDistance;
                }

                antiNodeRow = cursorTwo.Item1 + rowDistance;
                antiNodeCol = cursorTwo.Item2 + colDistance;

                while (antiNodeRow < _numberOfRows && antiNodeCol < _numberOfColumns)
                {
                    PlaceAntiNodeIfValid(antiNodeRow, antiNodeCol);

                    antiNodeRow += rowDistance;
                    antiNodeCol += colDistance;
                }
            }
        }
    }

    private void PlaceAntiNodeIfValid(int antiNodeRow, int antiNodeCol)
    {
        if (antiNodeRow >= 0 && antiNodeRow < _numberOfRows)
        {
            if (antiNodeCol >= 0 && antiNodeCol < _numberOfColumns)
            {
                _solutionMatrix[antiNodeRow, antiNodeCol] = '#';
            }
        }
    }

    private List<Tuple<int, int>> FindAllCoordinates(int inputRow, int inputCol, char value)
    {
        List<Tuple<int, int>>? list = [];

        bool first = true;

        for (int row = inputRow; row < _numberOfRows; row++)
        {
            int colStart = first ? inputCol : 0;

            for (int col = colStart; col < _numberOfColumns; col++)
            {
                if (_matrix[row, col] == value)
                {
                    list.Add(Tuple.Create(row, col));
                }
            }

            first = false;
        }

        return list;
    }

    private void InitMatrix()
    {
        string[] rows = File.ReadAllLines("../../../Common/Inputs/DayEight.txt");

        _numberOfRows = rows.Length;
        _numberOfColumns = rows[0].Length;

        _matrix = new char[_numberOfRows, _numberOfColumns];

        for (int i = 0; i < _numberOfRows; i++)
        {
            for (int j = 0; j < _numberOfRows; j++)
            {
                _matrix[i, j] = rows[i][j];
            }
        }

        _solutionMatrix = (char[,])_matrix.Clone();
    }

    private void PrintMatrix(char[,] matrix)
    {
        for (int row = 0; row < _numberOfRows; row++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                Console.Write(matrix[row, col]);
            }

            Console.WriteLine();
        }
    }
}