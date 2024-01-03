using AdventOfCode2023.Common;

namespace AdventOfCode2023.Schema.Day19.Part2;

public record CompareRange(char Name, char Comparator, int Value)
{
    public (RatingRange, bool)[] Apply(RatingRange rating)
    {
        var isExist = rating.Values.TryGetValue(Name, out var ratingValue);
        if (!isExist) throw new ArgumentException();
        if (ratingValue == null) throw new ArgumentException();


        if (ratingValue.End < Value && ratingValue.Start < Value)
            return new[] { (rating, GetCompareValue(ratingValue.End)) };

        if (ratingValue.End > Value && ratingValue.Start > Value)
            return new[] { (rating, GetCompareValue(ratingValue.End)) };


        switch (Comparator)
        {
            case '<':
                return GetNewRating(new[]
                {
                    (new Int2(ratingValue.Start, Value - 1), ratingValue.Start < Value),
                    (new Int2(Value, ratingValue.End), ratingValue.End < Value)
                }, rating);
            case '>':
                return GetNewRating(new[]
                {
                    (new Int2(ratingValue.Start, Value), ratingValue.Start > Value),
                    (new Int2(Value+1, ratingValue.End), ratingValue.End > Value)
                }, rating);
            default: throw new ArgumentException();
        }
    }


    private (RatingRange, bool)[] GetNewRating((Int2 ranges, bool response)[] newRanges, RatingRange baseRating)
    {
        var response = new List<(RatingRange, bool)>();
        foreach (var newRange in newRanges)
        {
            var newDic = new Dictionary<char, Int2>();
            foreach (var keyValue in baseRating.Values)
            {
                if (keyValue.Key == Name)
                {
                    newDic.Add(Name, newRange.ranges);
                }
                else
                {
                    newDic.Add(keyValue.Key, keyValue.Value);
                }
            }

            response.Add((new RatingRange(newDic), newRange.response));
        }

        return response.ToArray();
    }

    private bool GetCompareValue(int ratingValue)
    {
        return Comparator switch
        {
            '<' => ratingValue < Value,
            '>' => ratingValue > Value,
            _ => throw new ArgumentException()
        };
    }
}