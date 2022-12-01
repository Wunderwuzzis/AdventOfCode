namespace AoC;

public class Day12 : Day<int>
{
    Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
    Dictionary<string, int> occurrences = new Dictionary<string, int>();

    public Day12(string dataPath) : base(dataPath)
    {
        foreach (string line in data)
        {
            var (node1, node2) = line.Split("-");
            AddEdge(node1, node2);
            AddEdge(node2, node1);
        }
    }

    private void AddEdge(string u, string v)
    {
        if (u == "end" || v == "start") return;
        // if (u,v) is an edge then node u is adjacent to node v, and node v is adjacent to node u.
        if (!connections.ContainsKey(u))
            connections.Add(u, new List<string>());
        connections[u].Add(v);
    }

    protected override int Part1()
    {
        occurrences = connections.Keys.ToDictionary(n => n, c => 0);
        return FindPaths();
    }

    protected override int Part2()
    {
        occurrences = connections.Keys.ToDictionary(n => n, c => 0);
        return FindPaths();
    }

    private int FindPaths(string node = "start", int count = 0, bool anySmallCaveVisitedTwice = false)
    {
        if (node.IsLower() && node != "end" && occurrences[node] > 0)
        {
            if (part == Part.One || anySmallCaveVisitedTwice)
                return count;
            // otherwise it is allowed to visit, but this iteration will use up the two visits
            anySmallCaveVisitedTwice = true;
        }

        if (node == "end")
            return ++count;
        
        occurrences[node]++;
        foreach (string n in connections[node])
        {
            count = FindPaths(n, count, anySmallCaveVisitedTwice);
        }
        occurrences[node]--;
        return count;
    }
}
