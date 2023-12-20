namespace AdventOfCode2023.Schema.Day20;

public record Conjunction(string Name, List<string> OutputNames) : BaseModule(Name, OutputNames)
{
    public Dictionary<string, bool> Memory;

    public override void ComputeModules(Dictionary<string, BaseModule> dictionary)
    {
        base.ComputeModules(dictionary);
        Memory = Input.ToDictionary(x => x.Name, _ => false);
    }

    // Value &
    public override bool? OnPulse(BaseModule inputModule, bool pulse)
    {
        Memory[inputModule.Name] = pulse;
        var response = false;
        foreach (var module in Memory)
        {
            if (!module.Value) response = true;
        }

        return response;
    }
}