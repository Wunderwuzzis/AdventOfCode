using System.Numerics;

namespace AoC;

public class Day9 : Day<int>
{
    public Day9(string title, int target1 = default, int target2 = default) : base(9, title, target1, target2) { }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        var rope = new Vector2[10];
        var visitedPoints1 = new HashSet<Vector2>();
        var visitedPoints2 = new HashSet<Vector2>();

        foreach (var line in Data)
        {
            var direction = GetDirection(line[0]);
            var repeat = int.Parse(line[2..]);

            for (var i = 0; i < repeat; i++)
            {
                rope[0] += direction;

                for (var j = 1; j < rope.Length; j++)
                {
                    FollowRope(ref rope[j], rope[j - 1]);
                }

                visitedPoints1.Add(rope[1]);
                visitedPoints2.Add(rope[9]);
            }
        }

        part1 = visitedPoints1.Count;
        part2 = visitedPoints2.Count;
    }

    private static Vector2 GetDirection(char dir) => dir switch
    {
        'R' => Vector2.UnitX,
        'L' => -Vector2.UnitX,
        'U' => Vector2.UnitY,
        'D' => -Vector2.UnitY,
        _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
    };

    private static void FollowRope(ref Vector2 tail, Vector2 head)
    {
        var delta = head - tail;

        if (Math.Abs(delta.X) <= 1 && Math.Abs(delta.Y) <= 1)
            return;

        tail.X += Math.Sign(delta.X);
        tail.Y += Math.Sign(delta.Y);
    }
}