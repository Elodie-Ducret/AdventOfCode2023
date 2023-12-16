namespace AdventOfCode2023.Schema.Day10;

public record Grid(char[,] Tiles, int RowCount, int ColumnCount, Position StartingPosition)
{
    public int RowCount = RowCount;
    public int ColumnCount = ColumnCount;
    public char[,] Tiles = Tiles;
    public Position StartingPosition = StartingPosition;

    private Position[] GetOnlyValidPositions(Position[] positions)
    {
        return positions
            .Where(x => x.Column >= 0 && x.Column < ColumnCount && x.Row >= 0 && x.Row < RowCount)
            .ToArray();
    }

    public Position[] GetNewValidPositions(Position position)
    {
        var actual = Tiles[position.Row, position.Column];
        switch (actual)
        {
            case '|':
                return GetOnlyValidPositions(new[]
                {
                    new Position(position.Row + 1, position.Column),
                    new Position(position.Row - 1, position.Column)
                });
            case '-':
                return GetOnlyValidPositions(new[]
                {
                    new Position(position.Row, position.Column - 1),
                    new Position(position.Row, position.Column + 1)
                });
            case 'L':
                return GetOnlyValidPositions(new[]
                {
                    new Position(position.Row, position.Column + 1),
                    new Position(position.Row - 1, position.Column)
                });
            case 'J':
                return GetOnlyValidPositions(new[]
                {
                    new Position(position.Row, position.Column - 1),
                    new Position(position.Row - 1, position.Column)
                });
            case '7':
                return GetOnlyValidPositions(new[]
                {
                    new Position(position.Row, position.Column - 1),
                    new Position(position.Row + 1, position.Column)
                });
            case 'F':
                return GetOnlyValidPositions(new[]
                {
                    new Position(position.Row, position.Column + 1),
                    new Position(position.Row + 1, position.Column)
                });

            case 'S':
                return GetOnlyValidPositions(new[]
                {
                    new Position(position.Row - 1, position.Column),
                    new Position(position.Row + 1, position.Column),
                    new Position(position.Row, position.Column + 1),
                    new Position(position.Row, position.Column - 1),
                });
       

            default:
                return Array.Empty<Position>();
        }
    }

    public List<Position> GetNeighbors(Position position)
    {
        var potentialPositions = new []
        {
            new Position(position.Row - 1, position.Column),
            new Position(position.Row + 1, position.Column),
            new Position(position.Row, position.Column + 1),
            new Position(position.Row, position.Column - 1)
        };

        return GetOnlyValidPositions(potentialPositions).ToList(); 

    }
}