namespace TwentyFour.Days;

internal class Nine
{
    private readonly List<string> _memory = [];
    private readonly List<int> _map = [];

    public long Run()
    {
        return PartOne();
    }

    private long PartOne()
    {
        Init();

        CreateMemoryFromMap();

        OrderMemory();

        //PrintMemory();

        return GetChecksum();
    }

    private long GetChecksum()
    {
        int i = 0;
        string value = _memory[i];
        long checksum = 0;

        while (value != ".")
        {
            int product = int.Parse(value) * i;
            checksum += product;
            value = _memory[++i];
        }

        return checksum;
    }

    private void OrderMemory()
    {
        for (int i = 0; i < _memory.Count; i++)
        {
            if (_memory[i] == ".")
            {
                if (AreStillFilePartsComing(i))
                {
                    int lastFilePartPosition = GetPositionOfLastFilePartOfMemory();

                    // Swap:
                    _memory[i] = _memory[lastFilePartPosition];
                    _memory[lastFilePartPosition] = ".";
                }

            }
        }
    }

    private bool AreStillFilePartsComing(int i)
    {
        while (i < _memory.Count)
        {
            if (_memory[i] != ".")
            {
                return true;
            }
            i++;
        }

        return false;
    }

    private int GetPositionOfLastFilePartOfMemory()
    {
        for (int j = _memory.Count - 1; j >= 0; j--)
        {
            if (_memory[j] != ".")
            {
                return j;
            }
        }

        throw new Exception();
    }

    private void Init()
    {
        var input = File.ReadAllText("../../../Common/Inputs/DayNine.txt");
        foreach (char character in input)
        {
            _map.Add(int.Parse(character.ToString()));
        }
    }

    private void CreateMemoryFromMap()
    {
        bool isFile = true;
        int fileId = 0;

        foreach (int digit in _map)
        {
            if (isFile)
            {
                FillMemory(digit, fileId.ToString());
                fileId++;
            }
            else
            {
                FillMemory(digit, ".");
            }

            isFile = !isFile;
        }
    }

    private void PrintMemory()
    {
        foreach (var place in _memory)
        {
            Console.Write($"{place}|");
        }
    }

    private void FillMemory(int digit, string value)
    {
        for (int i = 0; i < digit; i++)
        {
            _memory.Add(value);
        }
    }
}