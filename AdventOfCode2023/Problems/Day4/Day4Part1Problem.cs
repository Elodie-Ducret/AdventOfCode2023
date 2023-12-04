using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day4;

namespace AdventOfCode2023.Problems.Day4;

public class Day4Part1Problem(string name, string path) : Problem<List<Card>, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day4Part1Problem("Day 4 Part 1", "Day4/Day4.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day4Part1Problem("Day 4 Part 1 Test", "Day4/Day4_Part1_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override List<Card> Convert(IEnumerable<string> input)
    {
        var cards = new List<Card>();
        foreach (var card in input)
        {
            var p = card.Split(": ");
            var cardNumber = p[0].Split("Card ")[1];

            var numbers = p[1].Split(" | ");

            cards.Add(new Card
            {
                CardNumber = int.Parse(cardNumber),
                WinningNumbers = GetNumbers(numbers[0]),
                Numbers = GetNumbers(numbers[1])
            });
        }

        return cards;
    }

    private List<int> GetNumbers(string input)
    {
        var numbersSplit = input.Split(' ');

        var numbers = new List<int>();
        foreach (var number in numbersSplit)
        {
            var numberTrim = number.Trim();
            if (numberTrim == string.Empty) continue;
            numbers.Add(int.Parse(numberTrim));
        }

        return numbers;
    }

    protected override int Solve(List<Card> input)
    {
        var sum = 0;

        foreach (var card in input)
        {
            var count = card.Numbers.Count(x => card.WinningNumbers.Any(v => v == x));

            if (count > 0)
            {
                var e = 1;
                for (int i = 1; i < count; i++)
                {
                    e *= 2;
                }

                sum += e; 
            }
        }


        return sum;
    }
}