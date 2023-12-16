using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day10;

namespace AdventOfCode2023.Problems.Day10;

public class Day10Part2Problem(string name, string path) : Problem<Grid, int>(name, path)
{
    public static void Run()
    {
        var dayProblem = new Day10Part2Problem("Day 10 Part 2", "Day10/Day10.txt");
        var response = dayProblem.SolveProblem();
    }

    public static void RunTest1()
    {
        var dayProblem = new Day10Part2Problem("Day 10 Part 2 Test1", "Day10/Day10_Part2_Test1.txt");
        var response = dayProblem.SolveProblem();
    }

    public static void RunTest2()
    {
        var dayProblem = new Day10Part2Problem("Day 10 Part 2 Test2", "Day10/Day10_Part2_Test2.txt");
        var response = dayProblem.SolveProblem();
    }

    public static void RunTest3()
    {
        var dayProblem = new Day10Part2Problem("Day 10 Part 2 Test3", "Day10/Day10_Part2_Test3.txt");
        var response = dayProblem.SolveProblem();
    }

    public static void RunTest4()
    {
        var dayProblem = new Day10Part2Problem("Day 10 Part 2 Test4", "Day10/Day10_Part2_Test4.txt");
        var response = dayProblem.SolveProblem();
    }

    protected override Grid Convert(IEnumerable<string> input)
    {
        var inputArray = input.ToArray();
        var rowCount = inputArray.Length;
        var columnCount = inputArray.First().Length;
        var startingPosition = new Position(-1, -1);
        var titles = new char[rowCount, columnCount];

        var count = 0;
        foreach (var line in inputArray)
        {
            for (int i = 0; i < columnCount; i++)
            {
                titles[count, i] = line[i];
                if (line[i] == 'S') startingPosition = new Position(count, i);
            }

            count++;
        }

        return new Grid(titles, rowCount, columnCount, startingPosition);
    }

    protected override int Solve(Grid input)
    {
        var potentialValues = GetOnlyStartingValidPositions(input);
        var validPath = new List<Position>();
        foreach (var position in potentialValues)
        {
            var path = new List<Position> { input.StartingPosition };
            var oldPosition = input.StartingPosition;
            Position? actualPosition = position;
            do
            {
                path.Add(actualPosition);
                (oldPosition, actualPosition) = (actualPosition,
                    input.GetNewValidPositions(actualPosition).FirstOrDefault(c => c != oldPosition));
            } while (actualPosition != null && actualPosition != input.StartingPosition);


            if (actualPosition != input.StartingPosition) continue;
            validPath = path;
            break;
        }

        PrintPath(validPath, input);

        return 1;
    }

    private List<Position> GetOnlyStartingValidPositions(Grid input)
    {
        var potentialPositions = input.GetNewValidPositions(input.StartingPosition);
        var viablePositions = new List<Position>();
        foreach (var pos in potentialPositions)
        {
            var newPositions = input.GetNewValidPositions(pos);
            if (newPositions.Contains(input.StartingPosition)) viablePositions.Add(pos);
        }

        return viablePositions;
    }

    private void PrintPath(List<Position> positions, Grid input)
    {
        for (int row = 0; row < input.RowCount; row++)
        {
            for (int column = 0; column < input.ColumnCount; column++)
            {
                if (positions.Contains(new Position(row, column)))
                {
                    Console.Write(DebugChars[input.Tiles[row, column]]);
                }
                else
                {
                    Console.Write(".");
                }
            }

            Console.WriteLine();
        }
    }

    private static readonly Dictionary<char, char> DebugChars = new()
    {
        ['.'] = '.',
        ['S'] = 'S',
        ['|'] = '║',
        ['L'] = '╚',
        ['J'] = '╝',
        ['-'] = '═',
        ['F'] = '╔',
        ['7'] = '╗',
    };
}