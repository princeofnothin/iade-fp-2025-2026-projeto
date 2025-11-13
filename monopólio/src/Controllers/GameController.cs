namespace Monopólio.Controllers;

using Monopólio.Models;
using Monopólio.Services;
using Monopólio.Views.Console;
using Monopólio.Repositories;
using Monopólio.Rules;

/// <summary>
/// Controller para operações de jogo: IJ, DJ, LD, TT
/// </summary>
public class GameController
{
    private readonly IGameRepository _gameRepository;
    private readonly IPlayerRepository _playerRepository;

    public GameController(
        IGameRepository gameRepository,
        IPlayerRepository playerRepository)
    {
        _gameRepository = gameRepository;
        _playerRepository = playerRepository;
    }

    /// <summary>
    /// IJ NomeJogadorA NomeJogadorB ... - Iniciar Jogo
    /// </summary>
    public bool InitializeGame(List<string> playerNames)
    {
        if (_gameRepository.HasActiveGame())
        {
            System.Console.WriteLine("Existe um jogo em curso.");
            return true;
        }

        if (playerNames.Count < 2 || playerNames.Count > 5)
        {
            System.Console.WriteLine("Instrução inválida.");
            return true;
        }

        // Verificar se todos os jogadores existem
        var players = new List<Player>();
        foreach (var name in playerNames)
        {
            var player = _playerRepository.GetPlayerByName(name);
            if (player == null)
            {
                System.Console.WriteLine("Jogador inexistente.");
                return true;
            }
            players.Add(new Player(name));  // Criar nova instância para este jogo
        }

        // Criar o jogo
        var gameState = new GameState();
        gameState.Board = BoardLayout.CreateBoard();

        foreach (var player in players)
        {
            player.Money = 5000m;  // Dinheiro inicial
            player.Position = gameState.Board.CoordinatesToLinearIndex(3, 3);  // Start
            player.IsInPrison = false;
            player.TurnsInPrison = 0;
            player.OwnedProperties.Clear();
            gameState.AddPlayer(player);
        }

        gameState.IsGameRunning = true;
        gameState.RoundNumber = 1;
        gameState.CurrentPlayerIndex = 0;
        gameState.FreeParkPot = 0;

        _gameRepository.CreateNewGame(gameState);

        System.Console.WriteLine("Jogo iniciado com sucesso.");
        return true;
    }

    /// <summary>
    /// DJ - Mostrar estado do jogo
    /// </summary>
    public bool DisplayGame()
    {
        var gameState = _gameRepository.GetCurrentGame();
        if (gameState == null || !gameState.IsGameRunning)
        {
            System.Console.WriteLine("Não existe um jogo em curso.");
            return true;
        }

        // Exibir tabuleiro 7x7
        var grid = gameState.Board.GetGrid();
        for (int row = 0; row < 7; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                var space = grid[row, col];
                if (space != null)
                {
                    // Mostrar se há um jogador aqui
                    int index = row * 7 + col;
                    var playerHere = gameState.Players.FirstOrDefault(p => p.Position == index);
                    if (playerHere != null)
                    {
                        System.Console.Write(playerHere.Name.Substring(0, Math.Min(3, playerHere.Name.Length)).PadRight(3));
                    }
                    else
                    {
                        System.Console.Write(space.Name.Substring(0, Math.Min(3, space.Name.Length)).PadRight(3));
                    }
                }
                else
                {
                    System.Console.Write("   ");
                }
                System.Console.Write(" ");
            }
            System.Console.WriteLine();
        }

        // Exibir jogador da vez e dinheiro
        var currentPlayer = gameState.GetCurrentPlayer();
        if (currentPlayer != null)
        {
            System.Console.WriteLine();
            System.Console.WriteLine($"{currentPlayer.Name} - {currentPlayer.Money}€");
        }

        return true;
    }

    /// <summary>
    /// LD NomeJogador - Lançar dados
    /// </summary>
    public bool LaunchDice(string playerName)
    {
        var gameState = _gameRepository.GetCurrentGame();
        if (gameState == null || !gameState.IsGameRunning)
        {
            System.Console.WriteLine("Não existe um jogo em curso.");
            return true;
        }

        var player = gameState.Players.FirstOrDefault(p => p.Name == playerName);
        if (player == null)
        {
            System.Console.WriteLine("Jogador não participa no jogo em curso.");
            return true;
        }

        if (gameState.GetCurrentPlayer() != player)
        {
            System.Console.WriteLine("Não é a vez do jogador.");
            return true;
        }

        // Lançar dados
        gameState.Dice.Roll();
        int movement = gameState.Dice.Total;

        // Processar movimento (circular 0..48)
        int newPos = player.Position + movement;
        newPos %= 49;
        if (newPos < 0) newPos += 49;
        player.Position = newPos;

        // Obter o espaço onde pousou
        var space = gameState.Board.GetSpaceByLinearIndex(player.Position);
        string spaceName = space?.Name ?? "Desconhecido";

        // Exibir resultado
        System.Console.WriteLine($"Saiu {gameState.Dice} – espaço {spaceName}.");

        // Se saiu duplo, tratar duplos
        if (gameState.Dice.IsDouble)
        {
            // Se for o segundo duplo consecutivo do mesmo jogador -> enviar para prisão
            if (gameState.Dice.DoubleCount >= 2)
            {
                System.Console.WriteLine("Dois duplos consecutivos — jogador enviado para a prisão.");
                var prison = gameState.Board.GetAllSpaces().FirstOrDefault(s => s.Type == SpaceType.Prison);
                if (prison != null)
                {
                    player.Position = gameState.Board.CoordinatesToLinearIndex(prison.Row, prison.Column);
                }
                player.IsInPrison = true;
                player.TurnsInPrison = 0;
                _gameRepository.SaveGame(gameState);
                return true;
            }
            else
            {
                System.Console.WriteLine("Saiu duplo — pode jogar de novo.");
                // Processar efeitos do espaço normalmente e permitir novo lançamento (não termina o turno)
            }
        }
        else
        {
            // Reset contador de duplos quando não sai duplo
            gameState.Dice.ResetDoubleCount();
        }

        // Processar efeitos do espaço
        if (space != null)
        {
            ProcessSpaceEffect(player, space, gameState);
        }

        // Atualizar o jogo
        _gameRepository.SaveGame(gameState);
        return true;
    }

    /// <summary>
    /// Processa o efeito do espaço
    /// </summary>
    private void ProcessSpaceEffect(Player player, Space space, GameState gameState)
    {
        switch (space.Type)
        {
            case SpaceType.Start:
                System.Console.WriteLine("Peça colocada no espaço Start.");
                break;

            case SpaceType.Property:
                if (space.Owner == null)
                {
                    System.Console.WriteLine("Espaço sem dono.");
                }
                else if (space.Owner == player)
                {
                    System.Console.WriteLine("Espaço já comprado.");
                }
                else
                {
                    System.Console.WriteLine("Espaço já comprado por outro jogador. Necessário pagar renda.");
                }
                break;

            case SpaceType.Police:
                System.Console.WriteLine("Jogador preso.");
                player.IsInPrison = true;
                player.TurnsInPrison = 0;
                break;

            case SpaceType.Prison:
                System.Console.WriteLine("Jogador só de passagem.");
                break;

            case SpaceType.FreePark:
                if (gameState.FreeParkPot > 0)
                {
                    decimal amount = gameState.RemoveFromFreeParkPot(gameState.FreeParkPot);
                    player.AddMoney(amount);
                    System.Console.WriteLine($"Jogador recebe {amount}€.");
                }
                break;

            case SpaceType.LuxTax:
                if (player.DeductMoney(80m))
                {
                    gameState.AddToFreeParkPot(80m);
                }
                break;

            case SpaceType.Chance:
                System.Console.WriteLine("Espaço especial. Tirar carta.");
                break;

            case SpaceType.Community:
                System.Console.WriteLine("Espaço especial. Tirar carta.");
                break;
        }
    }

    /// <summary>
    /// TT NomeJogador - Terminar turno
    /// </summary>
    public bool EndTurn(string playerName)
    {
        var gameState = _gameRepository.GetCurrentGame();
        if (gameState == null || !gameState.IsGameRunning)
        {
            System.Console.WriteLine("Não existe um jogo em curso.");
            return true;
        }

        var player = gameState.Players.FirstOrDefault(p => p.Name == playerName);
        if (player == null)
        {
            System.Console.WriteLine("Jogador não participa no jogo em curso.");
            return true;
        }

        if (gameState.GetCurrentPlayer() != player)
        {
            System.Console.WriteLine("Não é o turno do jogador indicado.");
            return true;
        }

        // Avançar para o próximo jogador
        gameState.NextPlayer();
        var nextPlayer = gameState.GetCurrentPlayer();

        _gameRepository.SaveGame(gameState);

        System.Console.WriteLine($"Turno terminado. Novo turno do jogador {nextPlayer?.Name}.");
        return true;
    }
}
