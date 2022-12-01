namespace AoC;

public class Day14 : Day<long>
{
    private List<char> polymer;
    private Dictionary<(char, char), char> rules = new Dictionary<(char, char), char>();
    private Dictionary<(char, char), int> pairs = new Dictionary<(char, char), int>();
    private Dictionary<char, int> counts = new Dictionary<char, int>();

    public Day14(string dataPath) : base(dataPath)
    {
        polymer = GetTemplate();
        var lines = ((IEnumerable<string>)data).GetEnumerator();
        lines.MoveNext();
        lines.MoveNext();

        // empty line reached, continue with parsing the insertions rules
        while (lines.MoveNext())
        {
            var rule = lines.Current.Split(" -> ");
            rules.Add((rule[0][0], rule[0][1]), rule[1][0]);
        }
    }

    protected override long Part1()
    {
        Insert(steps: 10);
        return counts.Values.Max() - counts.Values.Min();
    }

    protected override long Part2()
    {
        Insert(steps: 40);
        return counts.Values.Max() - counts.Values.Min();
    }

    private List<char> GetTemplate() => data[0].ToCharArray().ToList();

    private void Insert(int steps)
    {
        for (int i = 0; i < polymer.Count - 1; i++)
        {
            counts.AddAndIncrease<char, int>(polymer[i]);
            pairs.AddAndIncrease<(char, char), int>((polymer[i], polymer[i + 1]));
        }
        counts.AddAndIncrease<char, int>(polymer[polymer.Count - 1]);

        for (int i = 0; i < steps; i++)
        {
            var searchspace = new List<(char, char)>(pairs.Where(kvp => kvp.Value > 0).Select(kvp => kvp.Key));
            foreach (var (a, b) in searchspace)
            {
                char c;
                if (rules.TryGetValue((a, b), out c))
                {
                    pairs.AddAndIncrease<(char, char), int>((a, c));
                    pairs.AddAndIncrease<(char, char), int>((c, b));
                    counts.AddAndIncrease<char, int>(c);
                    pairs[(a, b)]--;
                }
            }
            var sum = pairs.Values.Sum() + 1;
            var totalcount = counts.Values.Sum();
            Console.WriteLine(totalcount);
        }
    }

    private void InsertOld(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            var modified = new List<char>(polymer);
            int addedCount = 0;
            for (int j = 0; j < polymer.Count - 1; j++)
            {
                char value = '0';
                // if (rules.TryGetValue($"{polymer[j]}{polymer[j + 1]}", out value))
                {
                    modified.Insert(j + 1 + addedCount, value);
                    addedCount++;
                }
            }
            polymer = modified;
        }
    }

    private Dictionary<char, long> CountOccurences()
    {
        var occurrences = new Dictionary<char, long>();
        foreach (char c in polymer)
        {
            if (!occurrences.ContainsKey(c)) occurrences.TryAdd(c, 0);
            occurrences[c]++;
        }
        return occurrences;
    }
}
