using System.Diagnostics;

namespace AoC;

public abstract class Day<TResult, TTarget> : IDay
{
    protected readonly string[] Data;

    private readonly string _title;
    private readonly TTarget? _target1;
    private readonly TTarget? _target2;

    protected Day(int day, string title, TTarget? target1, TTarget? target2)
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

    private static void ExecutePart(Func<TResult> part1, TTarget? target)
    {
        Timer.StartLap();
        var result = part1();
        Console.Write(CompareResult(result, target));
        Timer.LogLap();
    }

    private static string CompareResult(TResult result, TTarget? target) => result switch
    {
        null => "  No result found!",
        { } when target == null => $"  result: {result,-20}",
        { } when result.Equals(target) => $"  {result,-20}",
        { } => $"! result {result} does not match goal {target}",
    };

    protected abstract TResult Part1();
    protected abstract TResult Part2();
}