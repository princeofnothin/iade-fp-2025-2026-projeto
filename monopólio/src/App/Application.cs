namespace Monopólio.App;

using Monopólio.Controllers;
using Monopólio.Views.Console;

/// <summary>
/// Classe principal da aplicação
/// </summary>
public class Application
{
    private readonly CommandRouter _commandRouter;

    public Application(CommandRouter commandRouter)
    {
        _commandRouter = commandRouter;
    }

    /// <summary>
    /// Executa o loop principal da aplicação
    /// Termina quando recebe uma linha vazia
    /// </summary>
    public void Run()
    {
        while (true)
        {
            string? input = System.Console.ReadLine();

            // Termina com linha vazia
            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }

            _commandRouter.ProcessCommand(input);
        }
    }
}
