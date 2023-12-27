namespace AdventOfCode2023.Schema.Day14;

public record Platform(char[,] _initialPlatform, int _rowCount, int _columnCount)
{
    private readonly char[,] _initialPlatform = _initialPlatform;
    private readonly int _rowCount = _rowCount;
    private readonly int _columnCount = _columnCount;


    public bool EqualsContent(Platform other)
    {
        if (_rowCount != other._rowCount || _columnCount != other._columnCount) return false;

        for (int row = 0; row < _rowCount; row++)
        {
            for (int column = 0; column < _columnCount; column++)
            {
                if (_initialPlatform[row, column] != other._initialPlatform[row, column]) return false;
            }
        }

        return true;
    }

    public Platform BuildNewPlatformAfterCycle()
    {
        var afterNorth = BuildNewPlatformNorth();
        var afterWest = afterNorth.BuildNewPlatformWest();
        var afterSouth = afterWest.BuildNewPlatformSouth();
        var afterEast = afterSouth.BuildNewPlatformEast();
        return afterEast;
    }

    private Platform BuildNewPlatformSouth()
    {
        var newPlatform = new char[_rowCount, _columnCount];


        for (int column = 0; column < _columnCount; column++)
        {
            var newMaxPosition = _columnCount - 1;
            for (int row = _rowCount - 1; row >= 0; row--)
            {
                switch (_initialPlatform[row, column])
                {
                    case '.':
                        newPlatform[row, column] = '.';
                        break;
                    case 'O':
                        newPlatform[newMaxPosition, column] = 'O';
                        if (newMaxPosition != row)
                        {
                            newPlatform[row, column] = '.';
                        }

                        newMaxPosition--;
                        break;
                    case '#':
                        newMaxPosition = row - 1;
                        newPlatform[row, column] = '#';
                        break;
                }
            }
        }

        return new Platform(newPlatform, _rowCount, _columnCount);
    }

    private Platform BuildNewPlatformNorth()
    {
        var newPlatform = new char[_rowCount, _columnCount];

        for (int column = 0; column < _columnCount; column++)
        {
            var newMaxPosition = 0;
            for (int row = 0; row < _rowCount; row++)
            {
                switch (_initialPlatform[row, column])
                {
                    case '.':
                        newPlatform[row, column] = '.';
                        break;
                    case 'O':
                        newPlatform[newMaxPosition, column] = 'O';
                        if (newMaxPosition != row)
                        {
                            newPlatform[row, column] = '.';
                        }

                        newMaxPosition++;
                        break;
                    case '#':
                        newMaxPosition = row + 1;
                        newPlatform[row, column] = '#';
                        break;
                }
            }
        }

        return new Platform(newPlatform, _rowCount, _columnCount);
    }


    private Platform BuildNewPlatformWest()
    {
        var newPlatform = new char[_rowCount, _columnCount];
        for (int row = 0; row < _rowCount; row++)
        {
            var newMaxPosition = 0;
            for (int column = 0; column < _columnCount; column++)
            {
                switch (_initialPlatform[row, column])
                {
                    case '.':
                        newPlatform[row, column] = '.';
                        break;
                    case 'O':
                        newPlatform[row, newMaxPosition] = 'O';
                        if (newMaxPosition != column)
                        {
                            newPlatform[row, column] = '.';
                        }

                        newMaxPosition++;
                        break;
                    case '#':
                        newMaxPosition = column + 1;
                        newPlatform[row, column] = '#';
                        break;
                }
            }
        }

        return new Platform(newPlatform, _rowCount, _columnCount);
    }

    private Platform BuildNewPlatformEast()
    {
        var newPlatform = new char[_rowCount, _columnCount];
        for (int row = 0; row < _rowCount; row++)
        {
            var newMaxPosition = _rowCount - 1;
            for (int column = _columnCount - 1; column >= 0; column--)
            {
                switch (_initialPlatform[row, column])
                {
                    case '.':
                        newPlatform[row, column] = '.';
                        break;
                    case 'O':
                        newPlatform[row, newMaxPosition] = 'O';
                        if (newMaxPosition != column)
                        {
                            newPlatform[row, column] = '.';
                        }

                        newMaxPosition--;
                        break;
                    case '#':
                        newMaxPosition = column - 1;
                        newPlatform[row, column] = '#';
                        break;
                }
            }
        }

        return new Platform(newPlatform, _rowCount, _columnCount);
    }

    public void DisplayPlatform()
    {
        Console.WriteLine("----------------------------");
        for (int row = 0; row < _rowCount; row++)
        {
            for (int column = 0; column < _columnCount; column++)
            {
                Console.Write(_initialPlatform[row, column]);
            }

            Console.WriteLine();
        }

        Console.WriteLine("----------------------------");
    }

    public long GetCountPart1()
    {
        var sum = 0;
        for (int column = 0; column < _columnCount; column++)
        {
            var newMaxPosition = 0;
            for (int row = 0; row < _rowCount; row++)
            {
                switch (_initialPlatform[row, column])
                {
                    case 'O':
                        sum += _columnCount - newMaxPosition;
                        newMaxPosition++;
                        break;
                    case '#':
                        newMaxPosition = row + 1;
                        break;
                }
            }
        }

        return sum;
    }


    public long GetCountPart2()
    {
        var sum = 0;
        for (int column = 0; column < _columnCount; column++)
        {
            for (int row = 0; row < _rowCount; row++)
            {
                switch (_initialPlatform[row, column])
                {
                    case 'O':
                        sum += _columnCount - row;
                        break;
                    case '#':
                        break;
                }
            }
        }

        return sum;
    }
}