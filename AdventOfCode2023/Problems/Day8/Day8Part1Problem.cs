using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day8;

namespace AdventOfCode2023.Problems.Day8;

public class Day8Part1Problem(string name, string path) : Problem<Network, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day8Part1Problem("Day 8 Part 1", "Day8/Day8.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest1()
    {
        var day1Problem = new Day8Part1Problem("Day 8 Part 1 Test", "Day8/Day8_Part1_Test1.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest2()
    {
        var day1Problem = new Day8Part1Problem("Day 8 Part 1 Test", "Day8/Day8_Part1_Test2.txt");
        var response = day1Problem.SolveProblem();
    }


    protected override Network Convert(IEnumerable<string> input)
    {
        var inputArray = input.ToArray();

        var l = inputArray[0].ToCharArray().Select(x => x == 'L' ? 0 : 1).ToList();

        var dic = new Dictionary<string, List<string>>();
        for (var i = 2; i < inputArray.Length; i++)
        {
            var keyValues = inputArray[i].Split(" = ");
            var key = keyValues[0];

            var valuesCleaned = keyValues[1].Substring(1, keyValues[1].Length - 2);
            var values = valuesCleaned.Split(", ");


            dic.Add(key, new List<string> { values[0], values[1] });
        }

        return new Network(l, dic);
    }

    protected override int Solve(Network input)
    {
        var count = 0;
        var current = input.Nodes["AAA"];
        while (true)
        {
            foreach (var navigationElement in input.Navigation)
            {
                var next = current[navigationElement];
                current = input.Nodes[next];
                count++;
                if (next == "ZZZ") return count;
            }
        }
    }
}