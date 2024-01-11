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

    public long DijkstraAlgorithmPart2()
    {
        var targetPosition = new Position(RowCount - 1, ColumnCount - 1);
        var toExplore = new PriorityQueue<State, long>();
        var explored = new HashSet<State>();

        toExplore.Enqueue(new State(Direction.North, 0, new Position(0, 0)), 0);

        while (toExplore.TryDequeue(out var state, out var distance))
        {
            if (!explored.Add(state)) continue;
            if (state.Position == targetPosition) return distance;
            foreach (var neighbor in GetNeighborsPart2(state))
            {
                if (!explored.Contains(neighbor.NewState))
                    toExplore.Enqueue(neighbor.NewState,
                        distance + neighbor.Distance);
            }
        }

        return -1;
    }


    private List<(State NewState, int Distance)> GetNeighborsPart2(State state)
    {
        var list = new List<(State state, int distance)>();
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var newPosition = state.Position + DirectionPosition.GetPosition(direction);
            if (0 <= newPosition.Row && newPosition.Row < RowCount && 0 <= newPosition.Column &&
                newPosition.Column < ColumnCount)
            {
                if (state.CountSameDirection == 0)
                {
                    var steps = Get4Steps(state, direction);
                    if (steps != null) list.Add(steps.Value);
                    continue;
                }

                if (DirectionPosition.GetOpposite(direction) == state.Direction) continue;
                var sameDirection = direction == state.Direction;

                if (state.CountSameDirection is >= 1 and < 4)
                {
                    throw new Exception("Bad case");
                }

                if (state.CountSameDirection is >= 4 and < 10)
                {
                    if (sameDirection)
                    {
                        list.Add((new State(direction, state.CountSameDirection + 1, newPosition),
                            Cities[newPosition.Row, newPosition.Column]));
                    }
                    else
                    {
                        var steps = Get4Steps(state, direction);
                        if (steps != null) list.Add(steps.Value);
                    }

                    continue;
                }

                if (state.CountSameDirection == 10)
                {
                    if (state.Direction == direction) continue;
                    var steps = Get4Steps(state, direction);
                    if (steps != null) list.Add(steps.Value);
                }
            }
        }

        return list;
    }

    private (State, int)? Get4Steps(State state, Direction direction)
    {
        var newFarPosition = state.Position + 4 * DirectionPosition.GetPosition(direction);
        if (0 <= newFarPosition.Row && newFarPosition.Row < RowCount && 0 <= newFarPosition.Column &&
            newFarPosition.Column < ColumnCount)
        {
            var distance = 0;
            for (int i = 1; i < 5; i++)
            {
                var n = i * DirectionPosition.GetPosition(direction) + state.Position;
                var d = Cities[n.Row, n.Column];
                distance += d;
            }


            return (new State(direction, 4, newFarPosition), distance);
        }

        return null;
    }
}

public record struct State(Direction Direction, int CountSameDirection, Position Position);