namespace AdventOfCode2023.Schema.Day20;

public record ModulesPart2(Dictionary<string, BaseModule> ModulesByNames)
{
    private long _lowPulseCount = 0;
    private long _highPulseCount = 0;


    public long MultiplePressButton()
    {
        var count = 0;
        var isOk = false;
        do
        {
            isOk = PressButton();
            count++;
            Console.WriteLine($"Test part 1 = {count}");
        } while (isOk == false);


        return count;
    }

    private bool PressButton()
    {
        var queue = new Queue<(BaseModule sender, bool pulse)>();
        queue.Enqueue((ModulesByNames["broadcaster"], false));
        _lowPulseCount++;

        while (queue.TryDequeue(out var element))
        {
            foreach (var module in element.sender.Output)
            {
                //WriteTrace(element.sender.Name, element.pulse, module.Name);
                if (element.pulse) _highPulseCount++;
                else _lowPulseCount++;
                var pulse = module.OnPulse(element.sender, element.pulse);
                if (element.sender.Name == "rx" && element.pulse == false)
                {
                    return true;
                    
                }
                if (pulse != null)
                {
                    queue.Enqueue((module, pulse.Value));
                }
            }
        }

        return false;
    }

    private void WriteTrace(string name, bool pulse, string nameTo)
    {
        var lowOrHigh = pulse ? "high" : "low";
        Console.WriteLine($"{name} -{lowOrHigh}-> {nameTo}");
    }
}