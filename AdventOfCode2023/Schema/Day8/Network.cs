namespace AdventOfCode2023.Schema.Day8;

public record Network(List<int> Navigation, Dictionary<string, List<string>> Nodes)
{
    public List<int> Navigation = Navigation;
    public Dictionary<string, List<string>> Nodes = Nodes;
}