namespace AoC.Collections;

public class IntGrid : Grid<int>
{
    private bool diagonals;

    public IntGrid(int size, bool allowDiagonals) : base(size)
    {
        diagonals = allowDiagonals;
    }

    public void AddLine(int x1, int y1, int x2, int y2)
    {
        AssertInXRange(x1);
        AssertInXRange(y1);
        AssertInXRange(x2);
        AssertInXRange(y2);
        if (!diagonals && !(x1 == x2 || y1 == y2)) return; // discard diagonal lines

        int hDir, vDir; // horizontal and vertical direction multiplier
        if (x1 == x2) hDir = 0;
        else hDir = x1 < x2 ? 1 : -1;

        if (y1 == y2) vDir = 0;
        else vDir = y1 < y2 ? 1 : -1; // vertical direction multiplier

        int distance = Math.Max(Math.Abs(x2 - x1), Math.Abs(y2 - y1));

        for (int i = 0; i <= distance; i++)
        {
            content[x1, y1]++;
            x1 += hDir;
            y1 += vDir;
        }
    }

    public int CountIntersections()
    {
        int result = 0;
        for (int y = 0; y < dim.y; y++)
        {
            for (int x = 0; x < dim.x; x++)
            {
                if (content[x, y] > 1)
                    result++;
            }
        }
        return result;
    }

    public void Print()
    {
        for (int y = 0; y < dim.y; y++)
        {
            for (int x = 0; x < dim.x; x++)
            {
                Console.Write(content[x, y] == 0 ? "." : content[x, y].ToString());
            }
            Console.WriteLine();
        }
    }
}