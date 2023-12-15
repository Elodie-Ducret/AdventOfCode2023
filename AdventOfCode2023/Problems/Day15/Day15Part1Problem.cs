using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day15;

namespace AdventOfCode2023.Problems.Day15;

public class Day15Part1Problem(string name, string path) : Problem<Book, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day15Part1Problem("Day 15 Part 1", "Day15/Day15.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day15Part1Problem("Day 15 Part 1 Test1", "Day15/Day15_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }
    
    public static void RunTest2()
    {
        var day1Problem = new Day15Part1Problem("Day 15 Part 1 Test2", "Day15/Day15_Part1_Test2.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override Book Convert(IEnumerable<string> input)
    {
        var list = new List<Sequence>();
        foreach (var line in input)
        {
            list.AddRange(line.Split(",").Select(x => new Sequence(x)).ToList());
        }

        return new Book(list);
    }

    protected override long Solve(Book input)
    {
        return input.GetBookValue();
    }
}