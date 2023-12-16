namespace AdventOfCode2023.Schema.Day15;

public record Box(int Id)
{
    public int Id = Id;
    private readonly List<SequencePart2> _sequences = new();

    private void AddSequence(SequencePart2 sequence)
    {
        var sequenceToReplace = _sequences.FirstOrDefault(x => x.Sequence == sequence.Sequence);
        if (sequenceToReplace == null)
        {
            _sequences.Add(sequence);
        }
        else
        {
            sequenceToReplace.FocalLength = sequence.FocalLength; 
        }
    }

    private void RemoveSequence(SequencePart2 sequence)
    {
        var sequenceToRemove = _sequences.FirstOrDefault(x => x.Sequence == sequence.Sequence); 
        if(sequenceToRemove != null) _sequences.Remove(sequenceToRemove);
    }

    public void AddOrRemoveSequence(SequencePart2 sequence)
    {
        if (sequence.ToRemove)
        {
            RemoveSequence(sequence);
        }
        else
        {
            AddSequence(sequence);
        }
    }

    public long GetBoxCount()
    {
        var sum = 0; 
        var sequenceNumber = 1; 
        foreach (var sequence in _sequences)
        {
            sum += (Id + 1) * sequenceNumber * sequence.FocalLength.Value;
            sequenceNumber++; 
        }

        return sum;
    }
}