namespace AoC;

public class Day1 : Day<int>
{
    public Day1(string title, int? target1 = null, int? target2 = null) : base(1, title, target1, target2) { }

    private IEnumerable<int> GetCalories()
    {
        return Data.Select((_, i) => Data.Skip(i)
            .TakeWhile(line =>
            {
                i++;
                return line != string.Empty;
            })
            .Select(int.Parse)
            .Sum());
    }

    protected override int Part1()
    {
        return GetCalories().Max();
    }

    protected override int Part2()
    {
        return GetCalories().OrderByDescending(x => x).Take(3).Sum();
    }
}