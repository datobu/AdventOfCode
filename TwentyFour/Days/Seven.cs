namespace TwentyFour.Days;

internal class Seven
{
    private enum CalculationOperators { Add, Multiply };

    public static long Run()
    {
        // CreateOperatorList(4);

        return TaskOne();
    }

    private static long TaskOne()
    {
        var lines = File.ReadAllLines("../../../Common/Inputs/DaySeven.txt");

        List<Tuple<long, List<int>>> allLines = [];

        long sum = 0;

        foreach (var line in lines)
        {
            var lineParts = line.Split(':');
            long result = Int64.Parse(lineParts[0]);

            List<int> calculationParts = lineParts[1][1..].Split(' ').Select(x => int.Parse(x)).ToList();

            var operatorList = CreateOperatorList(calculationParts.Count - 1);

            bool resultFound = false;
            int z = 0;

            while (!resultFound && z < operatorList.Count)
            {
                long tempResult = 0;

                CalculationOperators[] operatorSelection = operatorList[z];

                for (int i = 0; i < calculationParts.Count - 1; i++)
                {
                    if (operatorSelection[i] == CalculationOperators.Add)
                    {
                        if (i == 0)
                        {
                            tempResult += calculationParts[i] + calculationParts[i + 1];
                        }
                        else
                        {
                            tempResult += calculationParts[i + 1];
                        }

                    }
                    else
                    {
                        if (i == 0)
                        {
                            tempResult += calculationParts[i] * calculationParts[i + 1];
                        }
                        else
                        {
                            tempResult *= calculationParts[i + 1];
                        }
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

    private static List<CalculationOperators[]> CreateOperatorList(int operatorCount)
    {
        var list = new List<CalculationOperators[]>();

        for (int i = 0; i < Math.Pow(2, operatorCount); i++)
        {
            var ops = new CalculationOperators[operatorCount];

            for (int j = 0; j < operatorCount; j++)
            {
                if ((i >> j & 1) == 0)
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