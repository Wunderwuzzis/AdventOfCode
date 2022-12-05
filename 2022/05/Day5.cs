using AoC.Utils;

namespace AoC;

public class Day5 : Day<string, string?>
{
    private readonly int _stackDataEndIndex;
    private readonly int _instructionDataStartIndex;
    private readonly int[] _crateIndices = { 1, 5, 9, 13, 17, 21, 25, 29, 33 };

    public Day5(string title, string? target1 = null, string? target2 = null) : base(5, title, target1, target2)
    {
        _stackDataEndIndex = Array.FindIndex(Data, str => str == "");
        _instructionDataStartIndex = _stackDataEndIndex + 1;
    }

    private Dictionary<int, Stack<char>> GetSupplyStacks()
    {
        var supplies = new Dictionary<int, IList<char>>();

        foreach (var line in Data[.._stackDataEndIndex])
        {
            var chars = line.ToCharArray();

            for (var i = 0; i < _crateIndices.Length; i++)
            {
                var c = chars[_crateIndices[i]];
                if (!char.IsLetter(c))
                    continue;

                var index = i + 1;
                if (!supplies.ContainsKey(index))
                    supplies.Add(index, new List<char>());

                supplies[index].Add(c);
            }
        }

        return supplies
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => new Stack<char>(x.Value.Reverse()));
    }

    protected override string Part1()
    {
        var stacks = GetSupplyStacks();
        foreach (var line in Data[_instructionDataStartIndex..])
        {
            var (repeat, from, to) = line.ExtractAllInt(3);
            for (var i = 0; i < repeat; i++)
            {
                stacks[to].Push(stacks[from].Pop());
            }
        }

        return string.Join("", stacks.Select(s => s.Value.Pop()));
    }

    protected override string Part2()
    {
        var stacks = GetSupplyStacks();
        var picked = new Stack<char>();
        foreach (var line in Data[_instructionDataStartIndex..])
        {
            var (amount, from, to) = line.ExtractAllInt(3);

            for (var i = 0; i < amount; i++)
                picked.Push(stacks[from].Pop());

            for (var i = 0; i < amount; i++)
                stacks[to].Push(picked.Pop());

            picked.Clear();
        }

        return string.Join("", stacks.Select(s => s.Value.Pop()));
    }
}