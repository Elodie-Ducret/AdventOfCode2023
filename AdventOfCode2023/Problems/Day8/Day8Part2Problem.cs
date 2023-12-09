using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day8;

namespace AdventOfCode2023.Problems.Day8;

public class Day8Part2Problem(string name, string path) : Problem<Network, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day8Part2Problem("Day 8 Part 2", "Day8/Day8.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day8Part2Problem("Day 8 Part 2 Test", "Day8/Day8_Part2_Test.txt");
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

    protected override long Solve(Network input)
    {
        long count = 0;
        var starts = input.Nodes.Where(x => x.Key.ToCharArray().Last() == 'A').Select(x => x.Key).ToList();


        var system = new List<Equation>();

        foreach (var start in starts)
        {
            var positions = new List<Follower>();
            var index = 0;
            var current = start;
            var zIndex = -1;
            var follower = new Follower(0, current);
            while (!positions.Contains(follower))
            {
                if (current.EndsWith('Z')) zIndex = index;
                positions.Add(follower);
                current = input.Nodes[current][input.Navigation[index % input.Navigation.Count]];
                follower = new Follower(index % input.Navigation.Count, current);
                index++;
            }

            system.Add(new Equation(positions.Count - positions.IndexOf(follower), zIndex));
        }

        return Gcd(system.Select(x => x.Constant).ToArray()); 
    }


    static long Gcd(long n1, long n2)
    {
        return n2 == 0 ? n1 : Gcd(n2, n1 % n2);
    }

    private static long Gcd(long[] numbers)
    {
        return numbers.Aggregate((s, val) => s * val / Gcd(s, val));
    }
}