using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day11;

namespace AdventOfCode2023.Problems.Day11;

public class Day11Part2Problem(string name, string path) : Problem<Universe, long>(name, path)
{
    public const long Multiplier = 1000000 -1;

    public static void Run()
    {
        var day1Problem = new Day11Part2Problem("Day 11 Part 2", "Day11/Day11.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day11Part2Problem("Day 11 Part 2 Test1", "Day11/Day11_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override Universe Convert(IEnumerable<string> input)
    {
        var inputArray = input.ToArray();
        var rowCount = inputArray.Length;
        var columnCount = inputArray.First().Length;
        var baseGalaxy = new char[rowCount, columnCount];


        var rowDuplicated = new HashSet<int>();

        var count = 0;
        foreach (var line in inputArray)
        {
            for (int i = 0; i < columnCount; i++)
            {
                baseGalaxy[count, i] = line[i];
            }

            if (!line.Contains('#'))
            {
                rowDuplicated.Add(count);
            }

            count++;
        }

        var galaxyPositions = new List<GalaxyPosition>();
        var columnDuplicated = new HashSet<int>();

        for (int column = 0; column < columnCount; column++)
        {
            var isGalaxyEmpty = true;
            for (int row = 0; row < rowCount; row++)
            {
                if (baseGalaxy[row, column] == '#')
                {
                    galaxyPositions.Add(new GalaxyPosition(row, column));
                    isGalaxyEmpty = false;
                }
            }

            if (isGalaxyEmpty)
            {
                columnDuplicated.Add(column);
            }
        }

        return new Universe(baseGalaxy, rowDuplicated, columnDuplicated, galaxyPositions);
    }

    protected override long Solve(Universe input)
    {
        long sum = 0;
        for (int i = 0; i < input.Galaxies.Count; i++)
        {
            var start = input.Galaxies[i] + new GalaxyPosition(
                input.RowDuplicated.Count(x => x <= input.Galaxies[i].Row) * Multiplier,
                input.ColumnDuplicated.Count(x => x <= input.Galaxies[i].Column) * Multiplier);
            for (int j = i + 1; j < input.Galaxies.Count; j++)
            {
                var baseValue = input.Galaxies[j];
                var e = new GalaxyPosition(input.RowDuplicated.Count(x => x <= baseValue.Row) * Multiplier,
                    input.ColumnDuplicated.Count(x => x <= baseValue.Column) * Multiplier);
                var endGalaxy = baseValue + e;

                sum += Math.Abs(endGalaxy.Column - start.Column) + Math.Abs(endGalaxy.Row - start.Row);
            }
        }

        return sum;
    }
}