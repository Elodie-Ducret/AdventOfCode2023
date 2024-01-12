using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day21;

namespace AdventOfCode2023.Problems.Day21;

public class Day21Part1Problem(string name, string path) : Problem<Garden, long>(name, path)
{
    public static void Run()
    {
        var problem = new Day21Part1Problem("Day 21 Part 1", "Day21/Day21.txt");
        var response = problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var problem = new Day21Part1Problem("Day 21 Part 1 Test1", "Day21/Day21_Part1_Test1.txt");
        var response = problem.SolveProblem();
    }

    protected override Garden Convert(IEnumerable<string> input)
    {
        var fullInput = string.Join('\n', input);
        var lines = fullInput.Split('\n');

        var columnCount = lines.First().Length;
        var rowCount = lines.Length;
        var tiles = new char[rowCount, columnCount];

        var count = 0;
        var start = new Position(0, 0);
        foreach (var line in lines)
        {
            for (var i = 0; i < columnCount; i++)
            {
                if (line[i] == 'S') start = new Position(count, i);
                tiles[count, i] = line[i];
            }

            count++;
        }

        return new Garden(tiles, start, rowCount, columnCount);
    }

    protected override long Solve(Garden input)
    {
        return input.GetAccessibleTilesCount();
    }
}