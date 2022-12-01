using System.Diagnostics;

namespace AoC;

public static class Timer
{
    private static readonly Stopwatch Watch = new();
    private static double _total;

    public static void Start()
    {
        _total = 0;
        Watch.Start();
    }

    public static void StartLap()
    {
        Watch.Restart();
    }

    public static void LogLap()
    {
        Watch.Stop();

        var lapTime = Watch.Elapsed.TotalMilliseconds;
        _total += lapTime;

        Console.WriteLine($"( {lapTime:0.000} ms)");
    }

    public static void LogTotal()
    {
        Watch.Stop();

        Console.WriteLine($"Total Execution Time: {_total:0.000} ms");
    }
}