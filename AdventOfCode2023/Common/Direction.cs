namespace AdventOfCode2023.Common;

public enum Direction
{
    North,
    East,
    South,
    West
}

public static class DirectionPosition
{
    private static readonly Position NorthPosition = new(-1, 0);
    private static readonly Position EastPosition = new(0, 1);
    private static readonly Position SouthPosition = new(1, 0);
    private static readonly Position WestPosition = new(0, -1);

    public static Position GetPosition(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return NorthPosition;
            case Direction.East:
                return EastPosition;
            case Direction.South:
                return SouthPosition;
            case Direction.West:
                return WestPosition;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    public static Direction GetDirectionRLUD(char dir)
    {
        switch (dir)
        {
            case 'R': return Direction.East;
            case 'L': return Direction.West;
            case 'U': return Direction.North;
            case 'D': return Direction.South;
            default: throw new NotImplementedException();
        }
    }
}