namespace AdventOfCode2023.Schema.Day15;

public record Sequence(string Value)
{
    public readonly string Value = Value;

    public long GetResult()
    {
        var currentValue = 0;
        foreach (var asciiInt in Value.Select(c => (int)c))
        {
            currentValue += asciiInt;
            currentValue *= 17;
            currentValue %= 256;
        }

        return currentValue;
    }
}