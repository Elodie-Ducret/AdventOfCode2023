using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day20;

namespace AdventOfCode2023.Problems.Day20;

public class Day20Part2Problem(string name, string path) : Problem<ModulesPart2, long>(name, path)
{
    public static void Run()
    {
        var dayProblem = new Day20Part2Problem("Day 20 Part 2", "Day20/Day20.txt");
        var response = dayProblem.SolveProblem();
    }

    protected override ModulesPart2 Convert(IEnumerable<string> input)
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

        return new ModulesPart2(dic); 
    }

    protected override long Solve(ModulesPart2 input)
    {
        
        
        return input.MultiplePressButton();
    }
}