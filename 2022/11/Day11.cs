namespace AoC;

public class Day11 : Day<long>
{
    public Day11(string title, long target1 = default, long target2 = default) : base(11, title, target1, target2) { }

    protected override void ExecuteParts(out long part1, out long part2)
    {
        var monkeys = GetMonkeys();
        for (var i = 0; i < 20; i++)
            foreach (var monkey in monkeys)
            foreach (var (item, target) in monkey.Throw(item => (long) Math.Floor(item / 3f)))
                monkeys[target].Items.Add(item);

        part1 = GetMonkeyBusiness(monkeys);

        var worrySum = monkeys.Select(m => m.Divisor).Aggregate((a, b) => a * b);
        monkeys = GetMonkeys();
        for (var i = 0; i < 10000; i++)
            foreach (var monkey in monkeys)
            foreach (var (item, target) in monkey.Throw(item => item % worrySum))
                monkeys[target].Items.Add(item);

        part2 = GetMonkeyBusiness(monkeys);
    }

    private static Monkey[] GetMonkeys()
    {
        return new[]
        {
            new Monkey(new List<long> { 98, 97, 98, 55, 56, 72 }, 11, 4, 7, item => item * 13),
            new Monkey(new List<long> { 73, 99, 55, 54, 88, 50, 55 }, 17, 2, 6, item => item + 4),
            new Monkey(new List<long> { 67, 98 }, 5, 6, 5, item => item * 11),
            new Monkey(new List<long> { 82, 91, 92, 53, 99 }, 13, 1, 2, item => item + 8),
            new Monkey(new List<long> { 52, 62, 94, 96, 52, 87, 53, 60 }, 19, 3, 1, item => item * item),
            new Monkey(new List<long> { 94, 80, 84, 79 }, 2, 7, 0, item => item + 5),
            new Monkey(new List<long> { 89 }, 3, 0, 5, item => item + 1),
            new Monkey(new List<long> { 70, 59, 63 }, 7, 4, 3, item => item + 3)
        };
    }

    private static long GetMonkeyBusiness(IEnumerable<Monkey> monkeys)
    {
        return monkeys.Select(m => m.InspectionCount).OrderByDescending(x => x).Take(2).Aggregate((a, b) => a * b);
    }
}