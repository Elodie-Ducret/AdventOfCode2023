namespace AdventOfCode2023.Schema.Day6;

public record Race(long Time, long Distance)
{
    public long Time = Time;
    public long Distance = Distance;
    
    public long GetDistanceTraveled(int pressDuration)
    {
        return (Time - pressDuration) * pressDuration;
    }

    public bool IsWin(int pressDuration)
    {
        return GetDistanceTraveled(pressDuration) > Distance;
    }
}