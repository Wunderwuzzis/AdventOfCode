using AoC.Utils;

namespace AoC;

public class Day9 : Day<int>
{
    private const int RopeLength = 10;
    private readonly Dictionary<char, Vector2Int> _directions = new()
    {
        { 'R', new Vector2Int(1, 0) },
        { 'L', new Vector2Int(-1, 0) },
        { 'U', new Vector2Int(0, 1) },
        { 'D', new Vector2Int(0, -1) }
    };

    public Day9(string title, int target1 = default, int target2 = default) : base(9, title, target1, target2) { }

    protected override (int, int) ExecuteParts()
    {
        var rope = new Vector2Int[RopeLength];
        var visitedPoints1 = new HashSet<Vector2Int>();
        var visitedPoints2 = new HashSet<Vector2Int>();

        foreach (var line in Data)
        {
            var direction = _directions[line[0]];
            var repeat = line.ParseInt();

            for (var i = 0; i < repeat; i++)
            {
                rope[0] += direction;

                for (var j = 1; j < RopeLength; j++)
                {
                    FollowRope(ref rope[j], rope[j - 1]);
                }

                visitedPoints1.Add(rope[1]);
                visitedPoints2.Add(rope[9]);
            }
        }

        return (visitedPoints1.Count, visitedPoints2.Count);
    }

    private static void FollowRope(ref Vector2Int tail, Vector2Int head)
    {
        var deltaX = head.X - tail.X;
        var deltaY = head.Y - tail.Y;

        if (Math.Abs(deltaX) <= 1 && Math.Abs(deltaY) <= 1) return;

        tail.X += deltaX == 0 ? 0 : Math.Sign(deltaX);
        tail.Y += deltaY == 0 ? 0 : Math.Sign(deltaY);
    }
}