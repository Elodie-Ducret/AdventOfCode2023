using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day6;

namespace AdventOfCode2023.Problems.Day6;

public class Day6Part1Problem(string name, string path) : Problem<List<Race>, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day6Part1Problem("Day 6 Part 1", "Day6/Day6.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day6Part1Problem("Day 6 Part 1 Test", "Day6/Day6_Part1_Test.txt");
        var response = day1Problem.SolveProblem();
    }


    protected override List<Race> Convert(IEnumerable<string> input)
    {
        var times = input.First();
        var timesSplit = times.Split(": ")[1].Split(" ").Where(x => x != string.Empty).ToList();
        var distances = input.Last();
        var distancesSplit = distances.Split(": ")[1].Split(" ").Where(x => x != string.Empty).ToList();


        var list = timesSplit.Select((t, i) => new Race(int.Parse(t), int.Parse(distancesSplit[i]))).ToList();
        return list;
    }

    protected override int Solve(List<Race> input)
    {
        var response = input.Select(GetWinCount).Aggregate(1, (x, y) => x * y);
        return response;
    }


    private int GetWinCount(Race race)
    {
        var sum = 0;
        for (var i = 0; i < race.Time; i++)
        {
            if (race.IsWin(i)) sum++; 
        }
        return sum; 
    }
}