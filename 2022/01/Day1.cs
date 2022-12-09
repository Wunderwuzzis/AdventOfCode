namespace AoC;

public class Day1 : Day<int>
{
    public Day1(string title, int target1 = default, int target2 = default) : base(1, title, target1, target2) { }

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

    protected override void ExecuteParts(out int part1, out int part2)
    {
        var calories = GetCalories().ToArray();
        part1 = calories.Max();
        part2 = calories.OrderByDescending(x => x).Take(3).Sum();
    }
}