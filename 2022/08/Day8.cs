namespace AoC;

public class Day8 : Day<int>
{
    private readonly int[][] _trees;
    private readonly bool[][] _visibleTrees;

    public Day8(string title, int target1 = default, int target2 = default) : base(8, title, target1, target2)
    {
        _trees = Data.Select(line => line.ToCharArray().Select(c => (int) char.GetNumericValue(c)).ToArray()).ToArray();
        _visibleTrees = Data.Select(line => line.ToCharArray().Select(_ => false).ToArray()).ToArray();
    }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        part1 = Part1();
        part2 = Part2();
    }

    private int Part1()
    {
        for (var row = 0; row < _trees.Length; row++)
        {
            MarkRowLeft(row);
            MarkRowRight(row);
        }

        for (var col = 0; col < _trees[0].Length; col++)
        {
            MarkColumnTop(col);
            MarkColumnBottom(col);
        }

        return _visibleTrees.Sum(x => x.Count(y => y));
    }

    private void MarkRowLeft(int rowIndex)
    {
        var max = -1;
        for (var i = 0; i < _trees[rowIndex].Length; i++)
        {
            if (_trees[rowIndex][i] > max)
            {
                _visibleTrees[rowIndex][i] = true;
                max = _trees[rowIndex][i];
            }
        }
    }

    private void MarkRowRight(int rowIndex)
    {
        var max = -1;
        for (var i = _trees[rowIndex].Length - 1; i >= 0; i--)
        {
            if (_trees[rowIndex][i] > max)
            {
                _visibleTrees[rowIndex][i] = true;
                max = _trees[rowIndex][i];
            }
        }
    }

    private void MarkColumnTop(int colIndex)
    {
        var max = -1;
        for (var i = 0; i < _trees.Length; i++)
        {
            if (_trees[i][colIndex] > max)
            {
                _visibleTrees[i][colIndex] = true;
                max = _trees[i][colIndex];
            }
        }
    }

    private void MarkColumnBottom(int colIndex)
    {
        var max = -1;
        for (var i = _trees.Length - 1; i >= 0; i--)
        {
            if (_trees[i][colIndex] > max)
            {
                _visibleTrees[i][colIndex] = true;
                max = _trees[i][colIndex];
            }
        }
    }

    private int Part2()
    {
        return CalculateTreehouseScores().Max(row => row.Max());
    }

    private IEnumerable<int[]> CalculateTreehouseScores()
    {
        var treehouseScores = _trees.Select(row => row.Select(_ => 0).ToArray()).ToArray();

        for (var row = 0; row < _trees.Length; row++)
        {
            for (var col = 0; col < _trees[0].Length; col++)
            {
                var houseHeight = _trees[row][col];
                var left = CountVisibleTreesInLineOfSight(_trees[row][..col].Reverse(), houseHeight);
                var right = CountVisibleTreesInLineOfSight(_trees[row][(col + 1)..], houseHeight);
                var top = CountVisibleTreesInLineOfSight(_trees[..row].Select(x => x[col]).Reverse(), houseHeight);
                var bottom = CountVisibleTreesInLineOfSight(_trees[(row + 1)..].Select(x => x[col]), houseHeight);

                treehouseScores[row][col] = left * right * top * bottom;
            }
        }

        return treehouseScores;
    }

    private static int CountVisibleTreesInLineOfSight(IEnumerable<int> lineOfSight, int houseHeight)
    {
        var count = 0;
        foreach (var height in lineOfSight)
        {
            if (height >= houseHeight)
            {
                count++;
                break;
            }

            count++;
        }

        return count;
    }
}