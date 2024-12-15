namespace TwentyFour.Days;

// completely alone
internal class SevenPartTwo
{
    private enum CalculationOperators
    {
        Add,
        Multiply,
        Combine
    }

    public static long Run()
    {
        // CreateOperatorList(4);

        return TaskTwo();
    }

    public static long TaskTwo()
    {
        var lines = File.ReadAllLines("../../../Common/Inputs/DaySeven.txt");

        List<Tuple<long, List<int>>> allLines = [];

        long sum = 0;

        foreach (var line in lines)
        {
            var lineParts = line.Split(':');
            long result = long.Parse(lineParts[0]);

            List<int> calculationParts = lineParts[1][1..].Split(' ').Select(x => int.Parse(x)).ToList();

            var operatorList = CreateOperatorListWithCombine(calculationParts.Count - 1);

            bool resultFound = false;
            int z = 0;

            while (!resultFound && z < operatorList.Count)
            {
                long tempResult = calculationParts[0];

                CalculationOperators[] operatorSelection = operatorList[z];

                for (int i = 0; i < calculationParts.Count - 1; i++)
                {
                    if (operatorSelection[i] == CalculationOperators.Add)
                    {
                        tempResult += calculationParts[i + 1];
                    }
                    else if (operatorSelection[i] == CalculationOperators.Multiply)
                    {
                        tempResult *= calculationParts[i + 1];
                    }
                    else
                    {
                        var tempString = $"{tempResult}{calculationParts[i + 1]}";
                        tempResult = long.Parse(tempString);
                    }
                }

                if (tempResult == result)
                {
                    sum += tempResult;
                    resultFound = true;
                }

                z++;
            }
        }

        return sum;
    }

    public static long TaskOne()
    {
        var lines = File.ReadAllLines("../../../Common/Inputs/DaySeven.txt");

        List<Tuple<long, List<int>>> allLines = [];

        long sum = 0;

        foreach (var line in lines)
        {
            var lineParts = line.Split(':');
            long result = long.Parse(lineParts[0]);

            List<int> calculationParts = lineParts[1][1..].Split(' ').Select(x => int.Parse(x)).ToList();

            var operatorList = CreateOperatorList(calculationParts.Count - 1);

            bool resultFound = false;
            int z = 0;

            while (!resultFound && z < operatorList.Count)
            {
                long tempResult = calculationParts[0];

                CalculationOperators[] operatorSelection = operatorList[z];

                for (int i = 0; i < calculationParts.Count - 1; i++)
                {
                    if (operatorSelection[i] == CalculationOperators.Add)
                    {
                        tempResult += calculationParts[i + 1];
                    }
                    else
                    {
                        tempResult *= calculationParts[i + 1];
                    }
                }

                if (tempResult == result)
                {
                    sum += tempResult;
                    resultFound = true;
                }

                z++;
            }
        }

        return sum;
    }

    private static List<CalculationOperators[]> CreateOperatorListWithCombine(int operatorCount)
    {
        var list = new List<CalculationOperators[]>();

        for (int i = 0; i < Math.Pow(3, operatorCount); i++)
        {
            var ops = new CalculationOperators[operatorCount];

            for (int j = 0; j < operatorCount; j++)
            {
                if ((i / (int)Math.Pow(3, j)) % 3 == 0)
                {
                    ops[j] = CalculationOperators.Add;
                }
                else if ((i / (int)Math.Pow(3, j)) % 3 == 1)
                {
                    ops[j] = CalculationOperators.Multiply;
                }
                else
                {
                    ops[j] = CalculationOperators.Combine;
                }
            }

            list.Add(ops);
        }

        return list;
    }

    private static List<CalculationOperators[]> CreateOperatorList(int operatorCount)
    {
        var list = new List<CalculationOperators[]>();

        for (int i = 0; i < Math.Pow(2, operatorCount); i++)
        {
            var ops = new CalculationOperators[operatorCount];

            for (int j = 0; j < operatorCount; j++)
            {
                if ((i / (int)Math.Pow(2, j)) % 2 == 0)
                {
                    ops[j] = CalculationOperators.Add;
                }
                else
                {
                    ops[j] = CalculationOperators.Multiply;
                }
            }

            list.Add(ops);
        }

        return list;
    }
}