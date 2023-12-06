using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day6;

namespace AdventOfCode2023.Problems.Day6;

public class Day6Part2Problem(string name, string path) : Problem<Race, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day6Part2Problem("Day 6 Part 2", "Day6/Day6.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day6Part2Problem("Day 6 Part 2 Test", "Day6/Day6_Part1_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override Race Convert(IEnumerable<string> input)
    {
        var time = new string(input.First().Split(": ")[1].Where(x=> x != ' ').ToArray());
        var distance = new string(input.Last().Split(": ")[1].Where(x=> x != ' ').ToArray());

        var race = new Race(long.Parse(time), long.Parse(distance));
        return race;
    }

    protected override long Solve(Race input)
    {
        var response = GetWinCount(input);
        return response;
    }


    private long GetWinCount(Race race)
    {
        var sum = 0;
        for (var i = 0; i < race.Time; i++)
        {
            if (race.IsWin(i)) sum++;
        }

        return sum;
    }
}