using AdventOfCode2023.Common;
using AdventOfCode2023.Schema.Day5;

namespace AdventOfCode2023.Problems.Day5;

public class Day5Part2Problem(string name, string path) : Problem<AlmanacRange, long>(name, path)
{
    public static void Run()
    {
        var day1Problem = new Day5Part2Problem("Day 5 Part 2", "Day5/Day5.txt");
        var response = day1Problem.SolveProblem();
    }

    public static void RunTest()
    {
        var day1Problem = new Day5Part2Problem("Day 5 Part 2 Test", "Day5/Day5_Part1_Test.txt");
        var response = day1Problem.SolveProblem();
    }

    protected override AlmanacRange Convert(IEnumerable<string> input)
    {
        var singleString = string.Join("\n", input.ToArray());

        var splitOnSpaces = singleString.Split("\n\n");

        var seeds = new List<Long2>();

        var seedsSplit = splitOnSpaces[0].Split(": ")[1].Split(' ').Select(long.Parse).ToList();
        for (var i = 0; i < seedsSplit.Count; i += 2)
        {
            seeds.Add(new Long2(Start: seedsSplit[i], End: seedsSplit[i] + seedsSplit[i + 1]));
        }

        var almanac = new AlmanacRange
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
                DestinationRangeStart = long.Parse(line[0]),
                SourceRangeStart = long.Parse(line[1]),
                RangeLength = long.Parse(line[2])
            });
        }

        return list;
    }

    protected override long Solve(AlmanacRange input)
    {
        var locations = input.Seeds.Select(seed => GetMinimumLocation(seed, input)).ToList();
        return locations.Min();
    }

    private long GetMinimumLocation(Long2 seedRange, AlmanacRange almanac)
    {
        var soilRange = GetCorrespondenceList(new List<Long2>() { seedRange }, almanac.SeedToSoil);
        var fertilizerRange = GetCorrespondenceList(soilRange, almanac.SoilToFertilizer);
        var waterRange = GetCorrespondenceList(fertilizerRange, almanac.FertilizerToWater);
        var lightRange = GetCorrespondenceList(waterRange, almanac.WaterToLight);
        var temperatureRange = GetCorrespondenceList(lightRange, almanac.LightToTemperature);
        var humidityRange = GetCorrespondenceList(temperatureRange, almanac.TemperatureToHumidity);
        var locationRange = GetCorrespondenceList(humidityRange, almanac.HumidityToLocation);

        return locationRange.Select(x => x.Start).Min();
    }


    private List<Long2> GetCorrespondenceList(List<Long2> baseRange, List<Map> mapping)
    {
        var list = new List<Long2>();

        foreach (var baseValue in baseRange)
        {
            var e = baseValue;
            do
            {
                var partialCorrespondence = GetPartialCorrespondence(e, mapping);
                list.Add(partialCorrespondence.response);
                e = partialCorrespondence.newRange;
            } while (e != null && e.Start < e.End);
        }

        return list;
    }

    private (Long2? newRange, Long2 response) GetPartialCorrespondence(Long2 values, List<Map> mapping)
    {
        var correspondenceMapping =
            mapping.FirstOrDefault(x => x.SourceRangeStart <= values.Start && values.Start < x.SourceEnd);

        if (correspondenceMapping == null)
        {
            var nextMapping = mapping.Where(x => values.Start < x.SourceRangeStart).MinBy(x => x.SourceRangeStart);

            if (nextMapping == null) return (null, new Long2(values.Start, values.End));
            return (new Long2(nextMapping.SourceRangeStart, values.End),
                new Long2(values.Start, nextMapping.SourceRangeStart - 1));
        }

        if (values.End <= correspondenceMapping.SourceEnd)
        {
            return (null,
                new Long2(correspondenceMapping.GetDestinationValue(values.Start),
                    correspondenceMapping.GetDestinationValue(values.End)));
        }

        return (new Long2(correspondenceMapping.SourceEnd, values.End),
            new Long2(correspondenceMapping.GetDestinationValue(values.Start),
                correspondenceMapping.DestinationEnd));
    }
}