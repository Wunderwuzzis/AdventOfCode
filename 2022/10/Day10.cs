namespace AoC;

public class Day10 : Day<int>
{
    public Day10(string title, int target1 = default, int target2 = default) : base(10, title, target1, target2) { }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        var signal = 0;
        var cycle = 1;
        var x = 1;
        var output = new char[40 * 6]; // screen width * height

        foreach (var line in Data)
        {
            Cycle();

            if (line == "noop") continue;

            x += int.Parse(line[5..]);
            Cycle();
        }

        void Cycle()
        {
            cycle++;
            signal += (cycle - 20) % 40 == 0 ? cycle * x : 0;
            output[cycle - 1] = Math.Abs((cycle - 1) % 40 - x) <= 1 ? '#' : ' ';
        }

        Console.WriteLine();
        Console.WriteLine(string.Join("\n", output.Chunk(40).Select(c => new string(c))));
        // RKAZAJBR

        part1 = signal;
        part2 = 0;
    }
}