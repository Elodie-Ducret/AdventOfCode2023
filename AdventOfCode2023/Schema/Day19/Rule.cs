namespace AdventOfCode2023.Schema.Day19;

public record Rule(Compare? Compare, string NextWorkflow)
{
    public string? Apply(Rating rating)
    {
        if (Compare == null) return NextWorkflow;
        return Compare.Apply(rating) ? NextWorkflow : null;
    }
}