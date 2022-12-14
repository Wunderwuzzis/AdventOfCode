using System.Text.Json.Nodes;
using AoC.Utils;

namespace AoC;

// Adaptation from https://github.com/encse/adventofcode/blob/master/2022/Day13/Solution.cs
public class Day13 : Day<int>
{
    public Day13(string title, int target1 = default, int target2 = default) : base(13, title, target1, target2) { }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        JsonNode Parse(string line) =>
            JsonNode.Parse(line) ?? throw new InvalidOperationException($"Line: {line} is no json node.");

        part1 = Data.Chunk(3)
            .Select((pair, index) => Compare(Parse(pair[0]), Parse(pair[1])) < 0 ? index + 1 : 0)
            .Sum();

        var div2 = Parse("[[2]]");
        var div6 = Parse("[[6]]");
        var packets = Data.Where(line => line != "")
            .Select(Parse)
            .Concat(new List<JsonNode> { div2, div6 })
            .ToArray()
            .Sort(Compare);

        part2 = (packets.IndexOf(div2) + 1) * (packets.IndexOf(div6) + 1);
    }

    private static int Compare(JsonNode nodeA, JsonNode nodeB)
    {
        if (nodeA is JsonValue && nodeB is JsonValue)
            return (int) nodeA - (int) nodeB;

        var arrayA = nodeA as JsonArray ?? new JsonArray((int) nodeA);
        var arrayB = nodeB as JsonArray ?? new JsonArray((int) nodeB);
        return arrayA.Zip(arrayB)
            .Select(packet => Compare(packet.First!, packet.Second!))
            .FirstOrDefault(comparison => comparison != 0, arrayA.Count - arrayB.Count);
    }
}