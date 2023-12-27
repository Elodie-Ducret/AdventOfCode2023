namespace AdventOfCode2023.Schema.Day22;

public record Snapshot(
    Brick?[,,] BricksMatrix,
    HashSet<Brick> Bricks,
    int RowCount,
    int ColumnCount,
    int ElevationCount)
{
    private readonly Brick?[,,] _bricksAfterFallen = new Brick[RowCount, ColumnCount, ElevationCount];

    public long GetSafeBricksCountToDisintegrate()
    {
        BuildAfterFallen();
        var sum = 0;
        foreach (var brick in Bricks)
        {
            if (brick.BrickOver.Count == 0) sum++;
            else
            {
                var isRemovable = brick.BrickOver.All(brickOver => brickOver.BricksUnder.Count != 1);
                if (isRemovable) sum++;
            }
        }

        // DisplayRow(_bricksAfterFallen);
        // DisplayColumn(_bricksAfterFallen);

        return sum;
    }

    public long GetOtherBricksFallCount()
    {
        BuildAfterFallen();
        var sum = 0;
        foreach (var brick in Bricks)
        {
            if (brick.BrickOver.Count == 0) continue;

            var bricks = new HashSet<Brick>();

            var queue = new Queue<Brick>();
            queue.Enqueue(brick);

            while (queue.TryDequeue(out var element))
            {
                bricks.Add(element);
                foreach (var brickOver in element.BrickOver)
                {
                    if (!brickOver.BricksUnder.All(x => bricks.Contains(x))) continue;
                    bricks.Add(brickOver);
                    queue.Enqueue(brickOver);
                }
            }

            sum += bricks.Count - 1;
        }


        return sum;
    }

    private void BuildAfterFallen()
    {
        for (int elevation = 0; elevation < ElevationCount; elevation++)
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    var brick = BricksMatrix[row, column, elevation];
                    if (brick == null) continue;
                    var newBrickPositions = brick.BuildPositionAfterFalling(_bricksAfterFallen);
                    foreach (var position in newBrickPositions)
                    {
                        _bricksAfterFallen[position.Row, position.Column, position.Elevation] = brick;
                    }
                }
            }
        }
    }


    private void DisplayRow(Brick?[,,] bricks)
    {
        for (int elevation = ElevationCount - 1; elevation >= 1; elevation--)
        {
            for (int row = 0; row < RowCount; row++)
            {
                var letter = '.';
                for (int column = 0; column < ColumnCount; column++)
                {
                    var brick = bricks[row, column, elevation];
                    if (brick != null)
                    {
                        letter = letter == '.' || letter == brick.Name ? brick.Name : '?';
                    }
                }

                Console.Write(letter);
            }

            Console.WriteLine();
        }

        Console.WriteLine("---");
    }

    private void DisplayColumn(Brick?[,,] bricks)
    {
        for (int elevation = ElevationCount - 1; elevation >= 1; elevation--)
        {
            for (int column = 0; column < ColumnCount; column++)
            {
                var letter = '.';
                for (int row = 0; row < RowCount; row++)
                {
                    var brick = bricks[row, column, elevation];
                    if (brick != null)
                    {
                        letter = letter == '.' || letter == brick.Name ? brick.Name : '?';
                    }
                }

                Console.Write(letter);
            }

            Console.WriteLine();
        }

        Console.WriteLine("---");
    }
}