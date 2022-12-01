using System.Collections.Generic;

namespace AoC;

public class Day11 : Day<int>
{
    private Dictionary<Point, int> octopuses = new Dictionary<Point, int>();

    public Day11(string dataPath) : base(dataPath)
    {
        ReadOctopusesFromData();
    }

    protected override int Part1()
    {
        int steps = 100;
        int flashes = 0;
        for (int i = 0; i < steps; i++)
        {
            IncreaseAll();
            flashes += FlashOctopuses(0);
        }
        return flashes;
    }

    protected override int Part2()
    {
        ReadOctopusesFromData();
        int count = 0;
        bool extremelyBright = false;
        while (!extremelyBright)
        {
            ++count;
            IncreaseAll();
            FlashOctopuses(0);

            extremelyBright = true;
            foreach (int energyLevel in octopuses.Values)
                if (energyLevel > 0)
                    extremelyBright = false;
        }
        return count;
    }

    private void ReadOctopusesFromData()
    {
        int[] digits;
        for (int y = 0; y < data.Length; y++)
        {
            digits = data[y].ExtractAllDigits();
            for (int x = 0; x < data[0].Length; x++)
                octopuses[new Point(x, y)] = digits[x];
        }
    }

    private void IncreaseAll()
    {
        foreach (Point octopus in octopuses.Keys)
            octopuses[octopus]++;
    }

    private void IncreaseSurrounding(int x, int y)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                var point = new Point(x + i, y + j);
                if (octopuses.ContainsKey(point) && octopuses[point] > 0)
                    ++octopuses[point];
            }
        }
    }

    private int FlashOctopuses(int count)
    {
        foreach (Point point in octopuses.Keys)
        {
            if (octopuses[point] > 9)
            {
                octopuses[point] = 0;
                IncreaseSurrounding(point.x, point.y);
                count = FlashOctopuses(++count);
            }
        }
        return count;
    }
}
