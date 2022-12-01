namespace AoC;

public class Day1 : Day<int>
{
    public Day1(int day, string title, int? target1 = null, int? target2 = null) : base(day, title, target1, target2) { }

    protected override int Part1()
    {
        return GetCarriedCalories().MaxBy(x => x);
    }

    private IEnumerable<int> GetCarriedCalories()
    {
        var result = new List<int>();
        var skip = 0;
        while (skip < Data.Length)
        {
            var items = Data.Skip(skip).TakeWhile(line => line != string.Empty).Select(int.Parse).ToArray();
            skip += items.Length + 1; // + empty line

            result.Add(items.Sum());
        }

        return result;
    }

    protected override int Part2()
    {
        return GetCarriedCalories().OrderByDescending(x => x).Take(3).Sum();
    }
}