﻿namespace AdventOfCode2023.Schema.Day19.Part1;

public record Rating(Dictionary<char, int> Values)
{
    public long GetSumValues() => Values.Values.Sum();
}