using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC;

public class Day9 : Day<int>
{
    private int[][] heightmap;
    Dictionary<Point, bool> map;

    public Day9(string dataPath) : base(dataPath)
    {
        heightmap = data.Select(row => Utils.ExtractAllDigits(row)).ToArray();
        map = data.SelectMany((row, x) => Utils.ExtractAllDigits(row)
                               .Select((_, y) => new Point(x, y)))
                    .ToDictionary(p => p, b => false);
    }

    protected override int Part1()
    {
        List<int> lowest = new List<int>();
        for (int y = 0; y < heightmap.Length; y++)
            for (int x = 0; x < heightmap[0].Length; x++)
                if (IsLowest(x, y))
                    lowest.Add(GetHeight(x, y));
        return lowest.Sum() + lowest.Count();
    }

    protected override int Part2()
    {
        var basins = new List<int>(); // stores sizes of basins
        foreach (var item in map)
            if (!item.Value && GetHeight(item.Key.x, item.Key.y) != 9)
                basins.Add(FloodFill(item.Key));
        return basins.OrderByDescending(i => i).Take(3).Aggregate((x, y) => x * y); ;
    }

    private bool IsLowest(int x, int y)
    {
        var current = GetHeight(x, y);
        return (current < GetHeight(x - 1, y) &&
            current < GetHeight(x + 1, y) &&
            current < GetHeight(x, y - 1) &&
            current < GetHeight(x, y + 1));
    }

    private int GetHeight(int x, int y)
    {
        if (x < 0 || y < 0 || x >= heightmap[0].Length || y >= heightmap.Length)
            return 9;
        return heightmap[y][x];
    }

    private int FloodFill(Point pt)
    {
        Stack<Point> heights = new Stack<Point>();
        heights.Push(pt);

        var counter = 0;
        while (heights.Count > 0)
        {
            Point a = heights.Pop();
            if (GetHeight(a.x, a.y) != 9)
            {
                if (!map[a])
                {
                    map[a] = true;
                    counter++;
                    heights.Push(new Point(a.x - 1, a.y));
                    heights.Push(new Point(a.x + 1, a.y));
                    heights.Push(new Point(a.x, a.y - 1));
                    heights.Push(new Point(a.x, a.y + 1));
                }
            }
        }
        return counter;
    }
}
