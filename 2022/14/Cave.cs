using System.Numerics;

namespace AoC;

public class Cave
{
    private readonly Dictionary<Vector2, char> _map;
    private readonly int _height;
    private static readonly Vector2 SandSource = new(500, 0);
    private static readonly Vector2[] FallingDirections = { new(0, 1), new(-1, 1), new(1, 1) };
    public int SandAmount => _map.Values.Count(x => x == 'o');

    public Cave(IEnumerable<Vector2[]> lines)
    {
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

    public void FillWithSand(bool hasFloor)
    {
        var falling = true;
        while (falling)
            falling = DropSand(hasFloor);
    }

    private bool DropSand(bool hasFloor)
    {
        var sand = SandSource;
        while (sand.Y <= _height)
            foreach (var dir in FallingDirections)
                if (!_map.ContainsKey(sand + dir))
                    return _map.TryAdd(sand + dir, 'o');

        return hasFloor && _map.TryAdd(sand, 'o');
    }
}