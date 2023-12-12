namespace AdventOfCode2023.Schema.Day11;

public record struct GalaxyPosition(int Row, int Column)
{
    public int Row = Row;
    public int Column = Column;

    public static GalaxyPosition operator +(in GalaxyPosition lhs, in GalaxyPosition rhs) =>
        new(lhs.Row + rhs.Row,
            lhs.Column + rhs.Column);

    public static GalaxyPosition operator -(in GalaxyPosition lhs, in GalaxyPosition rhs) =>
        new(lhs.Row - rhs.Row,
            lhs.Column - rhs.Column);
}