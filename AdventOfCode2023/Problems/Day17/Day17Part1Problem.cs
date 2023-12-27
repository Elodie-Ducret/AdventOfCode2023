using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day17;

namespace AdventOfCode2023.Problems.Day17;

public class Day17Part1Problem(string name, string path) : Problem<Map, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day17Part1Problem("Day 17 Part 1", "Day17/Day17.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day17Part1Problem("Day 17 Part 1 Test1", "Day17/Day17_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }


    protected override Map Convert(IEnumerable<string> input)
    {
        var fullInput = string.Join('\n', input);
        var lines = fullInput.Split('\n');

        var columnCount = lines.First().Length;
        var rowCount = lines.Length;
        var cities = new int[rowCount, columnCount];

        var count = 0;
        foreach (var line in lines)
        {
            for (var i = 0; i < columnCount; i++)
            {
                cities[count, i] = int.Parse(line[i].ToString());
            }

            count++;
        }

        return new Map(cities, rowCount, columnCount);
    }

    protected override long Solve(Map input)
    {
        return input.DijkstraAlgorithm();
    }
}