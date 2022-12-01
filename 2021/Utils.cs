using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AoC;
#nullable disable annotations

public sealed record Point
{
    public int x, y;
    public Point(int x, int y) { this.x = x; this.y = y; }
}

public static class Utils
{
    public static Regex intRegex = new Regex(@"\d+");

    public static void CompareResult<T>(this T result, T goal)
    {
        var goalSet = !NullOrDefault<T>(goal);
        if (goalSet && !result.Equals(goal))
        {
            Console.Write(String.Format("! result {0} does not match goal {1}", result, goal));
            // throw new InvalidOperationException();
        }

        Console.Write(String.Format("  {0}{1, -20}", goalSet ? "" : "result: ", result));
    }

    public static void LogElapsedTime(ref Stopwatch watch)
    {
        Console.WriteLine($"( {watch.Elapsed.TotalMilliseconds} ms)");
        watch.Restart();
    }

    // source: https://stackoverflow.com/questions/65351/null-or-default-comparison-of-generic-argument-in-c-sharp
    private static bool NullOrDefault<T>(T obj) => EqualityComparer<T>.Default.Equals(obj, default(T));

    public static int ExtractFirstInt(this string text)
    {
        return int.Parse(intRegex.Match(text).Value);
    }

    public static int[] ExtractAllInt(this string text, int maxOccurrences = 0)
    {
        var integers = intRegex.Matches(text)
            .Cast<Match>()
            .Select(m => int.Parse(m.Value))
            .ToArray();
        return maxOccurrences > 0 ? integers.Take(maxOccurrences).ToArray() : integers;
    }

    public static int[] ExtractAllDigits(this string text, int maxOccurrences = 0)
    {
        var integers = Regex.Matches(text, @"\d")
            .Cast<Match>()
            .Select(m => int.Parse(m.Value))
            .ToArray();
        return maxOccurrences > 0 ? integers.Take(maxOccurrences).ToArray() : integers;
    }

    public static void Fill<T>(this T[,] array, T value)
    {
        for (int y = 0; y < array.GetLength(1); y++)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                array[x, y] = value;
            }
        }
    }

    public static void Print<TKey, TValue>(this Dictionary<TKey, TValue> dict)
    {
        Console.WriteLine(string.Join(Environment.NewLine, dict));
    }

    public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, out bool valueFound, TValue defaultValue = default(TValue))
    {
        if (dictionary == null) { throw new ArgumentNullException(nameof(dictionary)); }
        if (key == null) { throw new ArgumentNullException(nameof(key)); }

        TValue value;
        valueFound = dictionary.TryGetValue(key, out value);
        return valueFound ? value : defaultValue;
    }

    public static void AddAndIncrease<TKey, Int32>(this Dictionary<TKey, int> dict, TKey key)
    {
        if (!dict.ContainsKey(key)) dict.Add(key, 0);
        dict[key]++;
    }

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
                if (item.Equals(highlight))
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
            throw new ArgumentException(nameof(array));
        a0 = array[0];
        a1 = array[1];
    }

    public static bool IsUpper(this string value)
    {
        // Consider string to be uppercase if it has no lowercase letters.
        for (int i = 0; i < value.Length; i++)
            if (char.IsLower(value[i]))
                return false;
        return true;
    }

    public static bool IsLower(this string value)
    {
        // Consider string to be lowercase if it has no uppercase letters.
        for (int i = 0; i < value.Length; i++)
            if (char.IsUpper(value[i]))
                return false;
        return true;
    }
}