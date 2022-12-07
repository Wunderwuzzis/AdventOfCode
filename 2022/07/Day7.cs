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
    }

    protected override long Part1()
    {
        foreach (var line in Data)
        {
            var commands = line.Split(' ');
            switch (commands[0])
            {
                case "$" when commands[1] == "cd":
                    _currentDirectory = ChangeDirectory(commands[2]);
                    Console.WriteLine(_currentDirectory.Id);
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

        _root.Print();

        var smallDirectories = new List<Directory>();
        _root.FindAllUnder100000(ref smallDirectories);
        return smallDirectories.Sum(d => d.GetSize());
    }

    private Directory ChangeDirectory(string target) => target switch
    {
        "/" => _root,
        ".." => _root.GetParentOf(_currentDirectory), // parent
        { } => _currentDirectory.GetSubDirectory(target), // child
        _ => throw new ArgumentOutOfRangeException(nameof(target), target, null)
    };

    protected override long Part2()
    {
        return 0;
    }
}