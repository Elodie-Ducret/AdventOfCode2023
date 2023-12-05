using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day5;

namespace AdventOfCode2023.Problems.Day5;

public class Day5Part1Problem(string name, string path) : Problem<Almanac, double>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day5Part1Problem("Day 5 Part 1", "Day5/Day5.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day5Part1Problem("Day 5 Part 1 Test", "Day5/Day5_Part1_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override Almanac Convert(IEnumerable<string> input)
    {
        var singleString = string.Join("\n", input.ToArray());

        var splitOnSpaces = singleString.Split("\n\n");

        var seeds = splitOnSpaces[0].Split(": ")[1].Split(' ').Select(double.Parse).ToList();

        var almanac = new Almanac
        {
            Seeds = seeds,
            SeedToSoil = GetListOfMap(splitOnSpaces[1]),
            SoilToFertilizer = GetListOfMap(splitOnSpaces[2]),
            FertilizerToWater = GetListOfMap(splitOnSpaces[3]),
            WaterToLight = GetListOfMap(splitOnSpaces[4]),
            LightToTemperature = GetListOfMap(splitOnSpaces[5]),
            TemperatureToHumidity = GetListOfMap(splitOnSpaces[6]),
            HumidityToLocation = GetListOfMap(splitOnSpaces[7])
        };
        return almanac;
    }

    private List<Map> GetListOfMap(string input)
    {
        var list = new List<Map>();
        var lines = input.Split("\n");

        for (var i = 1; i < lines.Length; i++)
        {
            var line = lines[i].Split(" ");
            list.Add(new Map
            {
                DestinationRangeStart = double.Parse(line[0]),
                SourceRangeStart = double.Parse(line[1]),
                RangeLength = double.Parse(line[2])
            });
        }

        return list;
    }

    protected override double Solve(Almanac input)
    {
        var locations = input.Seeds.Select(seed => GetLocation(seed, input)).ToList();
        return locations.Min();
    }

    private double GetLocation(double seed, Almanac almanac)
    {
        var soil = GetCorrespondence(seed, almanac.SeedToSoil);
        var fertilizer = GetCorrespondence(soil, almanac.SoilToFertilizer);
        var water = GetCorrespondence(fertilizer, almanac.FertilizerToWater);
        var light = GetCorrespondence(water, almanac.WaterToLight);
        var temperature = GetCorrespondence(light, almanac.LightToTemperature);
        var humidity = GetCorrespondence(temperature, almanac.TemperatureToHumidity);
        var location = GetCorrespondence(humidity, almanac.HumidityToLocation);
        return location;
    }


    private double GetCorrespondence(double baseValue, List<Map> mapping)
    {
        var correspondenceMapping = mapping.FirstOrDefault(x =>
            x.SourceRangeStart <= baseValue && baseValue <= x.SourceRangeStart + x.RangeLength);
        if (correspondenceMapping == null) return baseValue;

        return correspondenceMapping.DestinationRangeStart + (baseValue - correspondenceMapping.SourceRangeStart);
    }
}