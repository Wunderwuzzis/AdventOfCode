using System.Text.RegularExpressions;

namespace AoC.Utils;

public static class StringExtensions
{
    private static readonly Regex IntRegex = new(@"\d+");

    public static int ParseInt(this string text)
    {
        return int.Parse(IntRegex.Match(text).Value);
    }

    public static int[] ParseAllInt(this string text, int maxOccurrences = 0)
    {
        var integers = IntRegex.Matches(text)
            .Select(m => int.Parse(m.Value))
            .ToArray();
        return maxOccurrences > 0 ? integers.Take(maxOccurrences).ToArray() : integers;
    }

    public static int[] ExtractAllDigits(this string text, int maxOccurrences = 0)
    {
        var integers = Regex.Matches(text, @"\d")
            .Select(m => int.Parse(m.Value))
            .ToArray();
        return maxOccurrences > 0 ? integers.Take(maxOccurrences).ToArray() : integers;
    }

    public static bool IsLower(this string value) => value.All(char.IsLower);

    public static bool IsUpper(this string value) => value.All(char.IsUpper);
}