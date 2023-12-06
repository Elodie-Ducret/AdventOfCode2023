namespace AdventOfCode2023.Schema.Day6;

public record Race(int Time, int Distance)
{
    public int Time = Time;
    public int Distance = Distance;
    
    public int GetDistanceTraveled(int pressDuration)
    {
        return (Time - pressDuration) * pressDuration;
    }

    public bool IsWin(int pressDuration)
    {
        return GetDistanceTraveled(pressDuration) > Distance;
    }
}