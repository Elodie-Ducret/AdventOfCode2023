namespace AdventOfCode2023.Schema.Day16;

public enum Direction
{
    North,
    East,
    South,
    West
}

public static class DirectionPosition
{
    public static Position GetPosition(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return new Position(-1, 0);
            case Direction.East:
                return new Position(0, 1);
            case Direction.South:
                return new Position(1, 0);
            case Direction.West:
                return new Position(0, -1);
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}