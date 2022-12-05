﻿using AoC;

Console.WriteLine("Hello, AOC 2022!");

Dictionary<int, IDay> days = new()
{
    [1] = new Day1("Calorie Counting", 71471, 211189),
    [2] = new Day2("Rock Paper Scissors", 12772, 11618),
    [3] = new Day3Short("Rucksack Reorganization", 7821, 2752),
    [4] = new Day4("Camp Cleanup", 450, 837),
    [5] = new Day5("Supply Stacks", "VWLCWGSDQ", "TCGLQSLPW"),
};

AdventOfCode.Run(days[DateTime.Today.Day]);

AdventOfCode.Run(days);