using System.Diagnostics;

namespace AoC;

public class Day3 : Day<int>
{
    public Day3(string title, int target1 = default, int target2 = default) : base(3, title, target1, target2) { }

    protected override int Part1()
    {
        return Data.Sum(line =>
        {
            var (compartment1, compartment2) = GetCompartments(line);
            var duplicateItemType = FindDuplicateItemType(compartment1, compartment2);
            return GetPriority(duplicateItemType);
        });
    }

    private static (char[], char[]) GetCompartments(string rucksack)
    {
        var half = (int) (rucksack.Length * 0.5);
        return (rucksack[..half].ToCharArray(), rucksack[half..].ToCharArray());
    }

    private static char FindDuplicateItemType(IEnumerable<char> compartment1, IEnumerable<char> compartment2)
    {
        return compartment1.First(compartment2.Contains);
    }

    private static int GetPriority(char character)
    {
        if (char.IsLower(character))
            return character - 'a' + 1;
        if (char.IsUpper(character))
            return character - 'A' + 1 + 26;
        throw new InvalidOperationException("Can't get type priority for non-letter character");
    }

    protected override int Part2()
    {
        var sum = 0;
        for (var i = 0; i < Data.Length; i += 3)
        {
            var group = Data.Skip(i).Take(3).ToArray();
            var commonItemType = FindCommonItemType(group);
            sum += GetPriority(commonItemType);
        }

        return sum;
    }

    private static char FindCommonItemType(IReadOnlyList<string> group)
    {
        Debug.Assert(group.Count == 3);
        return group[0].First(character => group[1].Contains(character) && group[2].Contains(character));
    }
}