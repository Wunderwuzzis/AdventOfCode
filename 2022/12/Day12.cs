namespace AoC;

public class Day12 : Day<int>
{
    private readonly Dictionary<(int x, int y), int> _map = new();
    private readonly Dictionary<(int x, int y), int> _distances = new();
    private readonly (int x, int y) _start;
    private readonly (int x, int y) _end;

    public Day12(string title, int target1 = default, int target2 = default) : base(12, title, target1, target2)
    {
        for (var y = 0; y < Data.Length; y++)
        {
            for (var x = 0; x < Data[0].Length; x++)
            {
                switch (Data[y][x])
                {
                    case 'S':
                        _start = (x, y);
                        _map.Add(_start, 0);
                        break;
                    case 'E':
                        _end = (x, y);
                        _map.Add(_end, 25);
                        break;
                    default:
                        _map.Add((x, y), Data[y][x] - 'a');
                        break;
                }
            }
        }
    }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        DetectDistancesFromEnd();
        part1 = _distances[_start];
        part2 = _distances.Where(x => _map[x.Key] == 0).Min(x => x.Value);
    }

    private void DetectDistancesFromEnd()
    {
        _distances[_end] = 0;
        var queue = new Queue<(int x, int y)>();
        queue.Enqueue(_end);

        while (queue.TryDequeue(out var p))
        {
            bool IsTraversable((int x, int y) n) => _map.ContainsKey(n) && !_distances.ContainsKey(n) && _map[p] - _map[n] <= 1;

            foreach (var n in GetNeighbours(p).Where(IsTraversable))
            {
                _distances[n] = _distances[p] + 1;
                queue.Enqueue(n);
            }
        }
    }

    private static IEnumerable<(int x, int y)> GetNeighbours((int x, int y) point)
    {
        yield return (point.x + 1, point.y);
        yield return (point.x - 1, point.y);
        yield return (point.x, point.y + 1);
        yield return (point.x, point.y - 1);
    }
}