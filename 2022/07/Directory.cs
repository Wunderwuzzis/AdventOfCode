namespace AoC.D7;

public class Directory
{
    private readonly string _id;
    private readonly HashSet<Directory> _directories;
    private readonly HashSet<File> _files;

    public string Id => _id;
    public HashSet<Directory> Directories => _directories;
    public HashSet<File> Files => _files;

    public Directory(string id)
    {
        _id = id;
        _files = new HashSet<File>();
        _directories = new HashSet<Directory>();
    }

    public void FindAllUnder100000(ref List<Directory> results)
    {
        if (GetSize() <= 100000)
            results.Add(this);

        foreach (var directory in Directories)
            directory.FindAllUnder100000(ref results);
    }

    public long GetSize()
    {
        return Files.Sum(f => f.Size) + Directories.Sum(d => d.GetSize());
    }

    public Directory GetParentOf(Directory target)
    {
        return FindParent(target) ?? throw new NullReferenceException($"No parent found for directory: {target._id}.");
    }

    private Directory? FindParent(Directory target)
    {
        if (Directories.Contains(target))
            return this;

        foreach (var directory in Directories)
        {
            var d = directory.FindParent(target);
            if (d != null)
                return d;
        }

        return null;
    }

    public Directory GetSubDirectory(string target)
    {
        return Directories.FirstOrDefault(d => d._id == target)
               ?? throw new NullReferenceException($"No directory found with name: {target}.");
    }

    public void Print(string indent = "")
    {
        Console.WriteLine($"{indent}- {_id}/");
        indent = $"{indent}  ";

        foreach (var dir in Directories)
            dir.Print(indent);

        foreach (var file in Files)
            Console.WriteLine($"{indent}- {file.Filename}, size: {file.Size}");

        indent = indent[..^2];
    }
}