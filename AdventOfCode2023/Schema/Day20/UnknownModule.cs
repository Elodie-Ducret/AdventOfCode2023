namespace AdventOfCode2023.Schema.Day20;

public record UnknownModule(string Name, List<string> OutputNames) : BaseModule(Name, OutputNames)
{
    public override bool? OnPulse(BaseModule inputModule, bool pulse)
    {
        return pulse;
    }
}