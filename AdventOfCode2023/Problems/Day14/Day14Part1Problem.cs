using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day14;

namespace AdventOfCode2023.Problems.Day14;

public class Day14Part1Problem(string name, string path) : Problem<Platform, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day14Part1Problem("Day 14 Part 1", "Day14/Day14.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day14Part1Problem("Day 14 Part 1 Test1", "Day14/Day14_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override Platform Convert(IEnumerable<string> input)
    {
        var fullInput = string.Join('\n', input);

        var platform = fullInput.Split('\n');

        var columnCount = platform.First().Length;
        var rowCount = platform.Length;
        var list = new char[rowCount, columnCount];

        var count = 0;
        foreach (var line in platform)
        {
            for (int i = 0; i < columnCount; i++)
            {
                list[count, i] = line[i];
            }

            count++;
        }

        return new Platform(list, rowCount, columnCount);
    }

    protected override long Solve(Platform input)
    {
        return input.GetCountPart1();
    }
}