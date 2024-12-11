namespace TwentyFour.Days;

internal class Eleven
{
    public int Run()
    {
        return PartOne();

        // return PartTwo();
    }

    public int PartTwo()
    {
        var stones = DoStuff(75);
        return stones.Count;
    }

    public int PartOne()
    {
        var stones = DoStuff(25);

        Print(stones);

        return stones.Count;
    }

    public void Print(List<long> stones)
    {
        foreach (var stone in stones)
        {
            Console.Write(stone.ToString() + " ");
        }
    }

    private List<long> DoStuff(int runCount)
    {
        var input = File.ReadAllText("../../../Common/Inputs/DayEleven.txt");

        List<long> stones = input.Split(' ').Select(long.Parse).ToList();

        for (int i = 0; i < runCount; i++)
        {
            List<long> stonesAfterBlink = [];

            foreach (long stone in stones)
            {
                if (stone == 0)
                {
                    stonesAfterBlink.Add(1);
                }
                else
                {
                    string textStone = stone.ToString();
                    if (textStone.Length % 2 == 0)
                    {
                        (long newStoneLeft, long newStoneRight) = Split(textStone);
                        stonesAfterBlink.Add(newStoneLeft);
                        stonesAfterBlink.Add(newStoneRight);
                    }
                    else
                    {
                        stonesAfterBlink.Add(stone * 2024);
                    }
                }
            }

            stones = stonesAfterBlink;
        }

        return stones;
    }

    private (long NewStoneLeft, long NewStoneRight) Split(string textStone)
    {
        int halfLength = textStone.Length / 2;

        long left = long.Parse(textStone[..halfLength]);
        long right = int.Parse(textStone[halfLength..]);

        return (left, right);
    }
}