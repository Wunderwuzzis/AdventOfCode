namespace AoC;

public class Day3Short : Day<int>
{
    public Day3Short(string title, int target1 = default, int target2 = default) : base(3, title, target1, target2) { }

    protected override int Part1()
    {
        return Data
            .Sum(l =>
            {
                var c = l[..(l.Length / 2)].First(l[(l.Length / 2)..].Contains);
                return char.IsLower(c) ? c - 'a' + 1 : c - 'A' + 1 + 26;
            });
    }

    protected override int Part2()
    {
        return Data.Chunk(3)
            .Sum(g =>
            {
                var c = g[0].First(c => g[1].Contains(c) && g[2].Contains(c));
                return char.IsLower(c) ? c - 'a' + 1 : c - 'A' + 1 + 26;
            });
    }
}