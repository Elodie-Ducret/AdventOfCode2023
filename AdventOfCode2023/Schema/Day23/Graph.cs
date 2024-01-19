using AdventOfCode2023.Common;

namespace AdventOfCode2023.Schema.Day23;

public record Graph(char[,] Tiles, int RowCount, int ColumnCount, Position Start, Position End)
{
    private readonly HashSet<Position> _explored = new();
    private long _maxDistance = -1;

    public long GetMaxCount()
    {
        Visit(Start, 0);
        return _maxDistance;
    }

    private void Visit(Position node, long distance)
    {
        _explored.Add(node);
        if (node == End)
        {
            if (distance > _maxDistance) _maxDistance = distance;
            _explored.Remove(node);
            return;
        }

        foreach (var neighbor in GetNeighbors(node).Where(neighbor => !_explored.Contains(neighbor.position)))
        {
            Visit(neighbor.position, distance + neighbor.distance);
        }

        _explored.Remove(node);
    }

    private List<(Position position, int distance)> GetNeighbors(Position position)
    {
        var neighbors = new List<(Position position, int distance)>();
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var nextPosition = position + DirectionPosition.GetPosition(direction);
            if (nextPosition.Row >= 0 && nextPosition.Row < RowCount && nextPosition.Column >= 0 &&
                nextPosition.Column < ColumnCount)
            {
                switch (Tiles[nextPosition.Row, nextPosition.Column])
                {
                    case '#':
                        break;
                    case '.':
                        neighbors.Add((nextPosition, 1));
                        break;
                    default:
                        var dir = GetDirection(Tiles[nextPosition.Row, nextPosition.Column]);
                        neighbors.Add((nextPosition + DirectionPosition.GetPosition(dir), 2));
                        break;
                }
            }
        }

        return neighbors.ToList();
    }

    private static Direction GetDirection(char c)
    {
        return c switch
        {
            '<' => Direction.West,
            'v' => Direction.South,
            '>' => Direction.East,
            _ => throw new ArgumentException($"Unknown {c}")
        };
    }
}