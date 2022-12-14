using System.Diagnostics;

namespace AoC;

public abstract class Day<T> : IDay where T : IEquatable<T>
{
    protected readonly string RawData;
    protected readonly string[] Data;

    private readonly string _title;
    private readonly T _target1;
    private readonly T _target2;

    protected Day(int day, string title, T target1, T target2)
    {
        RawData = DataReader.Read(day);
        Data = RawData.Split('\n');
        Debug.Assert(Data.Length > 0);

        _title = title;
        _target1 = target1;
        _target2 = target2;
    }

    public void Execute()
    {
        Console.Write("{0, -40}", $"{GetType().Name}: {_title}");

        Timer.StartLap();
        ExecuteParts(out var part1, out var part2);
        Timer.TimeLap();

        Console.Write(CompareResult(part1, _target1));
        Console.Write(CompareResult(part2, _target2));
        Timer.LogLastLap();

        Console.WriteLine();
    }

    protected abstract void ExecuteParts(out T part1, out T part2);

    private static string CompareResult(T result, T target) => result switch
    {
        null => "  No result found!",
        { } when target.Equals(default) => $"  result: {result,-10}",
        { } when result.Equals(target) => $"  {result,-18}",
        { } => $"! result {result} does not match goal {target}",
    };
}