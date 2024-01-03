using AdventOfCode2023.Common;

namespace AdventOfCode2023.Schema.Day19.Part2;

public record RatingRange(Dictionary<char, Int2> Values)
{
    public long GetSumValues() => Values
        .Select(x => (long)(x.Value.End - x.Value.Start + 1))
        .Aggregate<long, long>(1, (current, t) => current * t);
}