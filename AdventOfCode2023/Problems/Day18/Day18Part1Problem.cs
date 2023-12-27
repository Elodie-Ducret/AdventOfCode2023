using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day18;

namespace AdventOfCode2023.Problems.Day18;

public class Day18Part1Problem(string name, string path) : Problem<Plan, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day18Part1Problem("Day 18 Part 1", "Day18/Day18.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day18Part1Problem("Day 18 Part 1 Test1", "Day18/Day18_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override Plan Convert(IEnumerable<string> input)
    {
        var list = new List<DigPlan>();
        foreach (var line in input)
        {
            var lineSplit = line.Split(' ');
            var color = string.Join('\0', lineSplit.Last().Skip(1).SkipLast(1));
            var digPlan = new DigPlan(DirectionPosition.GetDirectionRLUD(lineSplit[0].First()), int.Parse(lineSplit[1]),
                color);
            list.Add(digPlan);
        }

        return new Plan(list);
    }

    protected override long Solve(Plan input)
    {
        return input.GetSum();
    }
}