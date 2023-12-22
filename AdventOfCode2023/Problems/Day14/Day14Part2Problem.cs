using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day14;

namespace AdventOfCode2023.Problems.Day14;

public class Day14Part2Problem(string name, string path) : Problem<Platform, long>(name, path)
{
    public static void Run()
    {
        var dayProblem = new Day14Part2Problem("Day 14 Part 2", "Day14/Day14.txt");
        var response = dayProblem.SolveProblem();
    }

    public static void RunTest1()
    {
        var dayProblem = new Day14Part2Problem("Day 14 Part 2 Test1", "Day14/Day14_Part1_Test1.txt");
        var response = dayProblem.SolveProblem();
    }

    private const int CycleCount = 1000000000;

    protected override Platform Convert(IEnumerable<string> input)
    {
        var fullInput = string.Join('\n', input);

        var platform = fullInput.Split('\n');

        var columnCount = platform.First().Length;
        var rowCount = platform.Length;
        var list = new char[rowCount, columnCount];

        var count = 0;
        foreach (var line in platform)
        {
            for (int i = 0; i < columnCount; i++)
            {
                list[count, i] = line[i];
            }

            count++;
        }

        return new Platform(list, rowCount, columnCount);
    }

    protected override long Solve(Platform input)
    {
        var cycles = new List<Platform>();
        var currentPlatform = input;
        var count = 0; 
        do
        {
            cycles.Add(currentPlatform);
            currentPlatform = currentPlatform.BuildNewPlatformAfterCycle();
            count++; 

        } while (cycles.Count(x => x.CompareTo(currentPlatform)) != 2);
        
        var beginOfCycle = cycles.IndexOf(cycles.First(x => x.CompareTo(currentPlatform)));
        var cycleSize = count - beginOfCycle;


        var tt = (CycleCount - beginOfCycle) % cycleSize;

        var lastPlatform = cycles[beginOfCycle + tt]; 
        lastPlatform.DisplayPlatform();
        
        return lastPlatform.GetCountPart2();
    }
}