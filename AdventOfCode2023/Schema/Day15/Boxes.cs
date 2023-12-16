namespace AdventOfCode2023.Schema.Day15;

public class Boxes
{
    private readonly Dictionary<int, Box> _boxes = new();

    public Boxes(List<SequencePart2> sequences)
    {
        foreach (var sequence in sequences)
        {
            var boxId = sequence.GetBoxId();
            if (_boxes.TryGetValue(boxId, out var value))
            {
                value.AddOrRemoveSequence(sequence);
            }
            else
            {
                _boxes.Add(boxId, new Box(boxId));
                _boxes[boxId].AddOrRemoveSequence(sequence);
            }
        }
    }


    public long GetCount()
    {
        return _boxes.Sum(box => box.Value.GetBoxCount());
    }
}