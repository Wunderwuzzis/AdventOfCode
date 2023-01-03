using System.Text.RegularExpressions;
using AoC.Utils;

namespace AoC;

public class Day15 : Day<int>
{
    private readonly int _width;
    private readonly int _height;
    private readonly bool[,] _map;

    public Day15(string title, int target1 = default, int target2 = default) : base(15, title, target1, target2)
    {
        var xRegex = new Regex(@"x=-?\d+");
        _width = xRegex.Matches(RawData).Select(match => match.Value.ParseInt()).Max() + 1;

        var yRegex = new Regex(@"y=-?\d+");
        _height = yRegex.Matches(RawData).Select(match => match.Value.ParseInt()).Max() + 1;

        _map = new bool[_height, _width];

        Console.WriteLine($"height {_height}, width {_width}");
    }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        foreach (var line in Data)
        {
            var (sensorX, sensorY, targetX, targetY) = line.ParseAllInt();

            var deltaX = Math.Abs(targetX - sensorX);
            var deltaY = Math.Abs(targetY - sensorY);
            var manhattenDist = deltaX + deltaY;

            Console.WriteLine($"pX {sensorX}, pY {sensorY}, dX {deltaX}, dY {deltaY}");

            for (var x = sensorX - deltaX; x <= sensorX + deltaX; x++)
            {
                for (var y = sensorY - deltaY; y <= sensorY + deltaY; y++)
                {
                    if (Math.Abs(sensorX - x) + Math.Abs(sensorY - y) > manhattenDist)
                        continue;

                    SetScannedPos(x, y);
                }
            }
        }

        _map.Print();

        part1 = 0;
        part2 = 0;
    }

    private void SetScannedPos(int x, int y)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _height)
            return;

        _map[y, x] = true;
    }
}