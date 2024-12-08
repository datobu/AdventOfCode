namespace Advent2024;
internal static class One
{
    public static void Run()
    {
        var input = File.ReadAllLines("../../../Common/Inputs/DayOne.txt");

        List<int> leftNumbers = new List<int>();
        List<int> rightNumbers = new List<int>();

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
                distance = distance * -1;
            }

            sum += distance;
        }

        Console.WriteLine(sum);
    }

}
