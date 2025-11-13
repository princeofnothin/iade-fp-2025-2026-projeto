namespace Monopólio.App;

using Monopólio.Controllers;
using Monopólio.Models;
using Monopólio.Repositories;
using Monopólio.Rules;
using Monopólio.Services;

/// <summary>
/// Registo/instanciação (DI manual) de todas as dependências
/// </summary>
public static class Bootstrap
{
    /// <summary>
    /// Cria a aplicação com todas as suas dependências
    /// </summary>
    public static Application CreateApplication()
    {
        // Repositórios
        IGameRepository gameRepository = new InMemoryGameRepository();
        IPlayerRepository playerRepository = new InMemoryPlayerRepository();

        // Tabuleiro
        var board = BoardLayout.CreateBoard();

        // Serviços de cartas
        var cardService = new CardService();

        // Outros serviços
        var purchaseService = new PurchaseService();
        var rentService = new RentService();

        // Controllers
        var gameController = new GameController(gameRepository, playerRepository);
        var playerController = new PlayerController(playerRepository, gameRepository, cardService, purchaseService, rentService);

        // Command Router
        var commandRouter = new CommandRouter(gameController, playerController);

        // Aplicação
        return new Application(commandRouter);
    }
}
