using System.Diagnostics;

namespace AoC;

public static class Timer
{
    private static readonly Stopwatch Watch = new();
    private static readonly Stopwatch Total = new();

    public static void Start()
    {
        Watch.Start();
        Total.Start();
    }

    private static void Stop()
    {
        Watch.Stop();
        Total.Stop();
    }

    public static void StartLap()
    {
        Watch.Restart();
    }

    public static void LogLap()
    {
        Watch.Stop();
        Console.WriteLine($"( {Watch.Elapsed.TotalMilliseconds} ms)");
    }

    public static void LogTotal()
    {
        Stop();
        Console.WriteLine($"Total Execution Time: {Total.Elapsed.TotalMilliseconds} ms");
    }
}