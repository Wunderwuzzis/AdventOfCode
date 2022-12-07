using System.Diagnostics;

namespace AoC;

public abstract class Day<T> : IDay where T : IEquatable<T>
{
    protected readonly string[] Data;

    private readonly string _title;
    private readonly T _target1;
    private readonly T _target2;

    protected Day(int day, string title, T target1, T target2)
    {
        Data = DataReader.Read(day);
        Debug.Assert(Data.Length > 0);

        _title = title;
        _target1 = target1;
        _target2 = target2;
    }

    public void Execute()
    {
        Console.WriteLine($"{GetType().Name}: {_title}");

        ExecutePart(Part1, _target1);
        ExecutePart(Part2, _target2);

        Console.WriteLine();
    }

    private static void ExecutePart(Func<T> part1, T target)
    {
        Timer.StartLap();
        var result = part1();
        Console.Write(CompareResult(result, target));
        Timer.LogLap();
    }

    private static string CompareResult(T result, T target) => result switch
    {
        null => "  No result found!",
        { } when target.Equals(default) => $"  result: {result,-20}",
        { } when result.Equals(target) => $"  {result,-20}",
        { } => $"! result {result} does not match goal {target}",
    };

    protected abstract T Part1();
    protected abstract T Part2();
}