using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day19.Part2;

namespace AdventOfCode2023.Problems.Day19;

public class Day19Part2Problem(string name, string path) : Problem<SystemWorkflowRange, long>(name, path)
{
    public static void Run()
    {
        var dayProblem = new Day19Part2Problem("Day 19 Part 2", "Day19/Day19.txt");
        var response = dayProblem.SolveProblem();
    }

    public static void RunTest1()
    {
        var dayProblem = new Day19Part2Problem("Day 19 Part 2 Test1", "Day19/Day19_Part1_Test1.txt");
        var response = dayProblem.SolveProblem();
    }

    protected override SystemWorkflowRange Convert(IEnumerable<string> input)
    {
        var inputs = string.Join('\n', input).Split("\n\n");

        var inputWorkflows = inputs[0];
        var workflows = new HashSet<WorkflowRange>();
        foreach (var line in inputWorkflows.Split('\n'))
        {
            var lineSplit = line.Split('{');
            var rules = lineSplit[1].Split('}')[0].Split(',');
            var rulesSplit = new List<RuleRange>();
            foreach (var rule in rules)
            {
                var ruleSplit = rule.Split(':');
                var comparePart = ruleSplit[0];
                if (ruleSplit.Length == 2)
                {
                    rulesSplit.Add(new RuleRange(new CompareRange(
                            comparePart[0],
                            comparePart[1],
                            int.Parse(new string(comparePart.Skip(2).ToArray()))),
                        ruleSplit[1]));
                }
                else
                {
                    rulesSplit.Add(new RuleRange(null, ruleSplit[0]));
                }
            }


            var workflow = new WorkflowRange(lineSplit[0], rulesSplit);
            workflows.Add(workflow);
        }

        return new SystemWorkflowRange(workflows);
    }

    protected override long Solve(SystemWorkflowRange input)
    {
        return input.GetCombinationAcceptedCount();
    }
}