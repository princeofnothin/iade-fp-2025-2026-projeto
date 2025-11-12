namespace Monopólio.Models;

/// <summary>
/// Representa o tabuleiro 7x7 do Monopoly
/// </summary>
public class Board
{
    public int Width { get; } = 7;
    public int Height { get; } = 7;
    
    private Space[,] _grid;
    private Dictionary<string, (int row, int col)> _spaceNameToCoordinates;
    private Dictionary<(int row, int col), Space> _coordinatesToSpace;

    public Board()
    {
        _grid = new Space[Height, Width];
        _spaceNameToCoordinates = new Dictionary<string, (int, int)>();
        _coordinatesToSpace = new Dictionary<(int, int), Space>();
    }

    /// <summary>
    /// Registra um espaço no tabuleiro
    /// </summary>
    public void RegisterSpace(Space space, int row, int col)
    {
        if (row < 0 || row >= Height || col < 0 || col >= Width)
            throw new ArgumentException("Posição fora dos limites do tabuleiro");

        space.Row = row;
        space.Column = col;
        _grid[row, col] = space;
        _spaceNameToCoordinates[space.Name] = (row, col);
        _coordinatesToSpace[(row, col)] = space;
    }

    /// <summary>
    /// Obtém um espaço pelo nome
    /// </summary>
    public Space? GetSpaceByName(string name)
    {
        if (_spaceNameToCoordinates.TryGetValue(name, out var coords))
        {
            return GetSpaceAtPosition(coords.row, coords.col);
        }
        return null;
    }

    /// <summary>
    /// Obtém um espaço pela posição na matriz
    /// </summary>
    public Space? GetSpaceAtPosition(int row, int col)
    {
        if (row < 0 || row >= Height || col < 0 || col >= Width)
            return null;

        return _grid[row, col];
    }

    /// <summary>
    /// Obtém um espaço pelo índice linear (0-48)
    /// </summary>
    public Space? GetSpaceByLinearIndex(int index)
    {
        if (index < 0 || index >= Width * Height)
            return null;

        int row = index / Width;
        int col = index % Width;
        return GetSpaceAtPosition(row, col);
    }

    /// <summary>
    /// Converte índice linear para coordenadas (row, col)
    /// </summary>
    public (int row, int col) LinearIndexToCoordinates(int index)
    {
        return (index / Width, index % Width);
    }

    /// <summary>
    /// Converte coordenadas (row, col) para índice linear
    /// </summary>
    public int CoordinatesToLinearIndex(int row, int col)
    {
        return row * Width + col;
    }

    /// <summary>
    /// Obtém todos os espaços do tabuleiro
    /// </summary>
    public IEnumerable<Space> GetAllSpaces()
    {
        for (int row = 0; row < Height; row++)
        {
            for (int col = 0; col < Width; col++)
            {
                if (_grid[row, col] != null)
                    yield return _grid[row, col];
            }
        }
    }

    /// <summary>
    /// Obtém a matriz de espaços
    /// </summary>
    public Space[,] GetGrid()
    {
        return _grid;
    }
}
