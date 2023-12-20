namespace AdventOfCode2023.Schema.Day20;

public record FlipFlop(string Name, List<string> OutputNames) : BaseModule(Name, OutputNames)
{
    public bool CurrentValue = false;

    // Value %
    public override bool? OnPulse(BaseModule inputModule, bool pulse)
    {
        if (pulse)
        {
            return null;
        }

        CurrentValue = !CurrentValue;
        return CurrentValue;
    }
}