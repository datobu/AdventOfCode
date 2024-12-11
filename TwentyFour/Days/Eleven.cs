namespace TwentyFour.Days;

internal class Eleven
{
    public int Run()
    {
        return PartOne();

        // PartTwo();
    }

    private int PartOne()
    {
        var input = File.ReadAllText("../../../Common/Inputs/DayEleven.txt");

        List<long> stones = input.Split(' ').Select(long.Parse).ToList();

        for (int i = 0; i < 9; i++)
        {
            List<long> stonesAfterBlink = [];

            foreach (long stone in stones)
            {
                string textStone = stone.ToString();

                if (stone == 0)
                {
                    stonesAfterBlink.Add(1);
                }
                else if (textStone.Length % 2 == 0)
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

            stones = stonesAfterBlink;
        }

        foreach (long stone in stones)
        {
            Console.Write($"{stone} ");
        }

        Console.WriteLine(stones.Count);
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