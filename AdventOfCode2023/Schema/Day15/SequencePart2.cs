namespace AdventOfCode2023.Schema.Day15;

public record SequencePart2(Sequence Sequence, bool ToRemove, int? FocalLength )
{
    public readonly Sequence Sequence = Sequence;
    public readonly bool ToRemove = ToRemove;
    public int? FocalLength = FocalLength;

    public int GetBoxId()
    {
        return (int)Sequence.GetResult();
    }
}