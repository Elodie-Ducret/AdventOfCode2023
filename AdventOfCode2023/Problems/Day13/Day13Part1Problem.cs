using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day13;

namespace AdventOfCode2023.Problems.Day13;

public class Day13Part1Problem(string name, string path) : Problem<List<Pattern>, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day13Part1Problem("Day 13 Part 1", "Day13/Day13.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day13Part1Problem("Day 13 Part 1 Test1", "Day13/Day13_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }
    
    public static void RunTest2()
    {
        var day1Problem = new Day13Part1Problem("Day 13 Part 1 Test2", "Day13/Day13_Part1_Test2.txt");
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
        var sum = 0;
        foreach (var pattern in input)
        {
            Console.WriteLine("--------------------------");
            pattern.PrintPattern();
            var sumNumberOfRowAboveHorizontal = 0;
            for (int row = 0; row < pattern.RowCount; row++)
            {
                sumNumberOfRowAboveHorizontal += pattern.GetMirrorRowCount(row);
            }


            var sumNumberOfColumnLeftVertical = 0;
            for (int column = 0; column < pattern.ColumnCount; column++)
            {
                sumNumberOfColumnLeftVertical += pattern.GetMirrorColumnCount(column);
            }

            sum += sumNumberOfColumnLeftVertical + 100 * sumNumberOfRowAboveHorizontal;
            Console.WriteLine("--------------------------");
        }

        return sum;
    }
}