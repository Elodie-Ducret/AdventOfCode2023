namespace AdventOfCode2023.Common;

public record struct Position(long Row, long Column)
{
    public long Row = Row;
    public long Column = Column;

    public static Position operator +(in Position lhs, in Position rhs) =>
        new(lhs.Row + rhs.Row,
            lhs.Column + rhs.Column);


    public static Position operator -(in Position lhs, in Position rhs) =>
        new(lhs.Row - rhs.Row,
            lhs.Column - rhs.Column);

    public static Position operator *(in long lhs, in Position rhs) =>
        new(lhs * rhs.Row,
            lhs * rhs.Column);
}