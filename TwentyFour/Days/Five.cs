namespace TwentyFour.Days;

internal class Five
{
    private readonly List<Tuple<int, int>> _rules = [];
    private readonly List<List<int>> _pages = [];

    public int Run()
    {
        Init();

        return PartTwo();

        // return PartOne();
    }

#pragma warning disable IDE0051 // Remove unused private members
    private int PartOne()
#pragma warning restore IDE0051 // Remove unused private members
    {
        int sum = 0;

        foreach (var page in _pages)
        {
            if (IsValid(page))
            {
                sum += page[Convert.ToInt32(Math.Floor(page.Count / 2.0))];
            }
        }

        return sum;
    }

    private int PartTwo()
    {
        int sum = 0;

        foreach (var page in _pages)
        {
            if (!FixIfInvalid(page))
            {
                sum += page[Convert.ToInt32(Math.Floor(page.Count / 2.0))];
            }
        }

        return sum;
    }

    private bool FixIfInvalid(List<int> page)
    {
        bool wasValid = true;
        bool unchanged = true;

        int i = 0;

        while (i < page.Count)
        {
            int pageNumberEarly = page[i];

            unchanged = true;

            for (int j = i + 1; j < page.Count; j++)
            {
                int pageNumberLate = page[j];
                if (_rules.Any(x => x.Item1 == pageNumberLate && x.Item2 == pageNumberEarly))
                {
                    (page[j], page[i]) = (page[i], page[j]);
                    wasValid = false;
                    unchanged = false;
                }
            }

            if (unchanged)
            {
                i++;
            }
        }

        return wasValid;
    }

    private bool IsValid(List<int> page)
    {
        for (int i = 0; i < page.Count; i++)
        {
            int pageNumberEarly = page[i];

            for (int j = i + 1; j < page.Count; j++)
            {
                int pageNumberLate = page[j];
                if (_rules.Any(x => x.Item1 == pageNumberLate && x.Item2 == pageNumberEarly))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void Init()
    {
        string[] lines = File.ReadAllLines("../../../Common/Inputs/DayFive.txt");

        bool rulePart = true;

        foreach (string line in lines)
        {
            if (rulePart)
            {
                if (line == string.Empty)
                {
                    rulePart = false;
                    continue;
                }

                AddRule(line);
                continue;
            }

            _pages.Add(line.Split(',').Select(x => int.Parse(x)).ToList());
        }
    }

    private void AddRule(string line)
    {
        var numbers = line.Split('|');
        _rules.Add(new Tuple<int, int>(int.Parse(numbers[0]), int.Parse(numbers[1])));
    }
}