namespace AoC;

public class Day2 : Day<int>
{
    int horizontal = 0;
    int depth = 0;
    int aim = 0;

    public Day2(string dataPath) : base(dataPath)
    {
    }

    protected override int Part1()
    {
        Reset();
        foreach (var line in data)
        {
            int v = line.ExtractFirstInt();
            if (line.Contains("forward"))
                horizontal += v;
            if (line.Contains("down"))
                depth += v;
            if (line.Contains("up"))
                depth -= v;
        }
        return horizontal * depth;
    }

    protected override int Part2()
    {
        Reset();
        foreach (var line in data)
        {
            int v = line.ExtractFirstInt();
            if (line.Contains("down"))
                aim += v;
            if (line.Contains("up"))
                aim -= v;
            if (line.Contains("forward"))
            {
                horizontal += v;
                depth += aim * v;
            }
        }
        return horizontal * depth;
    }

    private void Reset()
    {
        horizontal = 0;
        depth = 0;
        aim = 0;
    }
}
