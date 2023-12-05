namespace AdventOfCode2023.Schema.Day5;

public record Map
{
    public long DestinationRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long RangeLength { get; set; }

    public long GetDestinationValue(long sourceValue) =>  DestinationRangeStart + (sourceValue - SourceRangeStart);
    public long SourceEnd => SourceRangeStart + RangeLength;
    public long DestinationEnd => DestinationRangeStart + RangeLength;
    
}