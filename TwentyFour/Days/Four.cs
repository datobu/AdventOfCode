namespace TwentyFour.Days;

internal class Four
{
    public enum HorizontalDirection { Left, Right }
    public enum VerticalDirection { Up, Down }

    internal int Run()
    {
        return PartOne();
    }

    private int _numberOfLines = 0;
    private int _numberOfColumns = 0;
    private char[,] _matrix = null!;

    private int PartOne()
    {
        string[] lines = File.ReadAllLines("../../../Common/Inputs/DayFour.txt");

        _numberOfLines = lines.Length;
        _numberOfColumns = lines[0].Length;

        _matrix = new char[_numberOfLines, _numberOfColumns];

        FillMatrix(lines);

        int counter = 0;

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
                    GetPossibleDirections(line,col);

                if (possibleHorizontalDirections.Contains(HorizontalDirection.Left))
                {
                    if (CheckDirection(line, col - 1, 'M', 0, -1))
                    {
                        counter++;
                    }

                    if (possibleVerticalDirections.Contains(VerticalDirection.Up))
                    {
                        if (CheckDirection(line - 1, col - 1, 'M', -1, -1))
                        {
                            counter++;
                        }
                    }

                    if (possibleVerticalDirections.Contains(VerticalDirection.Down))
                    {
                        if (CheckDirection(line + 1, col - 1, 'M', 1, -1))
                        {
                            counter++;
                        }
                    }
                }

                if (possibleHorizontalDirections.Contains(HorizontalDirection.Right))
                {
                    if (CheckDirection(line, col + 1, 'M', 0, 1))
                    {
                        counter++;
                    }

                    if (possibleVerticalDirections.Contains(VerticalDirection.Up))
                    {
                        if (CheckDirection(line - 1, col + 1, 'M', -1, 1))
                        {
                            counter++;
                        }
                    }

                    if (possibleVerticalDirections.Contains(VerticalDirection.Down))
                    {
                        if (CheckDirection(line + 1, col + 1, 'M', 1, 1))
                        {
                            counter++;
                        }
                    }
                }

                if (possibleVerticalDirections.Contains(VerticalDirection.Up))
                {
                    if (CheckDirection(line - 1, col, 'M', -1, 0))
                    {
                        counter++;
                    }
                }

                if (possibleVerticalDirections.Contains(VerticalDirection.Down))
                {
                    if (CheckDirection(line + 1, col, 'M', 1, 0))
                    {
                        counter++;
                    }
                }
            }
        }

        return counter;
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

    private (List<HorizontalDirection> possibleHorizontalDirections, List<VerticalDirection> possibleVerticalDirections) GetPossibleDirections(int lineNumber, int colNumber)
    {
        List<HorizontalDirection> horizontalDirections = new List<HorizontalDirection>();
        List<VerticalDirection> verticalDirections = new List<VerticalDirection>();

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
            // Konvertiere die aktuelle Zeile in ein Zeichenarray
            char[] charArray = lines[lineNumber].ToCharArray();

            for (int col = 0; col < charArray.Length; col++)
            {
                _matrix[lineNumber, col] = charArray[col];
            }

            lineNumber++;
        }
    }
}