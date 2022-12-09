namespace AoC;

public class Day6 : Day<int>
{
    private readonly char[] _buffer;

    public Day6(string title, int target1 = default, int target2 = default) : base(6, title, target1, target2)
    {
        _buffer = Data[0].ToCharArray();
    }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        part1 = FindDistinctChunk(4);
        part2 = FindDistinctChunk(14);
    }

    private int FindDistinctChunk(int length)
    {
        return _buffer
            .TakeWhile((_, i) => _buffer[i..(i + length)]
                .Distinct()
                .Count() != length)
            .Count() + length;
    }
}