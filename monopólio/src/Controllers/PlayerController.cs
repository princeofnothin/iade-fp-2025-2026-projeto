using Monopólio.Models;
using Monopólio.Repositories;

namespace Monopólio.Controllers;

public class PlayerController
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameRepository _gameRepository;

    public PlayerController(IPlayerRepository playerRepository, IGameRepository gameRepository)
    {
        _playerRepository = playerRepository;
        _gameRepository = gameRepository;
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

        if (space.Type != SpaceType.Property)
        {
            System.Console.WriteLine("Este espaço não está para venda.");
            return;
        }

        if (space.Owner != null)
        {
            System.Console.WriteLine("O espaço já se encontra comprado.");
            return;
        }

        if (!player.CanAfford(space.Price))
        {
            System.Console.WriteLine("O jogador não tem dinheiro suficiente para adquirir o espaço.");
            return;
        }

        player.DeductMoney(space.Price);
        space.Purchase(player);
        player.OwnedProperties.Add(space);

        System.Console.WriteLine("Espaço comprado.");
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

        if (space.Type != SpaceType.Property || space.Owner == null)
        {
            System.Console.WriteLine("Não é necessário pagar aluguer.");
            return;
        }

        if (space.Owner.Name == playerName)
        {
            System.Console.WriteLine("Não é necessário pagar aluguer.");
            return;
        }

        decimal rent = space.CalculateRent();

        if (!player.CanAfford(rent))
        {
            System.Console.WriteLine("O jogador não tem dinheiro suficiente.");
            return;
        }

        player.DeductMoney(rent);
        space.Owner.AddMoney(rent);

        System.Console.WriteLine("Aluguer pago.");
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

        if (space.Type != SpaceType.Property)
        {
            System.Console.WriteLine("Não é possível comprar casa no espaço indicado.");
            return;
        }

        if (space.Owner?.Name != playerName)
        {
            System.Console.WriteLine("O jogador não possui este espaço.");
            return;
        }

        if (space.NumberOfHouses >= 4)
        {
            System.Console.WriteLine("Não é possível comprar casa no espaço indicado.");
            return;
        }

        if (space.Color.HasValue)
        {
            var colorProperties = gameState.Board.GetAllSpaces()
                .Where(s => s.Type == SpaceType.Property && s.Color == space.Color)
                .ToList();

            var playerOwnedOfColor = colorProperties.Where(s => s.Owner?.Name == playerName).Count();

            if (playerOwnedOfColor != colorProperties.Count)
            {
                System.Console.WriteLine("O jogador não possui todos os espaços da cor.");
                return;
            }
        }

        decimal houseCost = space.Price * 0.6m;

        if (!player.CanAfford(houseCost))
        {
            System.Console.WriteLine("O jogador não possui dinheiro suficiente.");
            return;
        }

        player.DeductMoney(houseCost);
        space.AddHouse(houseCost);

        System.Console.WriteLine("Casa adquirida.");
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
        _gameRepository.SaveGame(gameState);
    }
}
