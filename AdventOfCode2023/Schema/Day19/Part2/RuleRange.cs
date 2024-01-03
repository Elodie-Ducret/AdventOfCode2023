namespace AdventOfCode2023.Schema.Day19.Part2;

public record RuleRange(CompareRange? Compare, string NextWorkflow)
{
    public List<(RatingRange, string?)> Apply(RatingRange ratingRange)
    {
        return Compare == null
            ? [(ratingRange, NextWorkflow)]
            : Compare.Apply(ratingRange).Select(x => (x.Item1, x.Item2 ? NextWorkflow : null)).ToList();
    }
}