namespace AdventOfCode2023.Schema.Day15;

public record Book(List<Sequence> Items)
{
    public readonly List<Sequence> Items = Items;

    public long GetBookValue()
    {
        return Items.Sum(x => x.GetResult());
    }
}