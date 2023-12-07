using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day7;

namespace AdventOfCode2023.Problems.Day7;

public class Day7Part2Problem(string name, string path) : Problem<List<Hand>, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day7Part2Problem("Day 7 Part 2", "Day7/Day7.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day7Part2Problem("Day 7 Part 2 Test", "Day7/Day7_Part1_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override List<Hand> Convert(IEnumerable<string> input)
    {
        var hands = new List<Hand>();
        foreach (var hand in input)
        {
            var handSplit = hand.Split(' ');
            var cardChars = handSplit[0].ToCharArray();
            var cardInts = cardChars.Select(c => int.TryParse(c.ToString(), out var value) ? value : _numberChars[c])
                .ToList();
            hands.Add(new Hand(cardInts, int.Parse(handSplit.Last())));
        }

        return hands;
    }

    protected override int Solve(List<Hand> input)
    {
        var handsByType = input.GroupBy(x => x.GetBestTypeWithJoker()).OrderByDescending(c => c.Key).ToList();

        var sum = 0;
        var counter = 1;
        foreach (var group in handsByType)
        {
            var handsOrdered = group.Order();
            foreach (var hand in handsOrdered)
            {
                sum += hand.Bid * counter;
                counter++;
            }
        }

        return sum;
    }

    private readonly Dictionary<char, int> _numberChars = new()
    {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'J', 1 },
        { 'T', 10 },
    };
}