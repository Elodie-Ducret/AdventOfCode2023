namespace AdventOfCode2023.Schema.Day20;

public abstract record BaseModule(string Name, List<string> OutputNames)
{
    protected readonly List<BaseModule> Input = new();
    public readonly List<BaseModule> Output = new();


    public abstract bool? OnPulse(BaseModule inputModule, bool pulse);


    public virtual void ComputeModules(Dictionary<string, BaseModule> dictionary)
    {
        foreach (var moduleName in OutputNames)
        {
            var keyExist = dictionary.TryGetValue(moduleName, out var value);
            if (!keyExist) Output.Add(new UnknownModule(moduleName, new List<string>()));
            if (value != null) Output.Add(value);
        }

        foreach (var moduleName in dictionary.Where(x => x.Value.OutputNames.Contains(Name)))
        {
            var keyExist = dictionary.TryGetValue(moduleName.Key, out var value);
            if (!keyExist) Input.Add(new UnknownModule(moduleName.Key, new List<string>()));
            if (value != null) Input.Add(value);
        }
    }
}