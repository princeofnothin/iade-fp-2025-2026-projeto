namespace Monopólio.Repositories;

using Monopólio.Models;

/// <summary>
/// Implementação in-memory do IPlayerRepository
/// </summary>
public class InMemoryPlayerRepository : IPlayerRepository
{
    private readonly Dictionary<string, Player> _players = new();
    private readonly Dictionary<string, List<string>> _playerHistories = new();

    public Player? GetPlayerByName(string name)
    {
        return _players.TryGetValue(name.ToLower(), out var player) ? player : null;
    }

    public IEnumerable<Player> GetAllPlayers()
    {
        return _players.Values;
    }

    public void RegisterPlayer(Player player)
    {
        string key = player.Name.ToLower();
        _players[key] = player;
        
        if (!_playerHistories.ContainsKey(key))
        {
            _playerHistories[key] = new List<string>();
        }
    }

    public void UpdatePlayerStats(Player player)
    {
        RegisterPlayer(player);
    }

    public void RemovePlayer(string playerName)
    {
        string key = playerName.ToLower();
        _players.Remove(key);
        _playerHistories.Remove(key);
    }

    public bool PlayerExists(string playerName)
    {
        return _players.ContainsKey(playerName.ToLower());
    }

    public IEnumerable<string> GetPlayerHistory(string playerName)
    {
        string key = playerName.ToLower();
        return _playerHistories.TryGetValue(key, out var history) ? history : new List<string>();
    }

    public void AddHistoryEntry(string playerName, string entry)
    {
        string key = playerName.ToLower();
        if (!_playerHistories.ContainsKey(key))
        {
            _playerHistories[key] = new List<string>();
        }
        _playerHistories[key].Add(entry);
    }
}
