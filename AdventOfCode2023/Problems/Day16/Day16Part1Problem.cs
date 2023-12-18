using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day16;

namespace AdventOfCode2023.Problems.Day16;

public class Day16Part1Problem(string name, string path) : Problem<Grid, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day16Part1Problem("Day 16 Part 1", "Day16/Day16.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day16Part1Problem("Day 16 Part 1 Test1", "Day16/Day16_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override Grid Convert(IEnumerable<string> input)
    {
        var fullInput = string.Join('\n', input);
        var lines = fullInput.Split('\n');

        var columnCount = lines.First().Length;
        var rowCount = lines.Length;
        var titles = new char[rowCount, columnCount];

        var count = 0;
        foreach (var line in lines)
        {
            for (var i = 0; i < columnCount; i++)
            {
                titles[count, i] = line[i];
            }

            count++;
        }

        return new Grid(titles, rowCount, columnCount);
    }

    protected override long Solve(Grid input)
    {
        return input.GenerateEnergizedPositions(new Position(0, 0), Direction.East);
    }
}