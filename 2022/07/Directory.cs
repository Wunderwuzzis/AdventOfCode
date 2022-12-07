namespace AoC.D7;

public class Directory
{
    private string Id { get; }
    public HashSet<Directory> SubDirectories { get; } = new();
    public List<File> Files { get; } = new();
    public int Size { get; private set; }

    public Directory(string id)
    {
        Id = id;
    }

    public void CalculateSize()
    {
        Size = GetSize();
    }

    private int GetSize()
    {
        return Files.Sum(f => f.Size) + SubDirectories.Sum(d => d.GetSize());
    }

    public Directory GetSubDirectory(string target)
    {
        return SubDirectories.First(d => d.Id == target);
    }
}