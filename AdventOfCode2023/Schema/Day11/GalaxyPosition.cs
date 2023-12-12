namespace AdventOfCode2023.Schema.Day11;

public record struct GalaxyPosition(long Row, long Column)
{
    public long Row = Row;
    public long Column = Column;

    public static GalaxyPosition operator +(in GalaxyPosition lhs, in GalaxyPosition rhs) =>
        new(lhs.Row + rhs.Row,
            lhs.Column + rhs.Column);

    public static GalaxyPosition operator -(in GalaxyPosition lhs, in GalaxyPosition rhs) =>
        new(lhs.Row - rhs.Row,
            lhs.Column - rhs.Column);
}