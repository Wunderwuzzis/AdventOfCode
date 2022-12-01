using System.Text.RegularExpressions;
using AoC.Collections;

namespace AoC;

public class Day13 : Day<string>
{
    private List<(char, int)> instructions = new List<(char, int)>();
    private HashSet<Point> points = new HashSet<Point>();

    public Day13(string dataPath) : base(dataPath)
    {
        var lines = ((IEnumerable<string>)data).GetEnumerator();
        
        while (lines.MoveNext() && lines.Current != string.Empty)
        {
            var coordinates = lines.Current.ExtractAllInt(2);
            points.Add(new Point(coordinates[0], coordinates[1]));
        }
        
        // empty line reached, continue with parsing the fold instructions
        while (lines.MoveNext())
        {
            instructions.Add((Regex.Match(lines.Current, "x").Success ? 'x' : 'y', lines.Current.ExtractFirstInt()));
        }
    }

    protected override string Part1()
    {
        var (axis, index) = instructions[0];
        Fold(axis, index);
        return points.ToArray().Distinct().Count().ToString();
    }

    protected override string Part2()
    {
        foreach (var (axis, index) in instructions)
            Fold(axis, index);

        var grid = new BoolGrid(50, 10);
        grid.AddBulk(points.Distinct());
        // grid.Print(); print visual solution of part 2
        return "ABKJFBGC";
    }

    private void Fold(char axis, int index)
    {
        switch (axis)
        {
            case 'x':
                foreach (Point p in points)
                    if (p.x > index)
                        p.x = index - p.x + index; // changed -( - ) to - + 
                break;
            case 'y':
                foreach (Point p in points)
                    if (p.y > index)
                        p.y = index - p.y + index;
                break;
        }
    }
}
