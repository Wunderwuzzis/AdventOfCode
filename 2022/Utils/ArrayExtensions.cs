namespace AoC.Utils;

public static class ArrayExtensions
{
    public static void Print<T>(this T[][] array)
    {
        Console.WriteLine(string.Join(Environment.NewLine, array.Select(line => string.Join(", ", line))));
    }

    public static void PrintWithHighlight<T>(this T[][] array, T highlight)
    {
        var foreground = Console.ForegroundColor;
        foreach (var line in array)
        {
            foreach (var item in line)
            {
                if (item != null && item.Equals(highlight))
                    Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{item}, ");
                Console.ForegroundColor = foreground;
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    public static void Deconstruct<T>(this T[] array, out T a0, out T a1)
    {
        if (array is not { Length: 2 })
            throw new ArgumentException("Cannot deconstruct array. Length for tuple needs to be 2!", nameof(array));
        a0 = array[0];
        a1 = array[1];
    }

    public static void Fill<T>(this T[,] array, T value)
    {
        for (var y = 0; y < array.GetLength(1); y++)
        {
            for (var x = 0; x < array.GetLength(0); x++)
            {
                array[x, y] = value;
            }
        }
    }
}