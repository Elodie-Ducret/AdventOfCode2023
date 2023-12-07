namespace AdventOfCode2023.Schema.Day7;

public record Hand(List<int> Cards, int Bid) : IComparable<Hand>
{
    private const int Joker = 1;
    private const int BestCard = 14;

    public List<int> Cards = Cards;
    public int Bid = Bid;
    private Type? _handType;

    public Type GetBestType()
    {
        if (_handType != null) return _handType.Value;
        _handType = GetType();
        return _handType.Value;
    }

    public Type GetBestTypeWithJoker()
    {
        if (_handType != null) return _handType.Value;
        _handType = GetTypeWithJoker();
        return _handType.Value;
    }

    private new Type GetType()
    {
        var cardsDuplicateCount = GetCardDuplicateCount();
        return GetType(cardsDuplicateCount);
    }

    private new Type GetTypeWithJoker()
    {
        var cardsDuplicateCount = GetCardDuplicateCountWithJoker();
        return GetType(cardsDuplicateCount);
    }


    private Type GetType(Dictionary<int, int> cardsDuplicateCount)
    {
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

    private Dictionary<int, int> GetCardDuplicateCountWithJoker()
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

        var dictionaryGroups = groups.ToDictionary(g => g.Stuff, g => g.Count);

        if (!dictionaryGroups.ContainsKey(Joker)) return dictionaryGroups;
        
        if (dictionaryGroups.First(x => x.Key == Joker).Value == 5)
        {
            dictionaryGroups.Add(BestCard, 5); 
            dictionaryGroups.Remove(Joker);
            return dictionaryGroups;
        }
        
        var best = dictionaryGroups.Where(x => x.Key != Joker).MaxBy(x => x.Value);
        dictionaryGroups[best.Key] += dictionaryGroups.First(x => x.Key == Joker).Value;
        dictionaryGroups.Remove(Joker);
        return dictionaryGroups;
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

    public int CompareTo(Hand? other)
    {
        if (other == null) return 1;

        for (int i = 0; i < other.Cards.Count; i++)
        {
            if (Cards[i] != other.Cards[i])
            {
                return Cards[i].CompareTo(other.Cards[i]);
            }
        }

        return 1;
    }
}