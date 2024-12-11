namespace TwentyFour.Days;

internal class Eleven
{
    private long _sum = 0;

    public long Run()
    {
        // RunRecursive(0, 9);

        PartOne();
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
            RunRecursive(stone, runCount);
        }
    }

    private long RunRecursive(long stone, int count)
    {
        if (count == 0)
        {
            return 1;
        }

        List<long> stonesAfterBlink = [];

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

        Dictionary<long, long> stoneWithCount = [];

        foreach (var newStone in stonesAfterBlink)
        {
            if (stoneWithCount.ContainsKey(newStone))
            {
                stoneWithCount[newStone]++;
            }
            else
            {
                stoneWithCount.Add(newStone, 1);
            }
        }

        foreach (var keyValuePair in stoneWithCount)
        {
            long newValue = RunRecursive(keyValuePair.Key, count - 1);
            /*if (newValue <= 0)
            {
                Console.WriteLine($"{newValue} / {_sum}");
            }*/

            _sum += keyValuePair.Value * newValue;
        }

        return 0;
    }

    private (long NewStoneLeft, long NewStoneRight) Split(string textStone)
    {
        int halfLength = textStone.Length / 2;

        long left = long.Parse(textStone[..halfLength]);
        long right = int.Parse(textStone[halfLength..]);

        return (left, right);
    }
}