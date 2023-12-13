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

    public int GetMirrorRowCount(int row)
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
        Console.WriteLine($"ROW OK : `{row}` => `{row + 1}`");
        return row + 1;
    }

    public int GetMirrorColumnCount(int column)
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
        Console.WriteLine($"COLUMN OK : `{column}`=> `{column + 1}`");
        return column + 1;
    }
}