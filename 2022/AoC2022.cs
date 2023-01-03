using AoC;

Console.WriteLine("Hello, AOC 2022!");

Dictionary<int, IDay> days = new()
{
    [1] = new Day1("Calorie Counting", 71471, 211189),
    [2] = new Day2("Rock Paper Scissors", 12772, 11618),
    [3] = new Day3("Rucksack Reorganization", 7821, 2752),
    [4] = new Day4("Camp Cleanup", 450, 837),
    [5] = new Day5("Supply Stacks", "VWLCWGSDQ", "TCGLQSLPW"),
    [6] = new Day6("Tuning Trouble", 1896, 3452),
    [7] = new Day7("No Space Left On Device", 1844187, 4978279),
    [8] = new Day8("Treetop Tree House", 1538, 496125),
    [9] = new Day9("Rope Bridge", 5960, 2327),
    [10] = new Day10("Cathode-Ray Tube", 16880),
    [11] = new Day11("Monkey in the Middle", 54036, 13237873355),
    [12] = new Day12("Hill Climbing Algorithm", 462, 451),
    [13] = new Day13("Distress Signal", 4643, 21614),
    [14] = new Day14("Regolith Reservoir", 838, 27539),
    [15] = new Day15(""),
};

AdventOfCode.Run(days[15]); // DateTime.Today.Day

// AdventOfCode.Run(days);