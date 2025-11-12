namespace Monopólio.Models;

/// <summary>
/// Representa o estado global do jogo
/// </summary>
public class GameState
{
    public List<Player> Players { get; set; }
    public Board Board { get; set; }
    public int CurrentPlayerIndex { get; set; }
    public TurnState CurrentTurnState { get; set; }
    public Dice Dice { get; set; }
    public decimal FreeParkPot { get; set; }
    public bool IsGameRunning { get; set; }
    public int RoundNumber { get; set; }
    public List<Player> EliminatedPlayers { get; set; }
    public bool PlayerJustMovedWithDouble { get; set; } = false;

    public GameState()
    {
        Players = new List<Player>();
        Board = new Board();
        CurrentPlayerIndex = 0;
        CurrentTurnState = TurnState.RollingDice;
        Dice = new Dice();
        FreeParkPot = 0;
        IsGameRunning = false;
        RoundNumber = 1;
        EliminatedPlayers = new List<Player>();
    }

    /// <summary>
    /// Obtém o jogador atual
    /// </summary>
    public Player? GetCurrentPlayer()
    {
        if (CurrentPlayerIndex < 0 || CurrentPlayerIndex >= Players.Count)
            return null;

        return Players[CurrentPlayerIndex];
    }

    /// <summary>
    /// Avança para o próximo jogador
    /// </summary>
    public void NextPlayer()
    {
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
        if (CurrentPlayerIndex == 0)
            RoundNumber++;

        CurrentTurnState = TurnState.RollingDice;
        PlayerJustMovedWithDouble = false;
    }

    /// <summary>
    /// Adiciona um jogador ao jogo
    /// </summary>
    public void AddPlayer(Player player)
    {
        if (!Players.Contains(player))
        {
            Players.Add(player);
            player.Money = 5000m;  // Dinheiro inicial
        }
    }

    /// <summary>
    /// Remove um jogador do jogo (vai para lista de eliminados)
    /// </summary>
    public void EliminatePlayer(Player player)
    {
        if (Players.Remove(player))
        {
            EliminatedPlayers.Add(player);
            if (CurrentPlayerIndex >= Players.Count && Players.Count > 0)
                CurrentPlayerIndex = CurrentPlayerIndex % Players.Count;
        }
    }

    /// <summary>
    /// Adiciona dinheiro ao pote do Parque Gratuito
    /// </summary>
    public void AddToFreeParkPot(decimal amount)
    {
        FreeParkPot += amount;
    }

    /// <summary>
    /// Retira dinheiro do pote do Parque Gratuito
    /// </summary>
    public decimal RemoveFromFreeParkPot(decimal amount)
    {
        decimal removed = Math.Min(FreeParkPot, amount);
        FreeParkPot -= removed;
        return removed;
    }

    /// <summary>
    /// Verifica se o jogo tem um vencedor
    /// </summary>
    public bool HasWinner()
    {
        return Players.Count == 1 && IsGameRunning;
    }

    /// <summary>
    /// Obtém o jogador vencedor
    /// </summary>
    public Player? GetWinner()
    {
        return HasWinner() ? Players[0] : null;
    }
}
