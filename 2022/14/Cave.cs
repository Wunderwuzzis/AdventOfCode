using System.Numerics;

namespace AoC;

public class Cave
{
    private static readonly Vector2 SandSource = new(500, 0);
    private static readonly Vector2 Down = new(0, 1);
    private static readonly Vector2 Left = new(-1, 1);
    private static readonly Vector2 Right = new(1, 1);

    private readonly Dictionary<Vector2, char> _map;
    private readonly int _height;
    private readonly bool _hasFloor;

    private int SandCount => _map.Values.Count(x => x == 'o');

    public Cave(IEnumerable<Vector2[]> lines, bool hasFloor)
    {
        _hasFloor = hasFloor;
        _map = new Dictionary<Vector2, char>();

        foreach (var steps in lines)
            for (var i = 1; i < steps.Length; i++)
                FillWithRocks(steps[i - 1], steps[i]);

        _height = (int) _map.Keys.Select(pos => pos.Y).Max();
    }

    private void FillWithRocks(Vector2 from, Vector2 to)
    {
        var dir = new Vector2(Math.Sign(to.X - from.X), Math.Sign(to.Y - from.Y));

        for (var pos = from; pos != to + dir; pos += dir)
            _map[pos] = '#';
    }

    public int FillWithSand()
    {
        Vector2 location;
        do
        {
            location = DropSand(SandSource);

            if (!_hasFloor && location.Y > _height)
                return SandCount;

        } while (_map.TryAdd(location, 'o'));

        return SandCount;
    }

    private Vector2 DropSand(Vector2 sand)
    {
        while (sand.Y <= _height)
        {
            if (!_map.ContainsKey(sand + Down))
                sand += Down;
            else if (!_map.ContainsKey(sand + Left))
                sand += Left;
            else if (!_map.ContainsKey(sand + Right))
                sand += Right;
            else
                break;
        }

        return sand;
    }
}