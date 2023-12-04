using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day4;

namespace AdventOfCode2023.Problems.Day4;

public class Day4Part2Problem(string name, string path) : Problem<List<CardPart2>, int>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day4Part2Problem("Day 4 Part 1", "Day4/Day4.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day4Part2Problem("Day 4 Part 1 Test", "Day4/Day4_Part1_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override List<CardPart2> Convert(IEnumerable<string> input)
    {
        var cards = new List<CardPart2>();
        foreach (var card in input)
        {
            var p = card.Split(": ");
            var cardNumber = p[0].Split("Card ")[1];

            var numbers = p[1].Split(" | ");

            cards.Add(new CardPart2
            {
                CardNumber = int.Parse(cardNumber),
                WinningNumbers = GetNumbers(numbers[0]),
                Numbers = GetNumbers(numbers[1]),
                Count = 1
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

    protected override int Solve(List<CardPart2> input)
    {
        var sum = 0;

        foreach (var card in input)
        {
            for (var c = 0; c < card.Count; c++)
            {
                var count = card.Numbers.Count(x => card.WinningNumbers.Any(v => v == x));
                sum++;

                for (int i = 0; i < count; i++)
                {
                    var newCard = input.FirstOrDefault(x => x.CardNumber == card.CardNumber + i + 1);
                    if (newCard != null)
                    {
                        newCard.Count += 1;
                    }
                }
            }
        }
        
        return sum;
    }
}