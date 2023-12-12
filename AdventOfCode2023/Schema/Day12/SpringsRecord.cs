using System.Text.RegularExpressions;

namespace AdventOfCode2023.Schema.Day12;

public record SpringsRecord(List<char> Springs, List<int> Conditions)
{
    public List<char> Springs = Springs;
    public List<int> Conditions = Conditions;


    private List<List<char>> Possibilities = new();

    private readonly Regex _regex = new Regex(@"(?:\#+)|\.+");


    public long GetArrangementValidCount()
    {
        GetAllPossibilities(Springs);
        return Possibilities.Count; 
    }


    private void GetAllPossibilities(List<char> inputString)
    {
        if (inputString.Contains('?'))
        {
            List<char> newList = [..inputString];
            var index = newList.IndexOf('?');
            newList[index] = '#';
            GetAllPossibilities(newList);
            newList[index] = '.';
            GetAllPossibilities(newList);
        }
        else
        {
            if (IsValid(inputString))
            {
                Possibilities.Add(inputString);
            }
        }
    }

    private bool IsValid(List<char> input)
    {
        var list = new List<int>();
        var matches = _regex.Matches(string.Join("", input));
        foreach (Match match in matches)
        {
            var m = match.Groups[0].Value;
            if(m[0] == '#') list.Add(m.Length);
        }

        if (list.Count != Conditions.Count) return false;
        return !list.Where((t, i) => t != Conditions[i]).Any();
    }
}