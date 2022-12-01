using System.Linq;
using System;
using System.Collections.Generic;

namespace AoC;

public class Day8 : Day<long>
{
    private Display[] displays;
    public Day8(string dataPath) : base(dataPath)
    {
        displays = new Display[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            var notes = data[i].Split(" | ");
            displays[i] = new Display(notes[0], notes[1]);
        }
    }

    protected override long Part1()
    {
        return displays.Sum(d => d.CountOutput(new int[] { 1, 4, 7, 8 }));
    }

    protected override long Part2()
    {
        return displays.Sum(d=> d.Analyze());
    }

    private class Display
    {
        private char[][] digits;
        private char[][] outputs;
        public Display(string digitsNote, string outputNote)
        {
            digits = digitsNote.Split(null).Select(d => d.OrderBy(c => c).ToArray()).ToArray();
            outputs = outputNote.Split(null).Select(d => d.OrderBy(c => c).ToArray()).ToArray();
        }

        public int CountOutput(int[] number)
        {
            int[] definitions = new int[] { 5, 2, 5, 5, 4, 5, 6, 3, 7, 6 }; // amounts of segments used by digit at [index]
            int count = 0;
            foreach (var o in outputs)
                if (number.Any(n => definitions[n] == o.Length))
                    count++;
            return count;
        }

        public int Analyze()
        {
            var mapping = new Dictionary<int, char[]>();

            mapping[1] = digits.Single(x => x.Length == 2);
            mapping[4] = digits.Single(x => x.Length == 4);
            mapping[7] = digits.Single(x => x.Length == 3);
            mapping[8] = digits.Single(x => x.Length == 7);

            mapping[9] = digits.Single(x => x.Length == 6 && x.Intersect(mapping[4]).Count() == 4);
            mapping[6] = digits.Single(x => x.Length == 6 && !mapping[1].All(x.Contains));
            mapping[0] = digits.Single(x => x.Length == 6 && !mapping.ContainsValue(x));

            mapping[5] = digits.Single(x => x.Length == 5 && x.Intersect(mapping[6]).Count() == 5);
            mapping[3] = digits.Single(x => x.Length == 5 && x.Intersect(mapping[7]).Count() == 3 && !mapping.ContainsValue(x));
            mapping[2] = digits.Single(x => x.Length == 5 && !mapping.ContainsValue(x));

            var result = outputs.Select(o => mapping.Single(x => x.Value.SequenceEqual(o)).Key).ToArray();
            return int.Parse(string.Join("", result.Select(d => d.ToString())));
        }
    }
}
