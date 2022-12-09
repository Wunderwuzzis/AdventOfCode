using AoC.Utils;

namespace AoC;

public class Day9 : Day<int>
{
    private readonly Dictionary<char, Vector2Int> _directions = new()
    {
        { 'R', new Vector2Int(1, 0) },
        { 'L', new Vector2Int(-1, 0) },
        { 'U', new Vector2Int(0, 1) },
        { 'D', new Vector2Int(0, -1) }
    };

    public Day9(string title, int target1 = default, int target2 = default) : base(9, title, target1, target2) { }

    protected override int Part1()
    {
        var visitedPoints = new HashSet<Vector2Int>();

        var tail = new Vector2Int(0, 0);
        var head = new Vector2Int(0, 0);

        foreach (var line in Data)
        {
            var instructions = line.Split(' ');
            var direction = _directions[instructions[0][0]];
            var amount = int.Parse(instructions[1]);

            for (var i = 0; i < amount; i++)
            {
                head += direction;
                FollowRope(ref tail, head);
                visitedPoints.Add(tail);
            }
        }

        return visitedPoints.Count;
    }

    private static void FollowRope(ref Vector2Int tail, Vector2Int head)
    {
        var deltaX = head.X - tail.X;
        var deltaY = head.Y - tail.Y;

        if (ShouldMove(deltaX, deltaY))
            tail += CalculateMove(deltaX, deltaY);
    }

    private static bool ShouldMove(int deltaX, int deltaY)
    {
        return Math.Abs(deltaX) > 1 || Math.Abs(deltaY) > 1;
    }

    private static Vector2Int CalculateMove(int deltaX, int deltaY)
    {
        var x = deltaX == 0 ? 0 : 1 * Math.Sign(deltaX);
        var y = deltaY == 0 ? 0 : 1 * Math.Sign(deltaY);
        return new Vector2Int(x, y);
    }

    protected override int Part2()
    {
        var visitedPoints = new HashSet<Vector2Int>();

        const int ropeLength = 10;
        var rope = new Vector2Int[ropeLength];

        foreach (var line in Data)
        {
            var instructions = line.Split(' ');
            var direction = _directions[instructions[0][0]];
            var amount = int.Parse(instructions[1]);

            for (var i = 0; i < amount; i++)
            {
                rope[0] += direction;

                for (var j = 1; j < ropeLength; j++)
                {
                    FollowRope(ref rope[j], rope[j - 1]);
                }

                visitedPoints.Add(rope[9]);
            }
        }

        return visitedPoints.Count;
    }
}