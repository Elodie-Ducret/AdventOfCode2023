using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day23;

namespace AdventOfCode2023.Problems.Day23;

public class Day23Part1Problem(string name, string path) : Problem<Graph, long>(name, path)
{
    public static void Run()
    {
        var problem = new Day23Part1Problem("Day 23 Part 1", "Day23/Day23.txt");
        var response = problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var problem = new Day23Part1Problem("Day 23 Part 1 Test1", "Day23/Day23_Part1_Test1.txt");
        var response = problem.SolveProblem();
    }

    protected override Graph Convert(IEnumerable<string> input)
    {
        var fullInput = string.Join('\n', input);
        var lines = fullInput.Split('\n');

        var columnCount = lines.First().Length;
        var rowCount = lines.Length;
        var titles = new char[rowCount, columnCount];
        var start = new Position();
        var end = new Position();

        var count = 0;
        foreach (var line in lines)
        {
            if (count == 0) start = new Position(0, line.IndexOf('.'));
            if (count == columnCount - 1) end = new Position(columnCount - 1, line.IndexOf('.'));

            for (var i = 0; i < columnCount; i++)
            {
                titles[count, i] = line[i];
            }

            count++;
        }

        return new Graph(titles, rowCount, columnCount, start, end);
    }

    protected override long Solve(Graph input)
    {
        return input.GetMaxCount();
    }
}