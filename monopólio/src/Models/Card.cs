namespace Monopólio.Models;

/// <summary>
/// Representa uma carta (Chance ou Community)
/// </summary>
public class Card
{
    public string Description { get; set; }
    public string FullText { get; set; }  // Texto completo da carta
    public CardType CardType { get; set; }
    public double Probability { get; set; }  // Probabilidade de sair (0-1)
    public Action<Player, GameState>? Effect { get; set; }  // Função com o efeito

    public Card(
        string description,
        string fullText,
        CardType cardType,
        double probability)
    {
        Description = description;
        FullText = fullText;
        CardType = cardType;
        Probability = probability;
        Effect = null;
    }

    /// <summary>
    /// Aplica o efeito da carta a um jogador
    /// </summary>
    public void ApplyEffect(Player player, GameState gameState)
    {
        Effect?.Invoke(player, gameState);
    }
}

