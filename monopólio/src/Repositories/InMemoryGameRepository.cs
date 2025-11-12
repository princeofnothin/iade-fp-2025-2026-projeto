namespace Monopólio.Repositories;

using Monopólio.Models;

/// <summary>
/// Implementação in-memory do IGameRepository
/// </summary>
public class InMemoryGameRepository : IGameRepository
{
    private GameState? _currentGame;

    public GameState? GetCurrentGame()
    {
        return _currentGame;
    }

    public void SaveGame(GameState gameState)
    {
        _currentGame = gameState;
    }

    public void CreateNewGame(GameState gameState)
    {
        _currentGame = gameState;
    }

    public void ClearGame()
    {
        _currentGame = null;
    }

    public bool HasActiveGame()
    {
        return _currentGame != null && _currentGame.IsGameRunning;
    }
}
