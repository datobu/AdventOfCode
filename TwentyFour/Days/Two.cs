namespace TwentyFour.Days;

internal static class Two
{
    public static void Run()
    {
        //PartOne();
        PartTwo();
    }

#pragma warning disable IDE0051 // Remove unused private members
    private static void PartOne()
#pragma warning restore IDE0051 // Remove unused private members
    {
        var input = File.ReadAllLines("../../../Common/Inputs/DayTwo.txt");

        int validCount = 0;

        foreach (var line in input)
        {
            var numbers = line.Split(' ').Select(x => int.Parse(x)).ToArray();

            int order = 0; // 1 = ascending, 2 = descending

            bool valid = true;

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] == numbers[i + 1])
                {
                    valid = false;
                    break;
                }
                else if ((order == 0 || order == 1) && numbers[i] < numbers[i + 1])
                {
                    order = 1;
                    if (numbers[i + 1] - numbers[i] > 3)
                    {
                        valid = false;
                        break;
                    }
                }
                else if ((order == 0 || order == 2) && numbers[i] > numbers[i + 1])
                {
                    order = 2;
                    if (numbers[i] - numbers[i + 1] > 3)
                    {
                        valid = false;
                        break;
                    }
                }
                else
                {
                    valid = false;
                    break;
                }
            }
            if (valid)
            {
                validCount++;
            }
        }

        Console.WriteLine(validCount);
    }

    private static void PartTwo()
    {
        var input = File.ReadAllLines("C:\\Users\\ButDa793\\Desktop\\advent\\day_2.txt");

        int validCount = 0;

        foreach (var line in input)
        {
            List<int>? numbers = line.Split(' ').Select(x => int.Parse(x)).ToList();

            if (IsValid(numbers))
            {
                validCount++;
            }
        }

        Console.WriteLine(validCount);
    }

    private static bool IsValid(List<int> numbers)
    {
        for (int i = -1; i < numbers.Count; i++)
        {
            var copy = 0;
            if (i != -1)
            {
                copy = numbers[i];
                numbers.RemoveAt(i);
            }
            if (IsValidAscending(numbers) || IsValidDescending(numbers))
            {
                return true;
            }
            if (i != -1)
            {
                numbers.Insert(i, copy);
            }
        }

        return false;
    }

    private static bool IsValidDescending(List<int> copy)
    {
        for (int i = 0; i < copy.Count - 1; i++)
        {
            if (copy[i] == copy[i + 1] || copy[i] - copy[i + 1] > 3 || copy[i] - copy[i + 1] < 0)
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsValidAscending(List<int> copy)
    {
        for (int i = 0; i < copy.Count - 1; i++)
        {
            if (copy[i] == copy[i + 1] || copy[i + 1] - copy[i] > 3 || copy[i + 1] - copy[i] < 0)
            {
                return false;
            }
        }

        return true;
    }
}