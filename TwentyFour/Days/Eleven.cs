namespace TwentyFour.Days;

// idea stolen from google / reddit
internal class Eleven
{
    private readonly int _runCount = 75;

    public long Run()
    {
        Dictionary<long, long> dict = [];

        dict.Add(5910927, 1);
        dict.Add(0, 1);
        dict.Add(1, 1);
        dict.Add(47, 1);
        dict.Add(261223, 1);
        dict.Add(94788, 1);
        dict.Add(545, 1);
        dict.Add(7771, 1);

        // 5910927 0 1 47 261223 94788 545 7771

        for (int i = 0; i < _runCount; i++)
        {
            dict = RunThroughDict(dict);
        }

        long sum = 0;

        foreach (var i in dict)
        {
            sum += i.Value;
        }

        return sum;
    }

    private static Dictionary<long, long> RunThroughDict(Dictionary<long, long> dict)
    {
        Dictionary<long, long> newDict = [];

        foreach (var entry in dict)
        {
            if (entry.Key == 0)
            {
                long myOldCount = newDict.TryGetValue(1, out long value) ? value : 0;
                myOldCount += entry.Value - 1;
                AddValueToDict(newDict, 1, myOldCount);
            }
            else
            {
                if (entry.Key.ToString().Length % 2 == 0)
                {
                    (long left, long right) = Split(entry.Key.ToString());

                    long oldCount = dict[entry.Key] - 1;
                    oldCount += newDict.TryGetValue(left, out long value) ? value : 0;
                    AddValueToDict(newDict, left, oldCount);

                    oldCount = dict[entry.Key] - 1;
                    oldCount += newDict.TryGetValue(right, out value) ? value : 0;
                    AddValueToDict(newDict, right, oldCount);
                }
                else
                {
                    AddValueToDict(newDict, entry.Key * 2024, entry.Value - 1);
                }
            }
        }

        return newDict;
    }

    private static void AddValueToDict(Dictionary<long, long> dict, long value, long oldCount)
    {
        if (!dict.ContainsKey(value))
        {
            dict.Add(value, 0);
        }

        dict[value] = oldCount + 1;
    }

    private static (long NewStoneLeft, long NewStoneRight) Split(string textStone)
    {
        int halfLength = textStone.Length / 2;

        long left = long.Parse(textStone[..halfLength]);
        long right = int.Parse(textStone[halfLength..]);

        return (left, right);
    }
}