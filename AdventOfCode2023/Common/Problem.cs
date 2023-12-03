namespace AdventOfCode2023.Common;

public abstract class Problem<TInput, TOutput>(string name, string path)
{
    private IEnumerable<string> ReadInput()
    {
        var lines = File.ReadAllLines("../../../Inputs/" + path);
        return lines;
    }

    protected abstract TInput Convert(IEnumerable<string> input);
    protected abstract TOutput Solve(TInput input);

    protected TOutput SolveProblem()
    {
        var input = ReadInput();
        var inputConverted = Convert(input);
        var result = Solve(inputConverted);
        Console.WriteLine($"Problem {name}: {result}");
        return result;
    }
}