using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AoC;

public class Day10 : Day<long>
{
    private ImmutableDictionary<char, char> code = new Dictionary<char, char>(){
                                                {'(', ')'},
                                                {'[',']'},
                                                {'{','}'},
                                                {'<','>'}}
                                                .ToImmutableDictionary();

    private List<char[]> lines;
    public Day10(string dataPath) : base(dataPath)
    {
        lines = data.Select(line => line.ToCharArray()).ToList();
    }

    protected override long Part1()
    {
        var points = new Dictionary<char, int>(){
                                        {')', 3},
                                        {']', 57},
                                        {'}', 1197},
                                        {'>', 25137}}
                                        .ToImmutableDictionary();
        long score = 0;
        foreach (var line in lines)
        {
            var items = new Stack<char>();
            foreach (var c in line)
            {
                if (code.Keys.Contains(c))
                    items.Push(c);
                else if (c == code[items.Peek()])
                    items.Pop();
                else // corrupted
                {
                    score += points[c];
                    break;
                }
            }
        }
        return score;
    }

    protected override long Part2()
    {
        var points = new Dictionary<char, int>(){
                                        {')', 1},
                                        {']', 2},
                                        {'}', 3},
                                        {'>', 4}}
                                        .ToImmutableDictionary();
        var scores = new List<long>();
        foreach (var line in lines)
        {
            var items = new Stack<char>();
            bool corrupted = false;
            foreach (var c in line)
            {
                if (code.Keys.Contains(c))
                    items.Push(c);
                else if (c == code[items.Peek()])
                    items.Pop();
                else
                    corrupted = true;
            }
            if (!corrupted)
            {
                long lineScore = 0;
                while (items.Count > 0)
                {
                    lineScore *= 5;
                    lineScore += points[code[items.Pop()]];
                }
                scores.Add(lineScore);
            }
        }
        return scores.OrderBy(s => s).ElementAt(scores.Count / 2);
    }
}