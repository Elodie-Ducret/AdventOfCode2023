using AdventOfCode2023.Common;

namespace AdventOfCode2023.Schema.Day22;

public record Brick(char Name, Position3 Start, Position3 End)
{
    private List<Position3>? _allPositions = null;

    private Position3 _startAfterFalling;
    private Position3 _endAfterFalling;
    private List<Position3>? _allPositionsFalling = null;

    public HashSet<Brick> BricksUnder = new();
    public HashSet<Brick> BrickOver = new();

    public Position3 GetMaxPosition()
    {
        return Position3.Max(Start, End);
    }

    public List<Position3> GetAllPositions()
    {
        if (_allPositions != null) return _allPositions;
        var list = new List<Position3>();
        var positions = End - Start;
        for (int row = 0; row < positions.Row + 1; row++)
        {
            for (int column = 0; column < positions.Column + 1; column++)
            {
                for (int elevation = 0; elevation < positions.Elevation + 1; elevation++)
                {
                    list.Add(new Position3(Start.Row + row, Start.Column + column, Start.Elevation + elevation));
                }
            }
        }

        _allPositions = list;
        return list;
    }


    private List<Position3> GetAllPositionsAfterFalling()
    {
        if (_allPositionsFalling != null) return _allPositionsFalling;
        var list = new List<Position3>();
        var positions = _endAfterFalling - _startAfterFalling;
        for (int row = 0; row < positions.Row + 1; row++)
        {
            for (int column = 0; column < positions.Column + 1; column++)
            {
                for (int elevation = 0; elevation < positions.Elevation + 1; elevation++)
                {
                    list.Add(new Position3(_startAfterFalling.Row + row, _startAfterFalling.Column + column,
                        _startAfterFalling.Elevation + elevation));
                }
            }
        }

        _allPositionsFalling = list;
        return list;
    }


    public List<Position3> BuildPositionAfterFalling(Brick?[,,] bricksFall)
    {
        if (_allPositionsFalling != null) return _allPositionsFalling;
        var allPositions = GetAllPositions();
        var validElevation = allPositions.MinBy(x => x.Elevation).Elevation;
        var bricksUnder = new HashSet<Brick>();
        do
        {
            foreach (var position in allPositions)
            {
                var touchingBrick = bricksFall[position.Row, position.Column, validElevation - 1];
                if (touchingBrick != null)
                {
                    bricksUnder.Add(touchingBrick);
                }
            }

            if (validElevation <= 1) break;

            if (bricksUnder.Count == 0) validElevation--;
        } while (bricksUnder.Count == 0);

        _startAfterFalling = new Position3(Start.Row, Start.Column, validElevation);
        _endAfterFalling = new Position3(End.Row, End.Column, End.Elevation - Start.Elevation + validElevation);
        _allPositionsFalling = GetAllPositionsAfterFalling();

        BricksUnder = bricksUnder;
        foreach (var b in bricksUnder)
        {
            b.BrickOver.Add(this);
        }

        return _allPositionsFalling;
    }
}