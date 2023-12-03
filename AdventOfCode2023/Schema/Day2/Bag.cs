namespace AdventOfCode2023.Schema.Day2;

public class Bag
{
    public int GameNumber { get; set; }

    public List<RGB> Hands { get; set; }
}

public record RGB
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
}