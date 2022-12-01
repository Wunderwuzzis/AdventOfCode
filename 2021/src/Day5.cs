using System.Linq;
using AoC.Collections;

namespace AoC;

public class Day5 : Day<int>
{
    private int[][] lines;
    
    public Day5(string dataPath) : base(dataPath)
    {
        lines = data.Select(line => Utils.ExtractAllInt(line)).ToArray();
    }

    protected override int Part1()
    {
        var grid = new IntGrid(FindMaxValue(lines) + 1, false);
        for (int i = 0; i < lines.Length; i++)
        {
            var l = lines[i];
            grid.AddLine(l[0], l[1], l[2], l[3]);
        }
        return grid.CountIntersections();
    }

    protected override int Part2()
    {
        var grid = new IntGrid(FindMaxValue(lines) + 1, true);
        for (int i = 0; i < lines.Length; i++)
        {
            var l = lines[i];
            grid.AddLine(l[0], l[1], l[2], l[3]);
        }
        return grid.CountIntersections();
    }

    private static int FindMaxValue(int[][] array)
    {
        int max = 0;
        foreach (var line in array)
            foreach (var number in line)
                if (number > max) max = number;
        return max;
    }
}