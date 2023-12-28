namespace AdventOfCode2023.Schema.Day19;

public record Compare(char Name, char Comparator, int Value)
{
    public bool Apply(Rating rating)
    {
        var isExist = rating.Values.TryGetValue(Name, out int ratingValue);
        if (!isExist) throw new ArgumentException();
        return Comparator switch
        {
            '<' => ratingValue < Value,
            '>' => ratingValue > Value,
            _ => throw new ArgumentException()
        };
    }
}