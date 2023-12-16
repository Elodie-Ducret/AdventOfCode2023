using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day15;

namespace AdventOfCode2023.Problems.Day15;

public class Day15Part2Problem(string name, string path) : Problem<Boxes, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day15Part2Problem("Day 15 Part 2", "Day15/Day15.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day15Part2Problem("Day 15 Part 2 Test1", "Day15/Day15_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest2()
    {
        var day1Problem = new Day15Part2Problem("Day 15 Part 2 Test2", "Day15/Day15_Part1_Test2.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override Boxes Convert(IEnumerable<string> input)
    {
        var list = new List<SequencePart2>();
        foreach (var line in input)
        {
            foreach (var seq in line.Split(","))
            {
                var seqSplit = seq.Split('=');
                list.Add(seqSplit.Length == 2
                    ? new SequencePart2(new Sequence(seqSplit[0]), false, int.Parse(seqSplit[1]))
                    : new SequencePart2(new Sequence(seqSplit[0].Split('-')[0]), true, null));
            }
        }

        return new Boxes(list);
    }

    protected override long Solve(Boxes input)
    {
        return input.GetCount();
    }
}