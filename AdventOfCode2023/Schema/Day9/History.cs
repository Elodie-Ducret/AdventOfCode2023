using System.Transactions;

namespace AdventOfCode2023.Schema.Day9;

public record History(List<int> Values)
{
    public List<int> Values = Values;
    private List<List<int>>? _pyramid;

    public int GetNext()
    {
        _pyramid = new List<List<int>> { Values };
        var precedent = _pyramid[0];
        while (precedent.Any(x => x != 0))
        {
            var list = new List<int>();
            for (int i = 1; i < precedent.Count; i++)
            {
                list.Add(precedent[i] - precedent[i - 1]);
            }

            _pyramid.Add(list);
            precedent = _pyramid.Last();
        }
        
        _pyramid.Last().Add(0);
        for (var i = _pyramid.Count-2; i >= 0; i--)
        {
            _pyramid[i].Add(_pyramid[i+1].Last() + _pyramid[i].Last());
        }

        return _pyramid.First().Last();
    }
}