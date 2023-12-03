using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day2;

namespace AdventOfCode2023.Problems.Day2;

public class Day2Part2Problem(string name, string path) : Problem<List<Bag>, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day2Part2Problem("Day 2 Part 1", "Day2/Day2.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day2Part2Problem("Day 2 Part 1 Test", "Day2/Day2_Part1_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override List<Bag> Convert(IEnumerable<string> input)
    {
        var games = new List<Bag>();
        foreach (var game in input)
        {
            var p = game.Split(": ");
            var gameNumber = p[0].Split("Game ")[1];

            var handsSplit = p[1].Split("; ");

            var rgbList = new List<RGB>();
            foreach (var hands in handsSplit)
            {
                var handSplit = hands.Split(", ");

                var r = handSplit.FirstOrDefault(x => x.Contains("red"))?.Split(" ").First();
                var g = handSplit.FirstOrDefault(x => x.Contains("green"))?.Split(" ").First();
                var b = handSplit.FirstOrDefault(x => x.Contains("blue"))?.Split(" ").First();

                var rgb = new RGB
                {
                    R = r == null ? 0 : int.Parse(r),
                    G = g == null ? 0 : int.Parse(g),
                    B = b == null ? 0 : int.Parse(b)
                };
                rgbList.Add(rgb);
            }

            games.Add(new Bag
            {
                GameNumber = int.Parse(gameNumber),
                Hands = rgbList
            });
        }

        return games;
    }

    protected override int Solve(List<Bag> input)
    {
        var sum = 0;
        foreach (var bag in input)
        {
            var biggerRgb = GetBiggerRgb(bag.Hands);
            var factor = biggerRgb.R * biggerRgb.G * biggerRgb.B;
            sum += factor;
        }

        return sum;
    }

    private RGB GetBiggerRgb(List<RGB> rgbs)
    {
        var rgb = new RGB
        {
            R = rgbs.Max(x => x.R),
            G = rgbs.Max(x => x.G),
            B = rgbs.Max(x => x.B)
        };

        return rgb;
    }
}