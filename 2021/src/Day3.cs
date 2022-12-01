using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Specialized;

namespace AoC;

public class Day3 : Day<int>
{
    BitVector32[] bitData;
    BitVector32 gamma;
    BitVector32 epsilon;
    int highestBitSet;

    public Day3(string dataPath) : base(dataPath)
    {
        bitData = Array.ConvertAll(data, converter: line => new BitVector32(Convert.ToInt32(line, 2)));
        highestBitSet = (int)Math.Pow(2, data[0].Length - 1); // e.g. 4 length: 2^3 | 2^2 | 2^1 | 2^0

        // Creates and initializes a BitVector32 with all bit flags set to FALSE
        gamma = new BitVector32(0);
        epsilon = new BitVector32(0);
    }

    protected override int Part1()
    {
        // powerConsumption = gamma rate * epsilon rate
        for (int i = 1; i <= highestBitSet; i = i << 1)
        {
            gamma[i] = MostCommonBitValue(bitData, i);
            epsilon[i] = LeastCommonBitValue(bitData, i);
        }
        return gamma.Data * epsilon.Data;
    }

    protected override int Part2()
    {
        // life support rating = oxygen generator rating * CO2 scrubber rating
        BitVector32[] oxygen = (BitVector32[])bitData.Clone();
        for (int i = highestBitSet; i > 0 && oxygen.Length > 1; i = i >> 1)
            oxygen = oxygen.Where(binary => binary[i] == MostCommonBitValue(oxygen, i)).ToArray();
        Debug.Assert(oxygen.Count() == 1);

        BitVector32[] co2 = (BitVector32[])bitData.Clone();
        for (int i = highestBitSet; i > 0 && co2.Length > 1; i = i >> 1)
            co2 = co2.Where(binary => binary[i] == LeastCommonBitValue(co2, i)).ToArray();
        Debug.Assert(co2.Count() == 1);

        return oxygen[0].Data * co2[0].Data;
    }

    private static bool MostCommonBitValue(BitVector32[] data, int index)
    {
        // are the bits that are set more/equal than half of all data ? set : notset; is most common
        return data.Count(binary => binary[index]) * 2 >= data.Length;
    }

    private static bool LeastCommonBitValue(BitVector32[] data, int index)
    {
        // are the bits that are set less than half of all data ? set : notset; is least common
        return data.Count(binary => binary[index]) * 2 < data.Length;
    }
}
