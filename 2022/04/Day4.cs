using System.Text.RegularExpressions;
using AoC.Utils;

namespace AoC;

public class Day4 : Day<int>
{
    private readonly Regex _regex = new(@"-|,");

    public Day4(string title, int target1 = default, int target2 = default) : base(4, title, target1, target2) { }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        part1 = Data.Count(line =>
        {
            var (aMin, aMax, bMin, bMax) = _regex.Split(line).Select(int.Parse).ToArray();
            return aMin <= bMin && aMax >= bMax || aMin >= bMin && aMax <= bMax;
        });

        part2 = Data.Count(line =>
        {
            var (aMin, aMax, bMin, bMax) = _regex.Split(line).Select(int.Parse).ToArray();
            return aMin <= bMax && aMax >= bMin;
        });
    }
}