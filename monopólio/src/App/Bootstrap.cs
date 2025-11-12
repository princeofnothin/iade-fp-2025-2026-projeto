namespace Monopólio.App;

using Monopólio.Controllers;
using Monopólio.Models;
using Monopólio.Repositories;
using Monopólio.Rules;

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

        // Controllers
        var gameController = new GameController(gameRepository, playerRepository);
        var playerController = new PlayerController(playerRepository, gameRepository);

        // Command Router
        var commandRouter = new CommandRouter(gameController, playerController);

        // Aplicação
        return new Application(commandRouter);
    }
}
