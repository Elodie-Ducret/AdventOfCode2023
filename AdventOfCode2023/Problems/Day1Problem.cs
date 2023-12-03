﻿using AdventOfCode2023.Common;

namespace AdventOfCode2023.Problems;

public class Day1Problem(string name, string path) : Problem<List<Day1Problem.Calibration>, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day1Problem("Day 1 Part 1", "Day1/Day_1.txt");
        var response = day1Problem.SolveProblem(); 
    }

    public static void RunTest()
    {
        var day1Problem = new Day1Problem("Day 1 Part 1 Test", "Day1/Day_1_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override List<Calibration> Convert(IEnumerable<string> input)
    {
        var line = 0;
        var calibrationList = new List<Calibration>(); 
        foreach (var calibrationInput in input)
        {
            var chars = calibrationInput.ToCharArray();
            var intList = (from ch in chars where char.IsDigit(ch) select int.Parse(ch.ToString())).ToList();
            calibrationList.Add(new Calibration(){Line = line, Numbers = intList});
            line++; 
        }

        return calibrationList;
    }

    protected override int Solve(List<Calibration> input)
    {
        return input.Sum(calibration => int.Parse(string.Concat(calibration.Numbers.First().ToString() + calibration.Numbers.Last().ToString())));
    }
    
    
    public class Calibration
    {
        public int Line;
        public List<int> Numbers;
    }
}