namespace Monopólio.Repositories;

using Monopólio.Models;

/// <summary>
/// Interface para persistência do GameState corrente
/// </summary>
public interface IGameRepository
{
    /// <summary>
    /// Obtém o estado do jogo atual
    /// </summary>
    GameState? GetCurrentGame();

    /// <summary>
    /// Salva o estado do jogo
    /// </summary>
    void SaveGame(GameState gameState);

    /// <summary>
    /// Cria um novo jogo
    /// </summary>
    void CreateNewGame(GameState gameState);

    /// <summary>
    /// Limpa o jogo atual
    /// </summary>
    void ClearGame();

    /// <summary>
    /// Verifica se existe um jogo em progresso
    /// </summary>
    bool HasActiveGame();
}
