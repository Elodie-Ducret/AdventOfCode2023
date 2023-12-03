using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day2;

namespace AdventOfCode2023.Problems.Day2;

public class Day2Part1Problem(string name, string path) : Problem<List<Bag>, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day2Part1Problem("Day 2 Part 1", "Day2/Day2.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day2Part1Problem("Day 2 Part 1 Test", "Day2/Day2_Part1_Test.txt");
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

    //The Elf would first like to know which games would have been possible if the bag contained only 12 red cubes, 13 green cubes, and 14 blue cubes?
    protected override int Solve(List<Bag> input)
    {
        var sum = 0; 
        foreach (var bag in input)
        {
            if (IsHandsValid(bag.Hands))
            {
                sum += bag.GameNumber; 
            }
        }

        return sum;
    }

    private bool IsHandsValid(List<RGB> rgbs)
    {
        foreach (var rgb in rgbs)
        {
            if (rgb.R > 12) return false;
            if (rgb.G > 13) return false;
            if (rgb.B > 14) return false;
        }
        return true; 
    }
}