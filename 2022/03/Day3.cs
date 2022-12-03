namespace AoC;

public class Day3 : Day<int>
{
    public Day3(string title, int? target1 = null, int? target2 = null) : base(3, title, target1, target2) { }

    protected override int Part1()
    {
        return Data.Sum(line =>
        {
            var (compartment1, compartment2) = GetCompartments(line);
            var duplicate = FindDuplicateItemType(compartment1, compartment2);
            return GetPriority(duplicate);
        });
    }

    private static (char[], char[]) GetCompartments(string rucksack)
    {
        var half = (int) (rucksack.Length * 0.5);
        return (rucksack[..half].ToCharArray(), rucksack[half..].ToCharArray());
    }

    private static char FindDuplicateItemType(IEnumerable<char> compartment1, IEnumerable<char> compartment2)
    {
        return compartment1.FirstOrDefault(compartment2.Contains);
    }

    private static int GetPriority(char character)
    {
        if (char.IsLower(character))
            return character - 'a' + 1;
        if (char.IsUpper(character))
            return character - 'A' + 1 + 26;
        throw new ArgumentException("Can't get type priority for non-letter character");
    }

    protected override int Part2()
    {
        return 0;
    }
}