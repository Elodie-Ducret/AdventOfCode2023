namespace AdventOfCode2023.Schema.Day5;

public class Almanac
{
    public List<double> Seeds { get; set; }
    public List<Map> SeedToSoil { get; set; }
    public List<Map> SoilToFertilizer { get; set; }
    public List<Map> FertilizerToWater { get; set; }
    public List<Map> WaterToLight { get; set; }
    public List<Map> LightToTemperature { get; set; }
    public List<Map> TemperatureToHumidity { get; set; }
    public List<Map> HumidityToLocation { get; set; }
    
}

public record Map
{
    public double DestinationRangeStart { get; set; }
    public double SourceRangeStart { get; set; }
    public double RangeLength { get; set; }
    
} 