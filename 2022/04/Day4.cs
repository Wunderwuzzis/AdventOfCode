using System.Text.RegularExpressions;
using AoC.Utils;

namespace AoC;

public class Day4 : Day<int, int?>
{
    private readonly Regex _regex = new(@"-|,");

    public Day4(string title, int? target1 = null, int? target2 = null) : base(4, title, target1, target2) { }

    protected override int Part1()
    {
        return Data.Count(line =>
        {
            var (aMin, aMax, bMin, bMax) = _regex.Split(line).Select(int.Parse).ToArray();
            return aMin <= bMin && aMax >= bMax || aMin >= bMin && aMax <= bMax;
        });
    }

    protected override int Part2()
    {
        return Data.Count(line =>
        {
            var (aMin, aMax, bMin, bMax) = _regex.Split(line).Select(int.Parse).ToArray();
            return aMin <= bMax && aMax >= bMin;
        });
    }
}