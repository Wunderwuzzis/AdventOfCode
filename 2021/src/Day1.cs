using System;

namespace AoC;

public class Day1 : Day<int>
{
    private int[] intData;

    public Day1(string dataPath) : base(dataPath)
    {
        intData = Array.ConvertAll(data, line => int.Parse(line));
    }

    protected override int Part1()
    {
        int count = 0;
        for (int i = 1; i < intData.Length; i++)
        {
            if (intData[i] > intData[i - 1])
                count++;
        }
        return count;
    }

    protected override int Part2()
    {
        int count = 0;
        int prevSum = SumFromIndex(intData, 0);
        for (int i = 1; i < intData.Length - 2; i++)
        {
            int sum = SumFromIndex(intData, i);
            if (sum > prevSum)
                count++;
            prevSum = sum;
        }
        return count;
    }

    private int SumFromIndex(int[] data, int index)
    {
        return data[index] + data[index + 1] + data[index + 2];
    }
}
