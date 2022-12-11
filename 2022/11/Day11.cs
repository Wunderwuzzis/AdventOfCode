namespace AoC;

public class Day11 : Day<ulong>
{
    public Day11(string title, ulong target1 = default, ulong target2 = default) : base(11, title, target1, target2) { }

    protected override void ExecuteParts(out ulong part1, out ulong part2)
    {
        var monkeys = GetMonkeys();
        for (var i = 0; i < 20; i++)
            foreach (var monkey in monkeys)
            foreach (var (item, target) in monkey.Throw(item => (ulong) Math.Floor(item / 3f)))
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
            new Monkey(new List<ulong> { 98, 97, 98, 55, 56, 72 }, 11, 4, 7, item => item * 13),
            new Monkey(new List<ulong> { 73, 99, 55, 54, 88, 50, 55 }, 17, 2, 6, item => item + 4),
            new Monkey(new List<ulong> { 67, 98 }, 5, 6, 5, item => item * 11),
            new Monkey(new List<ulong> { 82, 91, 92, 53, 99 }, 13, 1, 2, item => item + 8),
            new Monkey(new List<ulong> { 52, 62, 94, 96, 52, 87, 53, 60 }, 19, 3, 1, item => item * item),
            new Monkey(new List<ulong> { 94, 80, 84, 79 }, 2, 7, 0, item => item + 5),
            new Monkey(new List<ulong> { 89 }, 3, 0, 5, item => item + 1),
            new Monkey(new List<ulong> { 70, 59, 63 }, 7, 4, 3, item => item + 3)
        };
    }

    private static ulong GetMonkeyBusiness(IEnumerable<Monkey> monkeys)
    {
        return monkeys.Select(m => m.InspectionCount).OrderByDescending(x => x).Take(2).Aggregate((a, b) => a * b);
    }

    private record Monkey(List<ulong> Items, uint Divisor, uint TargetIfTrue, uint TargetIfFalse, Func<ulong, ulong> Inspect)
    {
        public ulong InspectionCount { get; private set; }

        public IEnumerable<(ulong, uint)> Throw(Func<ulong, ulong>? decreaseWorry)
        {
            var inspectedItems = Items.Select(item => Inspect(item)).ToArray();
            foreach (var inspectedItem in inspectedItems)
            {
                InspectionCount++;
                var worry = decreaseWorry?.Invoke(inspectedItem) ?? inspectedItem;
                yield return (worry, GetThrowTarget(worry));
            }

            Items.Clear();
        }

        private uint GetThrowTarget(ulong item)
        {
            return item % Divisor == 0 ? TargetIfTrue : TargetIfFalse;
        }
    }
}