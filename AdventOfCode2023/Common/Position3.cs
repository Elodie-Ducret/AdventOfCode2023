namespace AdventOfCode2023.Common;

public record struct Position3(int Row, int Column, int Elevation)
{
    public static Position3 operator +(in Position3 lhs, in Position3 rhs) =>
        new(lhs.Row + rhs.Row,
            lhs.Column + rhs.Column,
            lhs.Elevation + rhs.Elevation);


    public static Position3 operator -(in Position3 lhs, in Position3 rhs) =>
        new(lhs.Row - rhs.Row,
            lhs.Column - rhs.Column,
            lhs.Elevation - rhs.Elevation);

    public static Position3 Max(in Position3 lhs, in Position3 rhs) =>
        new (Math.Max(lhs.Row, rhs.Row),
            Math.Max(lhs.Column, rhs.Column),
            Math.Max(lhs.Elevation, rhs.Elevation));
}