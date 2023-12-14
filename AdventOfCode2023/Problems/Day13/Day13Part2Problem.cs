using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day13;

namespace AdventOfCode2023.Problems.Day13;

public class Day13Part2Problem(string name, string path) : Problem<List<Pattern>, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day13Part2Problem("Day 13 Part 2", "Day13/Day13.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day13Part2Problem("Day 13 Part 2 Test1", "Day13/Day13_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }
    
    protected override List<Pattern> Convert(IEnumerable<string> input)
    {
        var fullInput = string.Join('\n', input);

        var splitInput = fullInput.Split("\n\n");

        var patterns = new List<Pattern>();
        foreach (var singlePattern in splitInput)
        {
            var pattern = singlePattern.Split('\n');

            var columnCount = pattern.First().Length;
            var rowCount = pattern.Length;
            var list = new bool[rowCount, columnCount];

            var count = 0;
            foreach (var line in pattern)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    list[count, i] = line[i] == '#';
                }

                count++;
            }

            patterns.Add(new Pattern(list, rowCount, columnCount));
        }


        return patterns;
    }

    protected override long Solve(List<Pattern> input)
    {
        long sum = 0;
        foreach (var pattern in input)
        {
            var rowPossibility = (-1, -1);
            for (int row = 0; row < pattern.RowCount; row++)
            {
                var possibility = pattern.GetMirrorRowPossibility(row);
                if (possibility == null) continue;
                rowPossibility = possibility.Value;
                sum += 100 * (row + 1);
                break;
            }

            if (rowPossibility != (-1, -1)) continue;

            for (int column = 0; column < pattern.ColumnCount; column++)
            {
                var possibility = pattern.GetMirrorColumnPossibility(column);
                if (possibility == null) continue;
                sum += (column + 1);
                break;
            }
        }

        return sum;
    }
}