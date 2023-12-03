using System.Text.RegularExpressions;
using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day1;

namespace AdventOfCode2023.Problems.Day1;

public class Day1Part2Problem(string name, string path) : Problem<List<Calibration>, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day1Part2Problem("Day 1 Part 1", "Day1/Day1.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day1Part2Problem("Day 1 Part 1 Test", "Day1/Day1_Part2_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override List<Calibration> Convert(IEnumerable<string> input)
    {
        var regex = new Regex(@"(?=(nine|eight|seven|six|five|four|three|two|one))|\d", RegexOptions.Compiled);

        var line = 0;
        var calibrationList = new List<Calibration>();
        foreach (var calibrationInput in input)
        {
            var matches = regex.Matches(calibrationInput);

            var intList = new List<int>();
            foreach (Match match in matches)
            {
                var m = match.Groups[0].Value;
                if (m != string.Empty)
                {
                    var intResult = int.Parse(m);
                    intList.Add(intResult);
                }
                else
                {
                    var g = match.Groups[1].Value;
                    intList.Add(_numberWords[g]);
                }
            }

            calibrationList.Add(new Calibration() { Line = line, Numbers = intList });
            line++;
        }

        return calibrationList;
    }

    protected override int Solve(List<Calibration> input)
    {
        return input.Sum(calibration =>
            int.Parse(string.Concat(calibration.Numbers.First().ToString() + calibration.Numbers.Last().ToString())));
    }

    Dictionary<string, int> _numberWords = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
    };
}