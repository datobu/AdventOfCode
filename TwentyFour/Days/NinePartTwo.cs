namespace TwentyFour.Days;

internal class NinePartTwo
{
    private readonly List<Tuple<int, string, bool>> _memory = [];
    private readonly List<int> _map = [];

    public long Run()
    {
        return PartTwo();
    }

    public long PartTwo()
    {
        Init();

        CreateMemoryFromMap();

        // PrintMemory();

        OrderMemory();

        PrintMemory();

        return GetChecksum();
    }

    private long GetChecksum()
    {
        long checksum = 0;

        int i = 0;
        int x = 0;
        while (i < _memory.Count)
        {
            int j = 0;
            while (j < _memory[i].Item1)
            {
                if (_memory[i].Item2 != ".")
                {
                    checksum += int.Parse(_memory[i].Item2) * x;
                }

                j++;
                x++;
            }

            i++;
        }

        return checksum;
    }

    private void OrderMemory()
    {
        int oldIndex = _memory.Count - 1;
        while (oldIndex > 0)
        {
            string cursor = _memory[oldIndex].Item2;

            bool isFile = cursor != ".";

            bool wasMovedAlready = _memory[oldIndex].Item3;

            if (isFile && !wasMovedAlready)
            {
                int fileSize = _memory[oldIndex].Item1;

                string fileId = _memory[oldIndex].Item2;

                int newIndex = GetNewIndexWithAvailableSpace(fileSize);

                if (newIndex != -1 && newIndex < oldIndex)
                {
                    if (_memory[newIndex].Item1 > fileSize)
                    {
                        // Add new Free space after new location of file
                        _memory.Insert(newIndex + 1, new Tuple<int, string, bool>(_memory[newIndex].Item1 - fileSize, ".", false));
                        oldIndex++;
                    }

                    // Move item to new index
                    _memory[newIndex] = new Tuple<int, string, bool>(fileSize, fileId, true);

                    // Delete file on old index:
                    _memory[oldIndex] = new Tuple<int, string, bool>(_memory[oldIndex].Item1, ".", false);
                }
            }

            oldIndex--;
        }
    }

    private int GetNewIndexWithAvailableSpace(int fileSize)
    {
        for (int i = 0; i < _memory.Count; i++)
        {
            if (_memory[i].Item2 == "." && _memory[i].Item1 >= fileSize)
            {
                return i;
            }
        }

        return -1;
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
                _memory.Add(new Tuple<int, string, bool>(digit, fileId.ToString(), false));
                fileId++;
            }
            else
            {
                _memory.Add(new Tuple<int, string, bool>(digit, ".", false));
            }

            isFile = !isFile;
        }
    }

    private void PrintMemory()
    {
        foreach (var place in _memory)
        {
            Console.Write($"{place.Item1}: {place.Item2} | ");
        }
    }
}