namespace AdventOfCode2023.Schema.Day20;

public record Modules(Dictionary<string, BaseModule> ModulesByNames)
{
    private long _lowPulseCount = 0;
    private long _highPulseCount = 0;


    public long MultiplePressButton()
    {
        var count = 0;
        do
        {
            PressButton();
            count++;
        } while (count < 1000);


        return _lowPulseCount * _highPulseCount;
    }

    private void PressButton()
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
                if (pulse != null)
                {
                    queue.Enqueue((module, pulse.Value));
                }
            }
        }
    }

    private void WriteTrace(string name, bool pulse, string nameTo)
    {
        var lowOrHigh = pulse ? "high" : "low";
        Console.WriteLine($"{name} -{lowOrHigh}-> {nameTo}");
    }
}