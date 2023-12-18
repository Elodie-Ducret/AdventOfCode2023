namespace AdventOfCode2023.Schema.Day16;

public record Grid(char[,] Tiles, int RowCount, int ColumnCount)
{
    private HashSet<Position> EnergizedPositions = new();

    private void PrintGrid(Position nextPosition, Direction direction)
    {
        Console.WriteLine("----------------");
        for (int row = 0; row < RowCount; row++)
        {
            for (int column = 0; column < ColumnCount; column++)
            {
                if (nextPosition == new Position(row, column))
                {
                    switch (direction)
                    {
                        case Direction.North:
                            Console.Write('^');
                            break;
                        case Direction.East:
                            Console.Write('>');
                            break;
                        case Direction.South:
                            Console.Write('V');
                            break;
                        case Direction.West:
                            Console.Write('<');
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                    }
                }
                else
                {
                    Console.Write(EnergizedPositions.Contains(new Position(row, column)) ? '#' : Tiles[row, column]);
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("----------------");
    }

    public long GenerateEnergizedPositions(Position startingPosition, Direction startingDirection)
    {
        EnergizedPositions = new HashSet<Position>();
        var alreadySeenValues = new HashSet<(Position, Direction)>();
        var currentValues = new List<(Position position, Direction direction)>()
            { (startingPosition, startingDirection) };
        while (currentValues.Count != 0)
        {
            var currentValue = currentValues.First();

            var nextPositions = GetNextPositions(currentValue.position, currentValue.direction);
            EnergizedPositions.Add(currentValue.position);
            currentValues.Remove(currentValue);
            var validPositions = nextPositions.Where(x =>
                x.position.Row >= 0 && x.position.Row < RowCount && x.position.Column >= 0 &&
                x.position.Column < ColumnCount).ToList();
            alreadySeenValues.Add(currentValue);
            currentValues.AddRange(validPositions.Where(x => !alreadySeenValues.Contains(x)));
        }

        return EnergizedPositions.Count;
    }


    private List<(Position position, Direction direction)> GetNextPositions(Position position, Direction fromDirection)
    {
        var tile = Tiles[position.Row, position.Column];
        switch (tile)
        {
            case '.':
                return [(position + DirectionPosition.GetPosition(fromDirection), fromDirection)];
            case '-':
            case '|':
                return GetSplittersNextPositions(tile, position, fromDirection);
            case '/':
            case '\\':
                return GetMirrorsNextPositions(tile, position, fromDirection);
            default: return [];
        }
    }

    private List<(Position position, Direction direction)> GetMirrorsNextPositions(char tile, Position position,
        Direction fromDirection)
    {
        if (tile == '/')
        {
            switch (fromDirection)
            {
                case Direction.North:
                    return [(position + DirectionPosition.GetPosition(Direction.East), Direction.East)];
                case Direction.East:
                    return [(position + DirectionPosition.GetPosition(Direction.North), Direction.North)];
                case Direction.South:
                    return [(position + DirectionPosition.GetPosition(Direction.West), Direction.West)];
                case Direction.West:
                    return [(position + DirectionPosition.GetPosition(Direction.South), Direction.South)];
                default:
                    throw new ArgumentOutOfRangeException(nameof(fromDirection), fromDirection, null);
            }
        }

        switch (fromDirection)
        {
            case Direction.North:
                return [(position + DirectionPosition.GetPosition(Direction.West), Direction.West)];
            case Direction.East:
                return [(position + DirectionPosition.GetPosition(Direction.South), Direction.South)];
            case Direction.South:
                return [(position + DirectionPosition.GetPosition(Direction.East), Direction.East)];
            case Direction.West:
                return [(position + DirectionPosition.GetPosition(Direction.North), Direction.North)];
            default:
                throw new ArgumentOutOfRangeException(nameof(fromDirection), fromDirection, null);
        }
    }

    private List<(Position position, Direction direction)> GetSplittersNextPositions(char tile, Position position,
        Direction fromDirection)
    {
        if (tile == '-')
        {
            switch (fromDirection)
            {
                case Direction.North:
                case Direction.South:
                    return
                    [
                        (position + DirectionPosition.GetPosition(Direction.West), Direction.West),
                        (position + DirectionPosition.GetPosition(Direction.East), Direction.East)
                    ];
                case Direction.East:
                    return [(position + DirectionPosition.GetPosition(Direction.East), Direction.East)];

                case Direction.West:
                    return [(position + DirectionPosition.GetPosition(Direction.West), Direction.West)];
                default:
                    throw new ArgumentOutOfRangeException(nameof(fromDirection), fromDirection, null);
            }
        }

        switch (fromDirection)
        {
            case Direction.North:
                return [(position + DirectionPosition.GetPosition(Direction.North), Direction.North)];
            case Direction.East:
            case Direction.West:
                return
                [
                    (position + DirectionPosition.GetPosition(Direction.North), Direction.North),
                    (position + DirectionPosition.GetPosition(Direction.South), Direction.South)
                ];
            case Direction.South:
                return [(position + DirectionPosition.GetPosition(Direction.South), Direction.South)];

            default:
                throw new ArgumentOutOfRangeException(nameof(fromDirection), fromDirection, null);
        }
    }
}