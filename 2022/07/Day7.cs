using Directory = AoC.D7.Directory;
using File = AoC.D7.File;

namespace AoC;

public class Day7 : Day<long, long?>
{
    private readonly Directory _root = new("/");
    private Directory _currentDirectory;

    public Day7(string title, long? target1, long? target2) : base(7, title, target1, target2)
    {
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
                    _currentDirectory.Directories.Add(new Directory(commands[1]));
                    break;
                default:
                    _currentDirectory.Files.Add(new File(commands[1], long.Parse(commands[0])));
                    break;
            }
        }

        // _root.Print();
    }

    private Directory ChangeDirectory(string target) => target switch
    {
        "/" => _root,
        ".." => _root.GetParentOf(_currentDirectory), // parent
        { } => _currentDirectory.GetSubDirectory(target), // child
        _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
    };

    protected override long Part1()
    {
        var smallDirectories = new List<Directory>();
        _root.FindAllUnder100000(ref smallDirectories);
        return smallDirectories.Sum(d => d.GetSize());
    }

    protected override long Part2()
    {
        var minimum = _root.GetSize() - 40000000; // max allowed space
        long smallestAboveMinimum = 70000000; // 70000000 == max disc space

        _root.FindSmallestAboveMinSize(ref smallestAboveMinimum, minimum);
        return smallestAboveMinimum;
    }
}