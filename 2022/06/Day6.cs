namespace AoC;

public class Day6 : Day<int>
{
    public Day6(string title, int target1 = default, int target2 = default) : base(6, title, target1, target2) { }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        var buffer = Data[0].ToCharArray();
        part1 = FindDistinctChunk(buffer, 4);
        part2 = FindDistinctChunk(buffer, 14);
    }

    private static int FindDistinctChunk(char[] buffer, int length)
    {
        return buffer
            .TakeWhile((_, i) => buffer[i..(i + length)]
                .Distinct()
                .Count() != length)
            .Count() + length;
    }
}