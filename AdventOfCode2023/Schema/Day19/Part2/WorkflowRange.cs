namespace AdventOfCode2023.Schema.Day19.Part2;

public record WorkflowRange(string Name, List<RuleRange> Rules)
{
    public List<(RatingRange, string)> Apply(RatingRange ratingRange)
    {
        var queue = new Queue<(RatingRange ratingRange, int RuleId)>();
        queue.Enqueue((ratingRange, 0));


        var response = new List<(RatingRange, string)>();

        while (queue.TryDequeue(out var element))
        {
            var nextList = Rules[element.RuleId].Apply(element.ratingRange);
            foreach (var next in nextList)
            {
                if (next.Item2 == null)
                {
                    queue.Enqueue((next.Item1, element.RuleId + 1));
                }
                else
                {
                    response.Add((next.Item1, next.Item2));
                }
            }
        }

        return response;
    }
}