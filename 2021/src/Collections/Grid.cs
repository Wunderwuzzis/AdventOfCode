using System;
using System.Diagnostics;

namespace AoC.Collections;
#nullable disable annotations

public class Grid<T>
{
    protected (int x, int y) dim; // grid dimensions
    protected T[,] content;

    public Grid(int size)
    {
        dim = (size, size);
        content = new T[size, size];
        content.Fill<T>(default);
    }
    
    public Grid(int xSize,  int ySize)
    {
        dim = (xSize, ySize);
        content = new T[xSize, ySize];
        content.Fill<T>(default);
    }

    protected void AssertInXRange(int x) => Debug.Assert(x >= 0 && x < dim.x);
    protected void AssertInYRange(int y) => Debug.Assert(y >= 0 && y < dim.y);
}