using AoC.Utils;

namespace AoC;

public class Day4 : Day<int>
{
    public Day4(string title, int? target1 = null, int? target2 = null) : base(4, title, target1, target2) { }

    protected override int Part1()
    {
        return Data.Count(line =>
        {
            var bounds = line.ExtractAllInt(4);
            var contains = bounds[0] <= bounds[2] && bounds[1] >= bounds[3] || bounds[0] >= bounds[2] && bounds[1] <= bounds[3];
            return contains;
        });
    }

    protected override int Part2()
    {
        return Data.Count(line =>
        {
            var bounds = line.ExtractAllInt(4);
            var exclusive = bounds[0] > bounds[3] || bounds[1] < bounds[2];
            return !exclusive;
        });
    }
}