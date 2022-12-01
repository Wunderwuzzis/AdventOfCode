using System.Diagnostics;

namespace AoC;

class AoC2021
{
    private static readonly bool executeAll = false;
    private static readonly int day = 14;

    private static void Main(string[] args)
    {
        var watch = new Stopwatch();
        var days = new Dictionary<int, IDay>();
        days[1] = new Day1(DataPath(1))
                            .Title("Sonar Sweep")
                            .First(1400)
                            .Second(1429);
        days[2] = new Day2(DataPath(2))
                            .Title("Dive!")
                            .First(2120749)
                            .Second(2138382217);
        days[3] = new Day3(DataPath(3))
                            .Title("Binary Diagnostic")
                            .First(3969000)
                            .Second(4267809);
        days[4] = new Day4(DataPath(4))
                            .Title("Giant Squid")
                            .First(89001)
                            .Second(7296);
        days[5] = new Day5(DataPath(5))
                            .Title("Hydrothermal Venture")
                            .First(5690)
                            .Second(17741);
        days[6] = new Day6(DataPath(6))
                            .Title("Lanternfish")
                            .First(345387)
                            .Second(1574445493136);
        days[7] = new Day7(DataPath(7))
                            .Title("The Treachery of Whales")
                            .First(328187)
                            .Second(91257582);
        days[8] = new Day8(DataPath(8))
                            .Title("Seven Segment Search")
                            .First(504)
                            .Second(1073431);
        days[9] = new Day9(DataPath(9))
                            .Title("Smoke Basin")
                            .First(500)
                            .Second(970200);
        days[10] = new Day10(DataPath(10))
                            .Title("Syntax Scoring")
                            .First(323691)
                            .Second(2858785164);
        days[11] = new Day11(DataPath(11))
                            .Title("Dumbo Octopus")
                            .First(1688)
                            .Second(403);
        days[12] = new Day12(DataPath(12))
                            .Title("Passage Pathing")
                            .First(4720)
                            .Second(147848);
        days[13] = new Day13(DataPath(13))
                            .Title("Transparent Origami")
                            .First("745")
                            .Second("ABKJFBGC");
        days[14] = new Day14(DataPath(14))
                            .Title("Extended Polymerization")
                            .First(2768)
                            .Second();

        watch.Start();
        var totalTime = System.Diagnostics.Stopwatch.StartNew();
        if (executeAll)
            foreach (var day in days.Values)
                day.Calculate(ref watch);
        else
            days[day].Calculate(ref watch);
        watch.Stop();

        totalTime.Stop();
        Console.WriteLine($"Total Execution Time: {totalTime.Elapsed.TotalMilliseconds} ms");
    }

    private static string DataPath(int day)
    {
        return $"input/2021/data{day}.txt";
    }
}
