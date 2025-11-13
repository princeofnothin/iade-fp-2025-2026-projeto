using Monopólio.Models;
using Monopólio.Repositories;
using Monopólio.Services;

namespace Monopólio.Controllers;

public class PlayerController
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameRepository _gameRepository;
    private readonly CardService _cardService;
    private readonly PurchaseService _purchaseService;
    private readonly RentService _rentService;

    public PlayerController(IPlayerRepository playerRepository, IGameRepository gameRepository, CardService cardService, PurchaseService purchaseService, RentService rentService)
    {
        _playerRepository = playerRepository;
        _gameRepository = gameRepository;
        _cardService = cardService;
        _purchaseService = purchaseService;
        _rentService = rentService;
    }

    public void RegisterPlayer(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            System.Console.WriteLine("Nome inválido.");
            return;
        }

        if (_playerRepository.PlayerExists(playerName))
        {
            System.Console.WriteLine("Jogador existente.");
            return;
        }

        var player = new Player(playerName, 5000);
        _playerRepository.RegisterPlayer(player);
        System.Console.WriteLine("Jogador registado com sucesso.");
    }

    public void ListPlayers()
    {
        var players = _playerRepository.GetAllPlayers().ToList();

        if (players.Count == 0)
        {
            System.Console.WriteLine("Nenhum jogador registado.");
            return;
        }

        foreach (var player in players)
        {
            System.Console.WriteLine(player.GetStats());
        }
    }

    public void BuySpace(string playerName)
    {
        var gameState = _gameRepository.GetCurrentGame();
        if (gameState == null || !gameState.IsGameRunning)
        {
            System.Console.WriteLine("Nenhum jogo em progresso.");
            return;
        }
        var player = gameState.Players.FirstOrDefault(p => p.Name == playerName);
        if (player == null)
        {
            System.Console.WriteLine("Jogador não encontrado.");
            return;
        }

        var space = gameState.Board.GetSpaceByLinearIndex(player.Position);
        if (space == null)
        {
            System.Console.WriteLine("Espaço inválido.");
            return;
        }

        var (success, message) = _purchaseService.TryBuySpace(player, space, gameState);
        System.Console.WriteLine(message);
        if (success)
            _gameRepository.SaveGame(gameState);
    }

    public void PayRent(string playerName)
    {
        var gameState = _gameRepository.GetCurrentGame();
        if (gameState == null || !gameState.IsGameRunning)
        {
            System.Console.WriteLine("Nenhum jogo em progresso.");
            return;
        }

        var player = gameState.Players.FirstOrDefault(p => p.Name == playerName);
        if (player == null)
        {
            System.Console.WriteLine("Jogador não encontrado.");
            return;
        }

        var space = gameState.Board.GetSpaceByLinearIndex(player.Position);
        if (space == null)
        {
            System.Console.WriteLine("Espaço inválido.");
            return;
        }

        var (success, message) = _rentService.TryPayRent(player, space);
        System.Console.WriteLine(message);
        if (success)
            _gameRepository.SaveGame(gameState);
    }

    public void BuildHouse(string playerName, string spaceName)
    {
        var gameState = _gameRepository.GetCurrentGame();
        if (gameState == null || !gameState.IsGameRunning)
        {
            System.Console.WriteLine("Nenhum jogo em progresso.");
            return;
        }

        var player = gameState.Players.FirstOrDefault(p => p.Name == playerName);
        if (player == null)
        {
            System.Console.WriteLine("Jogador não encontrado.");
            return;
        }

        var space = gameState.Board.GetSpaceByName(spaceName);
        if (space == null)
        {
            System.Console.WriteLine("Espaço não encontrado.");
            return;
        }

        var (success, message) = _purchaseService.TryBuildHouse(player, space, gameState);
        System.Console.WriteLine(message);
        if (success)
            _gameRepository.SaveGame(gameState);
    }

    public void DrawCard(string playerName)
    {
        var gameState = _gameRepository.GetCurrentGame();
        if (gameState == null || !gameState.IsGameRunning)
        {
            System.Console.WriteLine("Nenhum jogo em progresso.");
            return;
        }

        var player = gameState.Players.FirstOrDefault(p => p.Name == playerName);
        if (player == null)
        {
            System.Console.WriteLine("Jogador não encontrado.");
            return;
        }

        var space = gameState.Board.GetSpaceByLinearIndex(player.Position);
        if (space == null)
        {
            System.Console.WriteLine("Espaço inválido.");
            return;
        }

        if (space.Type != SpaceType.Chance && space.Type != SpaceType.Community)
        {
            System.Console.WriteLine("Não é possível tirar carta neste espaço.");
            return;
        }

        if (space.CardDrawn)
        {
            System.Console.WriteLine("A carta já foi tirada.");
            return;
        }

        space.CardDrawn = true;
        System.Console.WriteLine("Tirou uma carta.");

        // Desenhar e aplicar carta
        var type = space.Type == SpaceType.Chance ? "Chance" : "Community";
        var card = _cardService.DrawCard(type);
        if (card != null)
        {
            card.ApplyEffect(player, gameState);
            // Mostrar texto da carta
            System.Console.WriteLine(card.FullText);
        }

        _gameRepository.SaveGame(gameState);
    }
}
