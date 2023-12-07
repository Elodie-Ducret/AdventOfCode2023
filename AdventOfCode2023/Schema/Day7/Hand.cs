using AdventOfCode2023.Schema.Day4;

namespace AdventOfCode2023.Schema.Day7;

public record Hand(List<int> Cards, int Bid) : IComparable
{
    public List<int> Cards = Cards;
    public int Bid = Bid;
    private Type? _handType;

    public Type GetBestType()
    {
        if (_handType != null) return _handType.Value;
        _handType = GetType();
        return _handType.Value;
    }


    private new Type GetType()
    {
        var cardsDuplicateCount = GetCardDuplicateCount();

        if (cardsDuplicateCount.Count == 1) return Type.FiveOfAKind;
        if (cardsDuplicateCount.Count(x => x.Value == 4) == 1) return Type.FourOfAKind;
        if (cardsDuplicateCount.Count(x => x.Value == 3) == 1)
        {
            return cardsDuplicateCount.Count(x => x.Value == 2) == 1
                ? Type.FullHouse
                : Type.ThreeOfAKind;
        }

        if (cardsDuplicateCount.Count(x => x.Value == 2) == 2) return Type.TwoPair;
        if (cardsDuplicateCount.Count(x => x.Value == 2) == 1) return Type.OnePair;
        return Type.HighCard;
    }

    public int CompareTo(object? obj)
    {
        if (obj is not Hand handToCompare) return 1;
       
        for (int i = 0; i < handToCompare.Cards.Count; i++)
        {
            if (Cards[i] != handToCompare.Cards[i])
            {
                return Cards[i].CompareTo(handToCompare.Cards[i]); 
            }
        }

        return 1; 
    }


    private Dictionary<int, int> GetCardDuplicateCount()
    {
        var groups =
            from s in Cards
            group s by s
            into g
            select new
            {
                Stuff = g.Key,
                Count = g.Count()
            };
        return groups.ToDictionary(g => g.Stuff, g => g.Count);
    }

    public int GetCard(int index)
    {
        return Cards[index];
    }
}