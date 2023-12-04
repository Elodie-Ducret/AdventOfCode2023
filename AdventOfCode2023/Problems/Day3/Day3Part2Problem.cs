using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day3;

namespace AdventOfCode2023.Problems.Day3;

public class Day3Part2Problem(string name, string path) : Problem<MotorMatrix, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day3Part2Problem("Day 3 Part 1", "Day3/Day3.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day3Part2Problem("Day 3 Part 1 Test", "Day3/Day3_Part1_Test.txt");
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
        var links = new List<List<(int, int)>>();

        for (var i = 0; i < input.RowCount; i++)
        {
            for (var j = 0; j < input.ColumnCount; j++)
            {
                if (isValid[i, j]) continue;
                isValid[i, j] = false;
                var isGear = IsGear(input.Matrix[i, j]);
                if (!isGear) continue;
                var rep = IsEngineSchematicGear(input, i, j);
                if (rep.isValid)
                {
                    links.Add(rep.numbers);
                    foreach (var num in rep.numbers)
                    {
                        isValid[num.Item1, num.Item2] = true;
                    }
                }
            }
        }

        var listValues = new List<(List<(int, int)> positions, int value)>();
        for (var i = 0; i < input.RowCount; i++)
        {
            for (var j = 0; j < input.ColumnCount; j++)
            {
                var isInt = int.TryParse(input.Matrix[i, j].ToString(), out int intValue);
                if (!isInt) continue;


                var list = new List<(int, int)>();
                list.Add((i, j));


                var lastIndex = j;
                if (j + 1 < input.ColumnCount)
                {
                    for (int k = j + 1; k < input.ColumnCount; k++)
                    {
                        var isIntK = int.TryParse(input.Matrix[i, k].ToString(), out int intValueK);
                        if (isIntK)
                        {
                            list.Add((i, k));
                        }
                        else
                        {
                            lastIndex = k;
                            break;
                        }
                    }
                }

                if (list.Any(x => isValid[i, x.Item2]))
                {
                    var value = int.Parse(string.Concat(list.Select(c => input.Matrix[i, c.Item2].ToString())));
                    listValues.Add((list, value));
                }


                j = lastIndex;
            }
        }


        var valuesLink = new List<(int, int)>();


        foreach (var link in links)
        {
            var filteredListValues = listValues.Where(x => link.Any(y => x.positions.Contains(y))).ToList();
            if (filteredListValues.Count == 2)
            {
                sum += filteredListValues.First().value * filteredListValues.Last().value;
            }
        }
        
        return sum;
    }

    private (bool isValid, List<(int, int)> numbers) IsEngineSchematicGear(MotorMatrix input, int i, int j)
    {
        var count = 0;
        var numbers = new List<(int, int)>();
        if (i - 1 >= 0)
        {
            if (IsNumber(input.Matrix[i - 1, j]))
            {
                numbers.Add((i - 1, j));
                count++;
            }

            if (j - 1 >= 0)
            {
                if (IsNumber(input.Matrix[i - 1, j - 1]))
                {
                    numbers.Add((i - 1, j - 1));
                    count++;
                }
            }

            if (j + 1 < input.ColumnCount)
            {
                if (IsNumber(input.Matrix[i - 1, j + 1]))
                {
                    numbers.Add((i - 1, j + 1));
                    count++;
                }
            }
        }

        if (i + 1 < input.RowCount)
        {
            if (IsNumber(input.Matrix[i + 1, j]))
            {
                numbers.Add((i + 1, j));
                count++;
            }

            if (j - 1 >= 0)
            {
                if (IsNumber(input.Matrix[i + 1, j - 1]))
                {
                    numbers.Add((i + 1, j - 1));
                    count++;
                }
            }

            if (j + 1 < input.ColumnCount)
            {
                if (IsNumber(input.Matrix[i + 1, j + 1]))
                {
                    numbers.Add((i + 1, j + 1));
                    count++;
                }
            }
        }

        if (j - 1 >= 0)
        {
            if (IsNumber(input.Matrix[i, j - 1]))
            {
                numbers.Add((i, j - 1));
                count++;
            }
        }

        if (j + 1 < input.ColumnCount)
        {
            if (IsNumber(input.Matrix[i, j + 1]))
            {
                numbers.Add((i, j + 1));
                count++;
            }
        }

        return (count >= 2, numbers);
    }

    private bool IsNumber(char character)
    {
        var isInt = int.TryParse(character.ToString(), out int intValue);
        return isInt;
    }

    private bool IsGear(char character)
    {
        return character == '*';
    }
}