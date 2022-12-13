using System.Diagnostics;

namespace AoC;

public static class Timer
{
    private static readonly Stopwatch Watch = new();
    private static double _total;
    private static double _lastLap;

    public static void Start()
    {
        _total = 0;
        Watch.Start();
    }

    public static void StartLap()
    {
        Watch.Restart();
    }

    public static void TimeLap()
    {
        Watch.Stop();

        _lastLap = Watch.Elapsed.TotalMilliseconds;
    }

    public static void LogLastLap()
    {
        _total += _lastLap;

        Console.Write($"( {_lastLap:0.000} ms)");
    }

    public static void LogTotal()
    {
        Watch.Stop();

        Console.WriteLine($"Total Execution Time: {_total:0.000} ms");
    }
}