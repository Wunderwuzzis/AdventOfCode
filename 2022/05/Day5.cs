﻿using AoC.Utils;

namespace AoC;

public class Day5 : Day<string>
{
    private readonly int _stackDataEndIndex;
    private readonly int _instructionDataStartIndex;
    private readonly int[] _crateIndices = { 1, 5, 9, 13, 17, 21, 25, 29, 33 };

    public Day5(string title, string? target1 = default, string? target2 = default) : base(5, title, target1 ?? string.Empty, target2 ?? string.Empty)
    {
        _stackDataEndIndex = Array.FindIndex(Data, str => str == "");
        _instructionDataStartIndex = _stackDataEndIndex + 1;
    }

    protected override void ExecuteParts(out string part1, out string part2)
    {
        var stacks1 = GetSupplyStacks();
        var stacks2 = GetSupplyStacks();

        var picked = new Stack<char>();

        foreach (var line in Data[_instructionDataStartIndex..])
        {
            var (amount, from, to) = line.ParseAllInt(3);

            for (var i = 0; i < amount; i++)
            {
                stacks1[to].Push(stacks1[from].Pop());
                picked.Push(stacks2[from].Pop());
            }

            for (var i = 0; i < amount; i++)
            {
                stacks2[to].Push(picked.Pop());
            }

            picked.Clear();
        }

        part1 = string.Join("", stacks1.Select(s => s.Value.Pop()));
        part2 = string.Join("", stacks2.Select(s => s.Value.Pop()));
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
}