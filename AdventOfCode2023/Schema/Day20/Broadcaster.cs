namespace AdventOfCode2023.Schema.Day20;

public record Broadcaster(string Name, List<string> OutputNames) : BaseModule(Name, OutputNames)
{
    public override bool? OnPulse(BaseModule inputModule, bool pulse)
    {
        return pulse;
    }
}