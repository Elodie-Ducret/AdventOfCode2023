using System.Transactions;

namespace AdventOfCode2023.Schema.Day9;

public record History(List<int> Values)
{
    public List<int> Values = Values;
    private List<List<int>>? _pyramid;

    public int GetNext()
    {
        _pyramid ??= BuildPyramid();

        _pyramid.Last().Add(0);
        for (var i = _pyramid.Count - 2; i >= 0; i--)
        {
            _pyramid[i].Add(_pyramid[i + 1].Last() + _pyramid[i].Last());
        }

        return _pyramid.First().Last();
    }

    public int GetBefore()
    {
        _pyramid ??= BuildPyramid();

        _pyramid.Last().Insert(0, 0);

        for (var i = _pyramid.Count - 2; i >= 0; i--)
        {
            _pyramid[i].Insert(0, _pyramid[i].First() - _pyramid[i + 1].First() );
        }

        return _pyramid.First().First();
    }

    private List<List<int>> BuildPyramid()
    {
        var pyramid = new List<List<int>> { Values };
        var precedent = pyramid[0];
        while (precedent.Any(x => x != 0))
        {
            var list = new List<int>();
            for (var i = 1; i < precedent.Count; i++)
            {
                list.Add(precedent[i] - precedent[i - 1]);
            }

            pyramid.Add(list);
            precedent = pyramid.Last();
        }

        return pyramid;
    }
}