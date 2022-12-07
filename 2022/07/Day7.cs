using Directory = AoC.D7.Directory;
using File = AoC.D7.File;

namespace AoC;

public class Day7 : Day<long>
{
    private readonly HashSet<Directory> _directories;
    private readonly Directory _root = new("/");
    private readonly Directory _currentDirectory;

    public Day7(string title, long target1 = default, long target2 = default) : base(7, title, target1, target2)
    {
        _directories = new HashSet<Directory> { _root };
        _currentDirectory = _root;

        foreach (var line in Data)
        {
            var commands = line.Split(' ');
            switch (commands[0])
            {
                case "$" when commands[1] == "cd":
                    _currentDirectory = ChangeDirectory(commands[2]);
                    break;
                case "$" when commands[1] == "ls":
                    break;
                case "dir":
                    var newDir = new Directory(commands[1]);
                    _directories.Add(newDir);
                    _currentDirectory.SubDirectories.Add(newDir);
                    break;
                default:
                    _currentDirectory.Files.Add(new File(commands[1], long.Parse(commands[0])));
                    break;
            }
        }

        foreach (var directory in _directories)
        {
            directory.CalculateSize();
        }
    }

    private Directory ChangeDirectory(string target) => target switch
    {
        "/" => _root,
        ".." => GetParentOf(_currentDirectory),
        { } => _currentDirectory.GetSubDirectory(target),
        _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
    };

    private Directory GetParentOf(Directory target)
    {
        return _directories.Single(d => d.SubDirectories.Contains(target));
    }

    protected override long Part1()
    {
        return _directories.Where(d => d.Size < 100000).Sum(d => d.Size);
    }

    protected override long Part2()
    {
        var minimum = _root.Size - 40000000; // min space to free
        return _directories.Where(d => d.Size >= minimum).Min(d => d.Size);
    }
}