namespace AoC;

public static class DataReader
{
    public static string[] Read(int day)
    {
        var dataPath = GetDataPath(day);

        if (!File.Exists(dataPath))
            throw new InvalidOperationException($"Data file for day {day} does not exist!");

        return File.ReadAllLines(dataPath);
    }

    private static string GetDataPath(int day)
    {
        return $"{day:00}/data";
    }
}