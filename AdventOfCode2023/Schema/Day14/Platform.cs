namespace AdventOfCode2023.Schema.Day14;

public record Platform(char[,] InitialPlatform, int RowCount, int ColumnCount)
{
    public char[,] InitialPlatform = InitialPlatform;
    public int RowCount = RowCount;
    public int ColumnCount = ColumnCount;
}