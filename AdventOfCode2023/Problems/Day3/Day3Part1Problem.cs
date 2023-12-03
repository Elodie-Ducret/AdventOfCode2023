using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day3;

namespace AdventOfCode2023.Problems.Day3;

public class Day3Part1Problem(string name, string path) : Problem<MotorMatrix, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day3Part1Problem("Day 3 Part 1", "Day3/Day3.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day3Part1Problem("Day 3 Part 1 Test", "Day3/Day3_Part1_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override MotorMatrix Convert(IEnumerable<string> input)
    {
        var rowCount = input.Count();
        var columnCount = input.First().Length;

        var matrix = new char[columnCount, rowCount];

        var count = 0;
        foreach (var line in input)
        {
            var lineSplit = line.ToCharArray();
            for (int i = 0; i < columnCount; i++)
            {
                matrix[count, i] = lineSplit[i];
            }

            count++;
        }

        return new MotorMatrix
        {
            RowCount = rowCount,
            ColumnCount = columnCount,
            Matrix = matrix
        };
    }

    protected override int Solve(MotorMatrix input)
    {
        var sum = 0;

        var isValid = new bool[input.RowCount, input.ColumnCount];

        for (var i = 0; i < input.RowCount; i++)
        {
            for (var j = 0; j < input.ColumnCount; j++)
            {
                isValid[i, j] = false;
                var isInt = int.TryParse(input.Matrix[i, j].ToString(), out int intValue);
                if (!isInt) continue;
                if (IsEngineSchematic(input, i, j)) isValid[i, j] = true;
            }
        }

        for (var i = 0; i < input.RowCount; i++)
        {
            for (var j = 0; j < input.ColumnCount; j++)
            {
                var isInt = int.TryParse(input.Matrix[i, j].ToString(), out int intValue);
                if (!isInt) continue;

                var list = new List<int>();
                list.Add(j);


                var lastIndex = j;
                if (j + 1 < input.ColumnCount)
                {
                    for (int k = j + 1; k < input.ColumnCount; k++)
                    {
                        var isIntK = int.TryParse(input.Matrix[i, k].ToString(), out int intValueK);
                        if (isIntK)
                        {
                            list.Add(k);
                        }
                        else
                        {
                            lastIndex = k;
                            break;
                        }
                    }
                }

                if (list.Any(x => isValid[i, x]))
                {
                    sum += int.Parse(string.Concat(list.Select(c => input.Matrix[i, c].ToString())));
                }

                j = lastIndex;
            }
        }


        return sum;
    }

    private bool IsEngineSchematic(MotorMatrix input, int i, int j)
    {
        if (i - 1 >= 0)
        {
            if (IsSymbol(input.Matrix[i - 1, j])) return true;

            if (j - 1 >= 0)
            {
                if (IsSymbol(input.Matrix[i - 1, j - 1])) return true;
            }

            if (j + 1 < input.ColumnCount)
            {
                if (IsSymbol(input.Matrix[i - 1, j + 1])) return true;
            }
        }

        if (i + 1 < input.RowCount)
        {
            if (IsSymbol(input.Matrix[i + 1, j])) return true;

            if (j - 1 >= 0)
            {
                if (IsSymbol(input.Matrix[i + 1, j - 1])) return true;
            }

            if (j + 1 < input.ColumnCount)
            {
                if (IsSymbol(input.Matrix[i + 1, j + 1])) return true;
            }
        }

        if (j - 1 >= 0)
        {
            if (IsSymbol(input.Matrix[i, j - 1])) return true;
        }

        if (j + 1 < input.ColumnCount)
        {
            if (IsSymbol(input.Matrix[i, j + 1])) return true;
        }

        return false;
    }

    private bool IsSymbol(char character)
    {
        var isInt = int.TryParse(character.ToString(), out int intValue);
        if (isInt) return false;
        return character != '.';
    }
}