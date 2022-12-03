using AoC;

Console.WriteLine("Hello, AOC 2022!");

Dictionary<int, IDay> days = new()
{
    [1] = new Day1("Calorie Counting", 71471, 211189),
    [2] = new Day2("Rock Paper Scissors", 12772, 11618),
    [3] = new Day3("Rucksack Reorganization", 7821, 2752),
};

AdventOfCode.Run(days[DateTime.Today.Day]);

AdventOfCode.Run(days);