namespace AoC;

public class Day6 : Day<int, int?>
{
    private readonly char[] _buffer;

    public Day6(string title, int? target1, int? target2) : base(6, title, target1, target2)
    {
        _buffer = Data[0].ToCharArray();
    }

    protected override int Part1()
    {
        return FindDistinctChunk(4);
    }

    protected override int Part2()
    {
        return FindDistinctChunk(14);
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