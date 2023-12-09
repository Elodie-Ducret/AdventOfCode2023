using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day9;

namespace AdventOfCode2023.Problems.Day9;

public class Day9Part1Problem(string name, string path) : Problem<Oasis, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day9Part1Problem("Day 9 Part 1", "Day9/Day9.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day9Part1Problem("Day 9 Part 1 Test", "Day9/Day9_Part1_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override Oasis Convert(IEnumerable<string> input)
    {
        var histories = new List<History>();
        foreach (var history in input)
        {
            histories.Add(new History(history.Split(' ').Select(int.Parse).ToList()));
        }

        var oasis = new Oasis(histories);
        return oasis;
    }

    protected override int Solve(Oasis input)
    {
        return input.Histories.Sum(x => x.GetNext()); 
    }
}