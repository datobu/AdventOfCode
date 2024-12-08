namespace TwentyFour.Days;

internal class Seven
{
    public static int Run()
    {
        return TaskOne();
    }

    private static int TaskOne()
    {
        var lines = File.ReadAllLines("../../../Common/Inputs/DayOne.txt");

        foreach (var line in lines)
        {
            var lineParts = line.Split(':');
            int result = int.Parse(lineParts[0]);

            List<int> resultParts = lineParts[1].Split(' ').Select(x => int.Parse(x)).ToList();


        }

        return 0;
    }
}
