using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day19;

namespace AdventOfCode2023.Problems.Day19;

public class Day19Part1Problem(string name, string path) : Problem<SystemWorkflow, long>(name, path)
{
    public static void Run()
    {
        var dayProblem = new Day19Part1Problem("Day 19 Part 1", "Day19/Day19.txt");
        var response = dayProblem.SolveProblem();
    }

    public static void RunTest1()
    {
        var dayProblem = new Day19Part1Problem("Day 19 Part 1 Test1", "Day19/Day19_Part1_Test1.txt");
        var response = dayProblem.SolveProblem();
    }

    protected override SystemWorkflow Convert(IEnumerable<string> input)
    {
        var inputs = string.Join('\n', input).Split("\n\n");

        var inputWorkflows = inputs[0];
        var workflows = new HashSet<Workflow>();
        foreach (var line in inputWorkflows.Split('\n'))
        {
            var lineSplit = line.Split('{');
            var rules = lineSplit[1].Split('}')[0].Split(',');
            var rulesSplit = new List<Rule>();
            foreach (var rule in rules)
            {
                var ruleSplit = rule.Split(':');
                var comparePart = ruleSplit[0];
                if (ruleSplit.Length == 2)
                {
                    rulesSplit.Add(new Rule(new Compare(
                            comparePart[0],
                            comparePart[1],
                            int.Parse(new string(comparePart.Skip(2).ToArray()))),
                        ruleSplit[1]));
                }
                else
                {
                    rulesSplit.Add(new Rule(null, ruleSplit[0]));
                }
            }


            var workflow = new Workflow(lineSplit[0], rulesSplit);
            workflows.Add(workflow);
        }


        var inputRatings = inputs[1];

        var ratings = new HashSet<Rating>();
        foreach (var line in inputRatings.Split('\n'))
        {
            var lineSplit = line.Split('{')[1].Split('}')[0];
            var valuesSplit = lineSplit.Split(',');
            var values = valuesSplit.Select(element => element.Split('='))
                .ToDictionary(elements => elements[0][0], elements => int.Parse(elements[1]));
            var rating = new Rating(values);
            ratings.Add(rating);
        }

        return new SystemWorkflow(workflows, ratings);
    }

    protected override long Solve(SystemWorkflow input)
    {
        return input.GetAcceptedCount();
    }
}