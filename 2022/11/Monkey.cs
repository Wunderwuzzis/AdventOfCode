namespace AoC;

public class Monkey
{
    private readonly int _targetIfTrue;
    private readonly int _targetIfFalse;
    private readonly Func<long, long> _inspect;
    public List<long> Items { get; }
    public int Divisor { get; }

    public Monkey(List<long> items, int divisor, int targetIfTrue, int targetIfFalse, Func<long, long> inspect)
    {
        _targetIfTrue = targetIfTrue;
        _targetIfFalse = targetIfFalse;
        _inspect = inspect;
        Items = items;
        Divisor = divisor;
    }

    public long InspectionCount { get; private set; }

    public IEnumerable<(long, int)> Throw(Func<long, long>? decreaseWorry)
    {
        var inspectedItems = Items.Select(item => _inspect(item)).ToArray();
        foreach (var inspectedItem in inspectedItems)
        {
            InspectionCount++;
            var worry = decreaseWorry?.Invoke(inspectedItem) ?? inspectedItem;
            yield return (worry, GetThrowTarget(worry));
        }

        Items.Clear();
    }

    private int GetThrowTarget(long item)
    {
        return item % Divisor == 0 ? _targetIfTrue : _targetIfFalse;
    }
}