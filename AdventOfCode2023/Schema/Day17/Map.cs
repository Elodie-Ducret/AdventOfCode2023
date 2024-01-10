using AdventOfCode2023.Common;

namespace AdventOfCode2023.Schema.Day17;

public record Map(int[,] Cities, int RowCount, int ColumnCount)
{
    public long DijkstraAlgorithm()
    {
        var targetPosition = new Position(RowCount - 1, ColumnCount - 1);
        var toExplore = new PriorityQueue<State, long>();
        var explored = new HashSet<State>();

        toExplore.Enqueue(new State(Direction.South, -1, new Position(0, 0)), 0);

        while (toExplore.TryDequeue(out var state, out var distance))
        {
            if (!explored.Add(state)) continue;
            if (state.Position == targetPosition) return distance;
            foreach (var neighbor in GetNeighbors(state))
            {
                if (!explored.Contains(neighbor))
                    toExplore.Enqueue(neighbor, distance + Cities[neighbor.Position.Row, neighbor.Position.Column]);
            }
        }

        return -1;
    }

    private List<State> GetNeighbors(State state)
    {
        var list = new List<State>();
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (DirectionPosition.GetOpposite(direction) == state.Direction) continue;
            var newPosition = state.Position + DirectionPosition.GetPosition(direction);
            if (0 <= newPosition.Row && newPosition.Row < RowCount && 0 <= newPosition.Column &&
                newPosition.Column < ColumnCount)
            {
                if (direction == state.Direction)
                {
                    if (state.CountSameDirection + 1 < 3)
                        list.Add(new State(direction, state.CountSameDirection + 1, newPosition));
                }
                else
                {
                    list.Add(new State(direction, 0, newPosition));
                }
            }
        }


        return list;
    }
}

public record struct State(Direction Direction, int CountSameDirection, Position Position);