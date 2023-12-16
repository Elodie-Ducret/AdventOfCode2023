namespace AdventOfCode2023.Schema.Day10;

public class Graph(Grid grid, HashSet<Position> path)
{
    private readonly Dictionary<Position, Position> _connectedComponents = new();


    public List<List<Position>> GetAllGroups()
    {
        for (int row = 0; row < grid.RowCount; row++)
        {
            for (int column = 0; column < grid.ColumnCount; column++)
            {
                var startingPosition = new Position(row, column);
                Explore(startingPosition, startingPosition);
            }
        }

        return _connectedComponents.GroupBy(x => x.Key).Select(c => c.Select(v => v.Value).ToList()).ToList();
    }


    private void Explore(Position startingPosition, Position currentPosition)
    {
        if (!_connectedComponents.TryAdd(currentPosition, startingPosition))
        {
            return;
        }

        var neighbors = GetNeighbors(currentPosition);
        foreach (var neighbor in neighbors)
        {
            Explore(startingPosition, neighbor);
        }
    }


    private List<Position> GetNeighbors(Position position)
    {
        /*  if (path.Contains(position)) return [];
        var neighbors = grid.GetNeighbors(position);
        return neighbors.Where(x => !path.Contains(x)).ToList();*/

        var list = new List<Position>();

      
        var topPosition = new Position(position.Row - 1, position.Column);


        
        return list;
    }

    private bool GetPosition(Position currentPosition, Position nextPosition)
    {
        if (!IsValidPosition(nextPosition)) return false;

        return false; 


    }


    private bool IsValidPosition(Position position)
    {
        return position.Column >= 0 && position.Column < grid.ColumnCount && position.Row >= 0 &&
               position.Row < grid.RowCount;
    }

    /*  

      public List<Position> GetNeighbors(Position position)
      {
          var potentialPositions = new []
          {
              new Position(position.Row - 1, position.Column),
              new Position(position.Row + 1, position.Column),
              new Position(position.Row, position.Column + 1),
              new Position(position.Row, position.Column - 1)
          };

          return GetOnlyValidPositions(potentialPositions).ToList();

      }*/
}