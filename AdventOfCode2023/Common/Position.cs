namespace AdventOfCode2023.Common;

public record struct Position(int Row, int Column)
{
    public int Row = Row;
    public int Column = Column;

    public static Position operator +(in Position lhs, in Position rhs) =>
        new(lhs.Row + rhs.Row,
            lhs.Column + rhs.Column);
    
    
    public static Position operator -(in Position lhs, in Position rhs) =>
        new(lhs.Row - rhs.Row,
            lhs.Column - rhs.Column);
}