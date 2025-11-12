namespace Monopólio.Repositories;

using Monopólio.Models;

/// <summary>
/// Interface para persistência de jogadores registados
/// </summary>
public interface IPlayerRepository
{
    /// <summary>
    /// Obtém um jogador pelo nome
    /// </summary>
    Player? GetPlayerByName(string name);

    /// <summary>
    /// Obtém todos os jogadores registados
    /// </summary>
    IEnumerable<Player> GetAllPlayers();

    /// <summary>
    /// Registra um novo jogador
    /// </summary>
    void RegisterPlayer(Player player);

    /// <summary>
    /// Atualiza as estatísticas de um jogador
    /// </summary>
    void UpdatePlayerStats(Player player);

    /// <summary>
    /// Remove um jogador do registo
    /// </summary>
    void RemovePlayer(string playerName);

    /// <summary>
    /// Verifica se um jogador existe
    /// </summary>
    bool PlayerExists(string playerName);

    /// <summary>
    /// Obtém o histórico de um jogador
    /// </summary>
    IEnumerable<string> GetPlayerHistory(string playerName);

    /// <summary>
    /// Adiciona uma entrada ao histórico do jogador
    /// </summary>
    void AddHistoryEntry(string playerName, string entry);
}
