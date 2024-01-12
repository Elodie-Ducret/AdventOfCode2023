using AdventOfCode2023.Common;

namespace AdventOfCode2023.Schema.Day21;

public record Garden(char[,] Tiles, Position Start, int RowCount, int ColumnCount)
{
    public long GetAccessibleTilesCount(int? step = 64)
    {
        var current = new Queue<Position>();
        var next = new Queue<Position>();
        current.Enqueue(Start);

        var positions = new HashSet<Position>();
        for (var s = 0; s < step; s++)
        {
            positions = new HashSet<Position>();
            while (current.TryDequeue(out var position))
            {
                var neighbors = GetNeighbors(position);
                foreach (var neighbor in neighbors)
                {
                    positions.Add(neighbor);
                    if (!next.Contains(neighbor)) next.Enqueue(neighbor);
                }
            }

            current = next;
            next = new Queue<Position>();
        }

        return positions.Count;
    }

    private List<Position> GetNeighbors(Position position)
    {
        var list = new List<Position>();
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var newPosition = position + DirectionPosition.GetPosition(direction);
            if (0 <= newPosition.Row && newPosition.Row < RowCount && 0 <= newPosition.Column &&
                newPosition.Column < ColumnCount)
            {
                if (Tiles[newPosition.Row, newPosition.Column] == '.' ||
                    Tiles[newPosition.Row, newPosition.Column] == 'S')
                {
                    list.Add(newPosition);
                }
            }
        }

        return list;
    }
}