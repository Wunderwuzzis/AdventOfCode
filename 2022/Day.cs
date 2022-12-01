using System.Diagnostics;

namespace AoC;

public abstract class Day<T> : IDay where T : struct
{
    protected readonly string[] Data;

    private readonly string _title;
    private readonly T? _target1;
    private readonly T? _target2;

    protected Day(int day, string title, T? target1 = null, T? target2 = null)
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

    private static void ExecutePart(Func<T> part1, T? target)
    {
        Timer.StartLap();
        var result = part1();
        CompareResult(result, target);
        Timer.LogLap();
    }

    private static void CompareResult(T result, T? target)
    {
        if (target.HasValue && !result.Equals(target))
        {
            Console.Write($"! result {result} does not match goal {target}");
        }
        else if (target.HasValue)
        {
            Console.Write($"  {result,-20}");
        }
        else
        {
            Console.Write($"  result: {result,-20}");
        }
    }

    protected abstract T Part1();
    protected abstract T Part2();
}