using Directory = AoC.D7.Directory;
using File = AoC.D7.File;

namespace AoC;

public class Day7 : Day<int>
{
    private readonly HashSet<Directory> _directories = new();
    private readonly Directory _root = new("/");

    public Day7(string title, int target1 = default, int target2 = default) : base(7, title, target1, target2) { }

    protected override void ExecuteParts(out int part1, out int part2)
    {
        MapDirectories();

        part1 = _directories.Where(d => d.Size < 100000).Sum(d => d.Size);

        var minimum = _root.Size - 40000000; // min space to free
        part2 = _directories.Where(d => d.Size >= minimum).Min(d => d.Size);
    }

    private void MapDirectories()
    {
        _directories.Add(_root);
        var currentDirectory = _root;

        foreach (var line in Data)
        {
            var commands = line.Split(' ');
            switch (commands[0])
            {
                case "$" when commands[1] == "cd":
                    currentDirectory = ChangeDirectory(commands[2], currentDirectory);
                    break;
                case "$" when commands[1] == "ls":
                    break;
                case "dir":
                    var newDir = new Directory(commands[1]);
                    _directories.Add(newDir);
                    currentDirectory.SubDirectories.Add(newDir);
                    break;
                default:
                    currentDirectory.Files.Add(new File(commands[1], int.Parse(commands[0])));
                    break;
            }
        }

        foreach (var directory in _directories)
            directory.CalculateSize();
    }

    private Directory ChangeDirectory(string target, Directory current) => target switch
    {
        "/" => _root,
        ".." => GetParentOf(current),
        { } => current.GetSubDirectory(target),
        _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
    };

    private Directory GetParentOf(Directory target)
    {
        return _directories.Single(d => d.SubDirectories.Contains(target));
    }
}