using static System.String;

namespace AdventOfCode2023.Schema.Day19.Part1;

public record SystemWorkflow(HashSet<Workflow> Workflows, HashSet<Rating> Ratings)
{
    public long GetAcceptedCount()
    {
        var startWorkflow = Workflows.First(x => x.Name == "in");
        long response = 0;
        foreach (var rating in Ratings)
        {
            var wf = startWorkflow.Name;
            do
            {
                wf = Workflows.First(x => x.Name == wf).Apply(rating);
            } while (Compare(wf, "A", StringComparison.Ordinal) != 0 &&
                     Compare(wf, "R", StringComparison.Ordinal) != 0);

            if (Compare(wf, "A", StringComparison.Ordinal) == 0)
            {
                response += rating.GetSumValues();
            }
        }

        return response;
    }
}