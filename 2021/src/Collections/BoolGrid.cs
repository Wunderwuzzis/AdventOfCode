namespace AoC.Collections;

public class BoolGrid : Grid<bool>
{
    public BoolGrid(int xSize, int ySize) : base(xSize, ySize)
    {
    }

    public void AddBulk(IEnumerable<Point> points)
    {
        foreach (Point p in points)
        {
            AddDot(p.x, p.y);
        }
    }

    public void AddDot(int x, int y)
    {
        AssertInXRange(x);
        AssertInYRange(y);
        content[x, y] = true;
    }

    public int CountSet()
    {
        int result = 0;
        for (int y = 0; y < dim.y; y++)
        {
            for (int x = 0; x < dim.x; x++)
            {
                if (content[x, y])
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
                Console.Write(content[x, y] ? "#" : ".");
            }
            Console.WriteLine();
        }
    }
}