namespace TwentyFour.Days;

internal class Four
{
    public enum HorizontalDirection { Left, Right }
    public enum VerticalDirection { Up, Down }

    private int _numberOfLines = 0;
    private int _numberOfColumns = 0;
    private char[,] _matrix = null!;
    private int _counter = 0;

    internal int Run()
    {
        string[] lines = InitMatrix();

        FillMatrix(lines);

        return PartTwo();
    }
    private string[] InitMatrix()
    {
        string[] lines = File.ReadAllLines("../../../Common/Inputs/DayFour.txt");

        _numberOfLines = lines.Length;
        _numberOfColumns = lines[0].Length;

        _matrix = new char[_numberOfLines, _numberOfColumns];
        return lines;
    }

#pragma warning disable IDE0051 // Remove unused private members
    private int PartOne()
#pragma warning restore IDE0051 // Remove unused private members
    {
        for (int line = 0; line < _numberOfLines; line++)
        {
            for (int col = 0; col < _numberOfColumns; col++)
            {
                char currentChar = _matrix[line, col];

                if (currentChar != 'X')
                {
                    continue;
                }

                (List<HorizontalDirection> possibleHorizontalDirections, List<VerticalDirection> possibleVerticalDirections) =
                    GetPossibleDirections(line, col);

                // simplified with chatgpt:
                var directions =
                    new (int rowStep, int colStep, HorizontalDirection? horizontalDirection, VerticalDirection? verticalDirection)[]
                {
                    (0, -1, HorizontalDirection.Left, null),  // Left
                    (0, 1, HorizontalDirection.Right, null),  // Right
                    (-1, 0, null, VerticalDirection.Up),     // Up
                    (1, 0, null, VerticalDirection.Down),    // Down
                    (-1, -1, HorizontalDirection.Left, VerticalDirection.Up),   // Left-Up
                    (1, -1, HorizontalDirection.Left, VerticalDirection.Down),  // Left-Down
                    (-1, 1, HorizontalDirection.Right, VerticalDirection.Up),  // Right-Up
                    (1, 1, HorizontalDirection.Right, VerticalDirection.Down)  // Right-Down
                };

                foreach (var (rowStep, colStep, horDir, verDir) in directions)
                {
                    if ((horDir == null || possibleHorizontalDirections.Contains(horDir.Value)) &&
                        (verDir == null || possibleVerticalDirections.Contains(verDir.Value)))
                    {
                        if (CheckDirection(line + rowStep, col + colStep, 'M', rowStep, colStep))
                        {
                            _counter++;
                        }
                    }
                }
            }
        }

        return _counter;
    }

    private int PartTwo()
    {
        for (int line = 1; line < _numberOfLines - 1; line++)
        {
            for (int col = 1; col < _numberOfColumns - 1; col++)
            {
                if (_matrix[line, col] != 'A')
                {
                    continue;
                }

                if (_matrix[line - 1, col - 1] == 'M' && _matrix[line + 1, col + 1] == 'S' ||
                    _matrix[line - 1, col - 1] == 'S' && _matrix[line + 1, col + 1] == 'M')
                {
                    if (_matrix[line - 1, col + 1] == 'M' && _matrix[line + 1, col - 1] == 'S' ||
                    _matrix[line - 1, col + 1] == 'S' && _matrix[line + 1, col - 1] == 'M')
                    {
                        _counter++;
                    }
                }
            }
        }

        return _counter;
    }

    private bool CheckDirection(int line, int col, char character, int lineStep, int colStep)
    {
        if (_matrix[line, col] != character)
        {
            return false;
        }

        char nextChar = character switch
        {
            'M' => 'A',
            'A' => 'S',
            _ => '\0'
        };

        if (nextChar != '\0')
        {
            return CheckDirection(line + lineStep, col + colStep, nextChar, lineStep, colStep);
        }

        return true;
    }

    private (List<HorizontalDirection> possibleHorizontalDirections, List<VerticalDirection> possibleVerticalDirections) GetPossibleDirections(
        int lineNumber,
        int colNumber)
    {
        List<HorizontalDirection> horizontalDirections = [];
        List<VerticalDirection> verticalDirections = [];

        foreach (HorizontalDirection horDir in Enum.GetValues(typeof(HorizontalDirection)))
        {
            if (horDir == HorizontalDirection.Left && colNumber - 3 >= 0)
            {
                horizontalDirections.Add(horDir);
            }
            else if (horDir == HorizontalDirection.Right && colNumber + 3 < _numberOfColumns)
            {
                horizontalDirections.Add(horDir);
            }
        }

        foreach (VerticalDirection verDir in Enum.GetValues(typeof(VerticalDirection)))
        {
            if (verDir == VerticalDirection.Up && lineNumber - 3 >= 0)
            {
                verticalDirections.Add(verDir);
            }
            else if (verDir == VerticalDirection.Down && lineNumber + 3 < _numberOfLines)
            {
                verticalDirections.Add(verDir);
            }
        }

        return (horizontalDirections, verticalDirections);
    }

    private void FillMatrix(string[] lines)
    {
        int lineNumber = 0;

        while (lineNumber < lines.Length)
        {
            char[] charArray = lines[lineNumber].ToCharArray();

            for (int col = 0; col < charArray.Length; col++)
            {
                _matrix[lineNumber, col] = charArray[col];
            }

            lineNumber++;
        }
    }
}