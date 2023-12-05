﻿namespace AdventOfCode2023.Schema.Day5;

public class Almanac
{
    public IEnumerable<long> Seeds { get; set; }
    public List<Map> SeedToSoil { get; set; }
    public List<Map> SoilToFertilizer { get; set; }
    public List<Map> FertilizerToWater { get; set; }
    public List<Map> WaterToLight { get; set; }
    public List<Map> LightToTemperature { get; set; }
    public List<Map> TemperatureToHumidity { get; set; }
    public List<Map> HumidityToLocation { get; set; }
}