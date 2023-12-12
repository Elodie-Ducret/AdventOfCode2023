namespace AdventOfCode2023.Schema.Day11;

public record Universe(char[,] BaseGalaxy, HashSet<int> RowDuplicated, HashSet<int> ColumnDuplicated, List<GalaxyPosition> Galaxies)
{
    public char[,] BaseGalaxy = BaseGalaxy;
    public HashSet<int> RowDuplicated = RowDuplicated; 
    public HashSet<int> ColumnDuplicated = ColumnDuplicated;
    public List<GalaxyPosition> Galaxies = Galaxies;



}