namespace AdventOfCode2023.Schema.Day13;

public record Pattern(bool[,] Floor, int RowCount, int ColumnCount)
{
    public bool[,] Floor = Floor;
    public int RowCount = RowCount;
    public int ColumnCount = ColumnCount;


    public void PrintPattern()
    {
        for (int row = 0; row < RowCount; row++)
        {
            for (int column = 0; column < ColumnCount; column++)
            {
                Console.Write(Floor[row, column] ? '#' : '.');
            }

            Console.WriteLine();
        }
    }

    private int GetMirrorRowCount(int row)
    {
        var count = Math.Min(row + 1, RowCount - 1 - row);
        if (count <= 0) return 0;
        for (var rowFloor = 0; rowFloor < count; rowFloor++)
        {
            for (int columnFloor = 0; columnFloor < ColumnCount; columnFloor++)
            {
                if (Floor[row - rowFloor, columnFloor] != Floor[row + 1 + rowFloor, columnFloor]) return 0;
            }
        }

        if (count == 0) return 0;
        return row + 1;
    }

    private int GetMirrorColumnCount(int column)
    {
        var count = Math.Min(column + 1, ColumnCount - 1 - column);
        if (count <= 0) return 0;
        for (var columnFloor = 0; columnFloor < count; columnFloor++)
        {
            for (int rowFloor = 0; rowFloor < RowCount; rowFloor++)
            {
                if (Floor[rowFloor, column - columnFloor] != Floor[rowFloor, column + 1 + columnFloor]) return 0;
            }
        }

        if (count == 0) return 0;
        return column + 1;
    }

    public (int, int)? GetMirrorRowPossibility(int row)
    {
        var count = Math.Min(row + 1, RowCount - 1 - row);
        if (count <= 0) return null;
        var possibilities = new List<(int, int)>();
        for (var rowFloor = 0; rowFloor < count; rowFloor++)
        {
            for (int columnFloor = 0; columnFloor < ColumnCount; columnFloor++)
            {
                if (Floor[row - rowFloor, columnFloor] != Floor[row + 1 + rowFloor, columnFloor])
                {
                    possibilities.Add(!Floor[row - rowFloor, columnFloor]
                        ? (row - rowFloor, columnFloor)
                        : (row + 1 + rowFloor, columnFloor));
                }

                if (possibilities.Count > 1) return null;
            }
        }

        return possibilities.Count == 1 ? possibilities.First() : null;
    }

    public (int, int)? GetMirrorColumnPossibility(int column)
    {
        var count = Math.Min(column + 1, ColumnCount - 1 - column);
        if (count <= 0) return null;
        var possibilities = new List<(int, int)>();
        for (var columnFloor = 0; columnFloor < count; columnFloor++)
        {
            for (int rowFloor = 0; rowFloor < RowCount; rowFloor++)
            {
                if (Floor[rowFloor, column - columnFloor] != Floor[rowFloor, column + 1 + columnFloor])
                {
                    if (!Floor[rowFloor, column - columnFloor] || !Floor[rowFloor, column + 1 + columnFloor])
                    {
                        possibilities.Add(!Floor[rowFloor, column - columnFloor]
                            ? (rowFloor, column - columnFloor)
                            : (rowFloor, column + 1 + columnFloor));
                    }
                }

                if (possibilities.Count > 1) return null;
            }
        }

        return (possibilities.Count == 1) ? possibilities.First() : null;
    }

    public long GetPatternCount()
    {
        Console.WriteLine("New pattern");
        PrintPattern();
        var sumNumberOfRowAboveHorizontal = 0;
        for (int row = 0; row < RowCount; row++)
        {
            sumNumberOfRowAboveHorizontal += GetMirrorRowCount(row);
        }


        var sumNumberOfColumnLeftVertical = 0;
        for (int column = 0; column < ColumnCount; column++)
        {
            sumNumberOfColumnLeftVertical += GetMirrorColumnCount(column);
        }

        Console.WriteLine($"Pattern count => {sumNumberOfColumnLeftVertical} + 100 * {sumNumberOfRowAboveHorizontal}");
        return sumNumberOfColumnLeftVertical + 100 * sumNumberOfRowAboveHorizontal;
    }
}