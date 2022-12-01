namespace AoC.Utils;

public static class DictionaryExtensions
{
    public static void Print<TKey, TValue>(this Dictionary<TKey, TValue> dict) where TKey : notnull
    {
        Console.WriteLine(string.Join(Environment.NewLine, dict));
    }

    public static TValue? GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
        TKey key) where TKey : notnull
    {
        if (dictionary == null)
            throw new ArgumentNullException(nameof(dictionary));
        if (key == null)
            throw new ArgumentNullException(nameof(key));

        dictionary.TryGetValue(key, out var value);
        return value;
    }

    public static void AddAndIncrease<TKey>(this Dictionary<TKey, int> dict, TKey key) where TKey : notnull
    {
        if (!dict.ContainsKey(key)) dict.Add(key, 0);
        dict[key]++;
    }
}