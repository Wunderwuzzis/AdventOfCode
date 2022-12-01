namespace AoC;

public static class AdventOfCode
{
    public static void Run(IDay day)
    {
        Timer.Start();

        day.Execute();

        Timer.LogTotal();
    }

    public static void Run(Dictionary<int, IDay> days)
    {
        Timer.Start();

        foreach (var day in days.Values)
            day.Execute();

        Timer.LogTotal();
    }
}