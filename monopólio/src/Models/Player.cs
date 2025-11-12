namespace Monopólio.Models;

/// <summary>
/// Representa um jogador no jogo
/// </summary>
public class Player
{
    public string Name { get; set; }
    public decimal Money { get; set; }
    public int Position { get; set; }  // Posição na matriz 7x7 (índice linear 0-48)
    public bool IsInPrison { get; set; }
    public int TurnsInPrison { get; set; }
    public bool HasGetOutOfJailCard { get; set; }
    public List<Space> OwnedProperties { get; set; } = new();
    public List<string> History { get; set; } = new();  // Histórico de ações
    
    // Estatísticas
    public int GamesPlayed { get; set; } = 0;
    public int Wins { get; set; } = 0;
    public int Draws { get; set; } = 0;
    public int Losses { get; set; } = 0;

    public Player(string name, decimal initialMoney = 5000m)
    {
        Name = name;
        Money = initialMoney;
        Position = 0;  // Começa na Start
        IsInPrison = false;
        TurnsInPrison = 0;
        HasGetOutOfJailCard = false;
    }

    /// <summary>
    /// Adiciona um evento ao histórico do jogador
    /// </summary>
    public void AddHistoryEntry(string entry)
    {
        History.Add($"[{DateTime.Now:HH:mm:ss}] {entry}");
    }

    /// <summary>
    /// Verifica se o jogador pode comprar uma propriedade
    /// </summary>
    public bool CanAfford(decimal amount)
    {
        return Money >= amount;
    }

    /// <summary>
    /// Deduz dinheiro da conta do jogador
    /// </summary>
    public bool DeductMoney(decimal amount)
    {
        if (!CanAfford(amount))
            return false;
        
        Money -= amount;
        return true;
    }

    /// <summary>
    /// Adiciona dinheiro à conta do jogador
    /// </summary>
    public void AddMoney(decimal amount)
    {
        Money += amount;
    }

    /// <summary>
    /// Retorna uma string com as estatísticas
    /// </summary>
    public string GetStats()
    {
        return $"{Name} {GamesPlayed} {Wins} {Draws} {Losses}";
    }
}

