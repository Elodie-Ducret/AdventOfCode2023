namespace AdventOfCode2023.Schema.Day19.Part1;

public record Workflow(string Name, List<Rule> Rules)
{
    public string Apply(Rating rating)
    {
        foreach (var rule in Rules)
        {
            if (rule.Compare == null) return rule.NextWorkflow;
            var nextWorkflow = rule.Apply(rating);
            if (nextWorkflow != null) return nextWorkflow;
        }

        return Rules.Last().NextWorkflow;
    }
}