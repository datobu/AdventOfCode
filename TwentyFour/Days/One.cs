namespace TwentyFour.Days;

internal static class One
{
    public static void PartOne()
    {
        var input = File.ReadAllLines("../../../Common/Inputs/DayOne.txt");

        List<int> leftNumbers = [];
        List<int> rightNumbers = [];

        foreach (var line in input)
        {
            var left = line.Split("   ")[0];
            var right = line.Split("   ")[1];

            leftNumbers.Add(int.Parse(left));
            rightNumbers.Add(int.Parse(right));
        }

        leftNumbers.Sort();
        rightNumbers.Sort();

        var sum = 0;

        foreach (var leftNumber in leftNumbers)
        {
            var rightNumber = rightNumbers[0];
            rightNumbers.RemoveAt(0);

            int distance = leftNumber - rightNumber;

            if (distance < 0)
            {
                distance *= -1;
            }

            sum += distance;
        }

        Console.WriteLine(sum);
    }

    public static void Run()
    {
        // PartOne();

        PartTwo();
    }

    private static void PartTwo()
    {
        var input = File.ReadAllLines("../../../Common/Inputs/DayOne.txt");

        List<int> leftNumbers = [];
        List<int> rightNumbers = [];

        foreach (var line in input)
        {
            var left = line.Split("   ")[0];
            var right = line.Split("   ")[1];

            leftNumbers.Add(int.Parse(left));
            rightNumbers.Add(int.Parse(right));
        }

        var sum = 0;

        foreach (var leftNumber in leftNumbers)
        {
            int count = rightNumbers.Count(x => x == leftNumber);

            sum += count * leftNumber;
        }

        Console.WriteLine(sum);
    }
}