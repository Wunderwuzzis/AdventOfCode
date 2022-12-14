using System.Numerics;

namespace AoC;

// Adaptation from https://github.com/encse/adventofcode/blob/master/2022/Day14/Solution.cs
public class Day14 : Day<int>
{
    public Day14(string title, int target1 = default, int target2 = default) : base(14, title, target1, target2) { }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        var steps = Data.Select(line => line.Split(" -> ").Select(coordinates =>
        {
            var parts = coordinates.Split(",");
            return new Vector2(int.Parse(parts[0]), int.Parse(parts[1]));
        }).ToArray()).ToArray();

        var cave = new Cave(steps);

        cave.FillWithSand(false);
        part1 = cave.SandAmount;
        cave.FillWithSand(true);
        part2 = cave.SandAmount;
    }
}