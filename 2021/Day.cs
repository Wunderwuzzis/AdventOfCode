using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AoC;
#nullable disable annotations

public interface IDay
{
    void Calculate(ref Stopwatch watch);
}

public enum Part
{
    One = 1,
    Two = 2
}

public abstract class Day<T> : IDay
{
    protected string[] data;
    protected abstract T Part1();
    protected abstract T Part2();
    private string title = string.Empty;
    private T first, second = default;
    private List<Action> tasks = new List<Action>();
    protected Part part;

    public Day(string dataPath)
    {
        data = File.ReadAllLines(dataPath);
        Debug.Assert(data.Length > 0);
    }

    public void Calculate(ref Stopwatch watch)
    {
        Console.WriteLine($"{this.GetType().Name}: {title}");

        foreach (var task in tasks)
        {
            watch.Restart();
            task();
            Utils.LogElapsedTime(ref watch);
        }
        Console.WriteLine();
    }

    public Day<T> Title(string value)
    {
        title = value;
        return this;
    }

    public Day<T> First(T value = default)
    {
        first = value;
        tasks.Add(() => { part = Part.One; Part1().CompareResult(first); });
        return this;
    }

    public Day<T> Second(T value = default)
    {
        second = value;
        tasks.Add(() => { part = Part.Two; Part2().CompareResult(second); });
        return this;
    }
}