using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day12;

namespace AdventOfCode2023.Problems.Day12;

public class Day12Part2Problem(string name, string path) : Problem<List<SpringsRecord>, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day12Part2Problem("Day 12 Part 2", "Day12/Day12.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day12Part2Problem("Day 12 Part 2 Test1", "Day12/Day12_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override List<SpringsRecord> Convert(IEnumerable<string> input)
    {
        var list = new List<SpringsRecord>();
        foreach (var row in input)
        {
            var rowSplit = row.Split(' ');
            var baseSprings = rowSplit[0];
            var baseConditions = rowSplit[1].Split(',').Select(int.Parse).ToList();
            
            var springs = new List<char>();
            var conditions = new List<int>();
            for (var i = 0; i < 5; i++) //5 copies
            {
                springs.AddRange(baseSprings);
                if (i < 4) springs.Add('?');
                conditions.AddRange(baseConditions);
            }
            list.Add(new SpringsRecord([..springs], conditions));
        }

        return list;
    }

    protected override long Solve(List<SpringsRecord> input)
    {
        return input.Sum(x => x.GetArrangementValidCount());
    }
}