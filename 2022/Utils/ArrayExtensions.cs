namespace AoC.Utils;

public static class ArrayExtensions
{
    public static void Print<T>(this T[][] array)
    {
        Console.WriteLine(string.Join(Environment.NewLine, array.Select(line => string.Join(", ", line))));
    }

    public static void Print<T>(this T[,] array)
    {
        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                Console.Write(array[i, j] + " ");
            }

            Console.WriteLine();
        }
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
        if (array == null || array.Length < 2)
            throw new ArgumentException($"Cannot deconstruct array. Length for tuple needs to be {2}!", nameof(array));

        a0 = array[0];
        a1 = array[1];
    }

    public static void Deconstruct<T>(this T[] array, out T a0, out T a1, out T a2)
    {
        if (array == null || array.Length < 3)
            throw new ArgumentException($"Cannot deconstruct array. Length needs to be {3}!", nameof(array));

        a0 = array[0];
        a1 = array[1];
        a2 = array[2];
    }

    public static void Deconstruct<T>(this T[] array, out T a0, out T a1, out T a2, out T a3)
    {
        if (array == null || array.Length < 4)
            throw new ArgumentException($"Cannot deconstruct array. Length needs to be {4}!", nameof(array));

        a0 = array[0];
        a1 = array[1];
        a2 = array[2];
        a3 = array[3];
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

    public static T[] Sort<T>(this T[] array, Comparison<T> comparison)
    {
        Array.Sort(array, comparison);
        return array;
    }

    public static int IndexOf<T>(this T[] array, T item)
    {
        return Array.IndexOf(array, item);
    }
}