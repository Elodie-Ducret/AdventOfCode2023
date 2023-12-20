using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day20;

namespace AdventOfCode2023.Problems.Day20;

public class Day20Part1Problem(string name, string path) : Problem<Modules, long>(name, path)
{
    public static void Run()
    {
        var dayProblem = new Day20Part1Problem("Day 20 Part 1", "Day20/Day20.txt");
        var response = dayProblem.SolveProblem();
    }

    public static void RunTest1()
    {
        var dayProblem = new Day20Part1Problem("Day 20 Part 1 Test1", "Day20/Day20_Part1_Test1.txt");
        var response = dayProblem.SolveProblem();
    }

    public static void RunTest2()
    {
        var dayProblem = new Day20Part1Problem("Day 20 Part 1 Test2", "Day20/Day20_Part1_Test2.txt");
        var response = dayProblem.SolveProblem();
    }

    protected override Modules Convert(IEnumerable<string> input)
    {
        var dic = new Dictionary<string, BaseModule>();
        foreach (var line in input)
        {
            var lineSplit = line.Split(" -> "); 
            var name = string.Join(string.Empty, lineSplit[0].Skip(1)); 
            switch (lineSplit[0][0])
            {
                case 'b':
                    dic.Add("broadcaster", new Broadcaster("broadcaster", lineSplit[1].Split(", ").ToList()) );
                    break;
                case '%':
                    dic.Add(name, new FlipFlop(name, lineSplit[1].Split(", ").ToList()) );
                    break;
                case '&':    
                    dic.Add(string.Join(string.Empty, lineSplit[0].Skip(1)), new Conjunction(name, lineSplit[1].Split(", ").ToList()) );
                    break;
                default: throw new NotImplementedException();
            }
        }

        foreach (var element in dic)
        {
            element.Value.ComputeModules(dic);
        }

        return new Modules(dic); 
    }

    protected override long Solve(Modules input)
    {
        return input.AffectAllModules();
    }
}