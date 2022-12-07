namespace AoC.D7;

public class Directory
{
    private string Id { get; }
    public HashSet<Directory> SubDirectories { get; } = new();
    public HashSet<File> Files { get; } = new();

    public Directory(string id)
    {
        Id = id;
    }

    public long GetSize()
    {
        return Files.Sum(f => f.Size) + SubDirectories.Sum(d => d.GetSize());
    }

    public Directory GetParentOf(Directory target)
    {
        return FindParent(target) ?? throw new NullReferenceException($"No parent found for directory: {target.Id}.");
    }

    private Directory? FindParent(Directory target)
    {
        if (SubDirectories.Contains(target))
            return this;

        foreach (var directory in SubDirectories)
        {
            var d = directory.FindParent(target);
            if (d != null)
                return d;
        }

        return null;
    }

    public Directory GetSubDirectory(string target)
    {
        return SubDirectories.FirstOrDefault(d => d.Id == target)
               ?? throw new NullReferenceException($"No directory found with name: {target}.");
    }
}