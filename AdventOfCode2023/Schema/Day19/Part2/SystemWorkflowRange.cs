using AdventOfCode2023.Common;
using static System.String;

namespace AdventOfCode2023.Schema.Day19.Part2;

public record SystemWorkflowRange(HashSet<WorkflowRange> Workflows)
{
    public long GetCombinationAcceptedCount()
    {
        var ratingRange = new RatingRange(new Dictionary<char, Int2>()
        {
            { 'x', new Int2(1, 4000) },
            { 'm', new Int2(1, 4000) },
            { 'a', new Int2(1, 4000) },
            { 's', new Int2(1, 4000) },
        });
        
        var startWorkflow = Workflows.First(x => x.Name == "in");

        var queue = new Queue<(RatingRange ratingRange, WorkflowRange Workflow)>();
        queue.Enqueue((ratingRange, startWorkflow));

        long response = 0;
        while (queue.TryDequeue(out var element))
        {
            var outputs = element.Workflow.Apply(element.ratingRange);
            foreach (var output in outputs)
            {
                if (Compare(output.Item2, "A", StringComparison.Ordinal) != 0 &&
                    Compare(output.Item2, "R", StringComparison.Ordinal) != 0)
                {
                    queue.Enqueue((output.Item1, Workflows.First(x => x.Name == output.Item2)));
                }
                else
                {
                    if (Compare(output.Item2, "A", StringComparison.Ordinal) != 0) continue;
                    response += output.Item1.GetSumValues();
                }
            }
        }

        return response;
    }
}