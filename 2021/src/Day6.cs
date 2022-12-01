using System.Linq;

namespace AoC;

public class Day6 : Day<long>
{
    public Day6(string dataPath) : base(dataPath)
    {
    }

    protected override long Part1()
    {
        return PopulateFish(GetFishPopulation(), 80);
    }

    protected override long Part2()
    {
        return PopulateFish(GetFishPopulation(), 256);
    }

    private long[] GetFishPopulation()
    {
        var fishContainer = new long[9];
        foreach (var number in data[0].ExtractAllInt())
            fishContainer[number]++; // count existing fishes
        return fishContainer;
    }

    // https://barretblake.dev/blog/2021/12/advent-of-code-day6/
    private long PopulateFish(long[] fishContainer, int days)
    {
        for (int i = 0; i < days; i++)
        {
            var fishReproducing = fishContainer[0];
            for (int y = 0; y < 8; y++)
            {
                //shift all fish down one day
                fishContainer[y] = fishContainer[y + 1];
            }
            //now reset reproducers
            fishContainer[6] = fishContainer[6] + fishReproducing;
            fishContainer[8] = fishReproducing;
        }

        return fishContainer.Sum();
    }
}