namespace TwentyFour.Days;

internal class Eleven
{
    private long _sum = 0;

    public long Run()
    {
        PartTwo();
        return _sum;
    }

    public void PartTwo()
    {
        DoStuff(75);
    }

    public void PartOne()
    {
        DoStuff(25);

        // Print(stones);
    }

    public void Print(List<long> stones)
    {
        foreach (var stone in stones)
        {
            Console.Write(stone.ToString() + " ");
        }
    }

    private void DoStuff(int runCount)
    {
        var input = File.ReadAllText("../../../Common/Inputs/DayEleven.txt");

        List<long> stones = input.Split(' ').Select(long.Parse).ToList();

        foreach (var stone in stones)
        {
            var list = new List<long>
            {
                stone
            };
            RunRecursive(list, runCount);
        }
    }

    private void RunRecursive(List<long> stones, int count)
    {
        if (count == 0)
        {
            _sum += stones.Count;
            return;
        }

        List<long> stonesAfterBlink = [];

        foreach (var stone in stones)
        {
            if (stone == 0)
            {
                stonesAfterBlink.Add(1);
            }
            else if (stone.ToString().Length % 2 == 0)
            {
                (long newStoneLeft, long newStoneRight) = Split(stone.ToString());
                stonesAfterBlink.Add(newStoneLeft);
                stonesAfterBlink.Add(newStoneRight);
            }
            else
            {
                stonesAfterBlink.Add(stone * 2024);
            }
        }

        RunRecursive(stonesAfterBlink, count - 1);
    }

    private (long NewStoneLeft, long NewStoneRight) Split(string textStone)
    {
        int halfLength = textStone.Length / 2;

        long left = long.Parse(textStone[..halfLength]);
        long right = int.Parse(textStone[halfLength..]);

        return (left, right);
    }
}