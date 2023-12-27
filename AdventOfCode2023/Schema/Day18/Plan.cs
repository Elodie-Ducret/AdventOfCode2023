using AdventOfCode2023.Common;

namespace AdventOfCode2023.Schema.Day18;

public record Plan(List<DigPlan> DigPlans)
{
    private readonly List<Position> _plans = new();

    private readonly HashSet<Position> _path = new();
    private bool[,]? _array;
    private int _rowCount = 0;
    private int _columnCount = 0;


    private void BuildPlanArray()
    {
        var position = new Position(0, 0);
        foreach (var plan in DigPlans)
        {
            for (int i = 0; i < plan.Meters; i++)
            {
                var newPosition = position + DirectionPosition.GetPosition(plan.Direction);
                _plans.Add(newPosition);
                position = newPosition;
            }
        }

        var minRow = _plans.MinBy(x => x.Row).Row;
        var minColumn = _plans.MinBy(x => x.Column).Column;
        _rowCount = _plans.MaxBy(x => x.Row).Row - minRow + 1;
        _columnCount = _plans.MaxBy(x => x.Column).Column - minColumn + 1;
        _array = new bool[_rowCount, _columnCount];
        foreach (var plan in _plans)
        {
            _array[plan.Row - minRow, plan.Column - minColumn] = true;
            _path.Add(new Position(plan.Row - minRow, plan.Column - minColumn));
        }

        PrintArray();
    }

    private void PrintArray()
    {
        var docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        using StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "info.txt"));
        for (int row = 0; row < _rowCount; row++)
        {
            for (int column = 0; column < _columnCount; column++)
            {
                if (_array != null && _array[row, column])
                {
                    outputFile.Write('#');
                }
                else
                {
                    outputFile.Write('.');
                }
            }

            outputFile.WriteLine();
        }
    }

    public long GetSum()
    {
        BuildPlanArray();
        var groups = GetAllGroups();

        long sum = 0;
        foreach (var group in groups)
        {
            var count = GetCount(group);
            if (count != null) sum += count.Value;
        }

        return sum;
    }

    private long? GetCount(List<Position> positions)
    {
        if (_array[positions.First().Row, positions.First().Column])
        {
            return positions.Count;
        }

        var topElement = positions.MinBy(x => x.Row);
        if (topElement.Row == 0) return null;

        if (!_array[topElement.Row - 1, topElement.Column]) return null;

        var bottomElement = positions.MaxBy(x => x.Row);
        if (bottomElement.Row == _rowCount - 1) return null;
        if (!_array[bottomElement.Row + 1, bottomElement.Column]) return null;

        var rightElement = positions.MinBy(x => x.Column);
        if (rightElement.Column == 0) return null;
        if (!_array[rightElement.Row, rightElement.Column - 1]) return null;

        var leftElement = positions.MaxBy(x => x.Column);
        if (leftElement.Column == _columnCount - 1) return null;
        if (!_array[leftElement.Row, leftElement.Column + 1]) return null;

        return positions.Count;
    }

    private readonly HashSet<Position> _visited = new();

    private List<List<Position>> GetAllGroups()
    {
        var components = new List<List<Position>>();
        for (var row = 0; row < _rowCount; row++)
        {
            for (var column = 0; column < _columnCount; column++)
            {
                var position = new Position(row, column);
                if (_visited.Contains(position)) continue;
                var positions = Compute(position);
                if (positions.Count > 0) components.Add(positions);
            }
        }

        return components;
    }

    private List<Position> Compute(Position source)
    {
        if (_visited.Contains(source)) return new List<Position>();
        var component = new List<Position>();
        var queue = new Queue<Position>();

        queue.Enqueue(source);

        while (queue.TryDequeue(out var element))
        {
            if (_visited.Contains(element)) continue;
            component.Add(element);
            _visited.Add(element);

            foreach (var adjacent in GetNeighbors(element))
                if (!_visited.Contains(adjacent))
                    queue.Enqueue(adjacent);
        }

        return component;
    }

    private List<Position> GetNeighbors(Position position)
    {
        var potentialPositions = new[]
        {
            position + DirectionPosition.GetPosition(Direction.North),
            position + DirectionPosition.GetPosition(Direction.South),
            position + DirectionPosition.GetPosition(Direction.East),
            position + DirectionPosition.GetPosition(Direction.West)
        };

        var validPositions = potentialPositions
            .Where(x => x.Row >= 0 && x.Row < _rowCount && x.Column >= 0 && x.Column < _columnCount)
            .ToList();

        var tt = _array[position.Row, position.Column]
            ? validPositions.Where(x => _path.Contains(x)).ToList()
            : validPositions.Where(x => !_path.Contains(x)).ToList();

        return tt;
    }
}