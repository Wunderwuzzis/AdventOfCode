using System;
using System.Linq;

namespace AoC;

public class Day7 : Day<long>
{
    private int[] crabPositions;
    public Day7(string dataPath) : base(dataPath)
    {
        crabPositions = data[0].ExtractAllInt();
    }

    protected override long Part1()
    {
        var avg = Median(crabPositions);
        return crabPositions.Sum(x => Math.Abs(x - avg));
    }

    protected override long Part2()
    {
        var avgdouble = crabPositions.Average();
        var avg = (int)Math.Min(Math.Floor(avgdouble), Math.Ceiling(avgdouble)); // 343165, 343381
        return crabPositions.Sum(x => SumEffort(Math.Abs(x - avg)));
    }

    private int Median(int[] array)
    {
        Array.Sort(array);
        return array[array.Length / 2];
    }

    private long SumEffort(int n)
    {
        return (n + 1) * n / 2;
    }
}