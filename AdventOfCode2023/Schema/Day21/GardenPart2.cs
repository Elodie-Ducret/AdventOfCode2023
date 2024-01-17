using AdventOfCode2023.Common;

namespace AdventOfCode2023.Schema.Day21;

public record GardenPart2(char[,] Tiles, Position Start, int RowCount, int ColumnCount)
{
    /* private List<(Position, long)> GetNewPositionsOut(Position start, long startCount)
     {
     }*/

    public long GetPanda(int totalCount)
    {
        // SaveGardenPlan();
        var dictionary = new Dictionary<Position, List<(Position, long)>>();
        var globalQueue = new Queue<(Position pos, long count)>();
        //var internalQueue = new Queue<(Position, long)>();
        globalQueue.Enqueue((Start, 0));
        long count = 0;

        while (globalQueue.TryDequeue(out var currentStep))
        {
            if (dictionary.TryGetValue(currentStep.pos, out List<(Position, long)>? internalCurrentStep))
            {
                count += internalCurrentStep.Count(x => x.Item2 == totalCount);
            }
            else
            {
                var internalQueue = new Queue<(Position pos, long count)>();
                internalQueue.Enqueue((Start, 0));

                var finalPositions = new List<(Position, long)>();
                var positions = new HashSet<(Position, long)>();

                while (internalQueue.TryDequeue(out var internalStep))
                {
                    if (internalStep.count <= totalCount)
                    {
                        var neighbors = GetNeighbors(internalStep.pos);
                        foreach (var neighbor in neighbors.inPostions)
                        {
                            positions.Add((neighbor, internalStep.count + 1 + currentStep.count));
                            if (!internalQueue.Contains((neighbor, internalStep.count + 1)))
                            {
                                internalQueue.Enqueue((neighbor, internalStep.count + 1));
                            }
                        }

                        foreach (var neighbor in neighbors.outPositions)
                        {
                            if (!globalQueue.Contains((neighbor, currentStep.count + internalStep.count + 1)) &&
                                currentStep.count + internalStep.count + 1 < totalCount)
                                globalQueue.Enqueue((neighbor, currentStep.count + internalStep.count + 1));
                            finalPositions.Add((neighbor, internalStep.count + 1));
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                if (!dictionary.TryAdd(currentStep.pos, finalPositions))
                {
                    dictionary[currentStep.pos].AddRange(finalPositions);
                }

                count += positions.Count(x => x.Item2 == totalCount);
            }
        }

        return count;
    }

    private (List<Position> inPostions, List<Position> outPositions) GetNeighbors(Position position)
    {
        var inPositions = new List<Position>();
        var outPositions = new List<Position>();
        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            var newPosition = position + DirectionPosition.GetPosition(direction);
            if (0 <= newPosition.Row && newPosition.Row < RowCount && 0 <= newPosition.Column &&
                newPosition.Column < ColumnCount)
            {
                if (Tiles[newPosition.Row, newPosition.Column] == '.' ||
                    Tiles[newPosition.Row, newPosition.Column] == 'S')
                {
                    inPositions.Add(newPosition);
                }
            }
            else
            {
                outPositions.Add(GetInPosition(newPosition));
            }
        }

        return (inPositions, outPositions);
    }

    private Position GetInPosition(Position outPosition)
    {
        if (outPosition.Row == -1) return new Position(RowCount - 1, outPosition.Column);
        if (outPosition.Row == RowCount) return new Position(0, outPosition.Column);
        if (outPosition.Column == -1) return new Position(outPosition.Row, ColumnCount - 1);
        if (outPosition.Column == ColumnCount) return new Position(outPosition.Row, 0);
        throw new Exception("Bad case");
    }

    private void SaveGardenPlan()
    {
        using (StreamWriter writer = new StreamWriter("C:\\Git\\AdventOfCode2023\\panda.txt"))
        {
            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    writer.Write(Tiles[row, column]);
                }

                writer.WriteLine();
            }
        }
    }
}