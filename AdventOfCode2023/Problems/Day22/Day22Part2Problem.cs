using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day22;

namespace AdventOfCode2023.Problems.Day22;

public class Day22Part2Problem(string name, string path) : Problem<Snapshot, long>(name, path)
{
    public static void Run()
    {
        var dayProblem = new Day22Part2Problem("Day 22 Part 2", "Day22/Day22.txt");
        var response = dayProblem.SolveProblem();
    }

    public static void RunTest1()
    {
        var dayProblem = new Day22Part2Problem("Day 22 Part 2 Test1", "Day22/Day22_Part1_Test1.txt");
        var response = dayProblem.SolveProblem();
    }

    protected override Snapshot Convert(IEnumerable<string> input)
    {
        var inputList = new HashSet<Brick>();
        var nameChar = 'A';
        foreach (var line in input)
        {
            var positions = line.Split('~');
            var startPositionList = positions[0].Split(',').Select(int.Parse).ToList();
            var startPosition = new Position3(startPositionList[0], startPositionList[1], startPositionList[2]);
            var endPositionList = positions[1].Split(',').Select(int.Parse).ToList();
            var endPosition = new Position3(endPositionList[0], endPositionList[1], endPositionList[2]);
            var brick = new Brick(nameChar, startPosition, endPosition);
            inputList.Add(brick);
            nameChar++;
        }

        var maxPositions = inputList.Select(x => x.GetMaxPosition()).ToHashSet();
        var rowCount = maxPositions.Select(x => x.Row).Max() + 1;
        var columnCount = maxPositions.Select(x => x.Column).Max() + 1;
        var elevationCount = maxPositions.Select(x => x.Elevation).Max() + 1;


        var brickSnapshot = new Brick?[rowCount, columnCount, elevationCount];

        foreach (var brick in inputList)
        {
            var positions = brick.GetAllPositions();
            foreach (var position in positions)
            {
                brickSnapshot[position.Row, position.Column, position.Elevation] = brick;
            }
        }

        return new Snapshot(brickSnapshot, inputList, rowCount, columnCount, elevationCount);
    }

    protected override long Solve(Snapshot input)
    {
        return input.GetOtherBricksFallCount();
    }
}