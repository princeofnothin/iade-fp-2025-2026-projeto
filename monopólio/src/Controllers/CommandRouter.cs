namespace Monopólio.Controllers;

using Monopólio.Views.Console;

/// <summary>
/// Recebe linha de comando e despacha para o Controller/UseCase apropriado
/// Formato: COMANDO arg1 arg2 arg3 ...
/// </summary>
public class CommandRouter
{
    private readonly GameController _gameController;
    private readonly PlayerController _playerController;

    public CommandRouter(GameController gameController, PlayerController playerController)
    {
        _gameController = gameController;
        _playerController = playerController;
    }

    /// <summary>
    /// Processa um comando recebido
    /// </summary>
    public void ProcessCommand(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return;
        }

        var parts = input.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
            return;

        string cmd = parts[0].ToUpper();

        try
        {
            switch (cmd)
            {
                // RJ NomeJogador
                case "RJ":
                    if (parts.Length == 2)
                        _playerController.RegisterPlayer(parts[1]);
                    else
                        InvalidCommand(cmd);
                    break;

                // LJ
                case "LJ":
                    if (parts.Length == 1)
                        _playerController.ListPlayers();
                    else
                        InvalidCommand(cmd);
                    break;

                // IJ NomeJogadorA NomeJogadorB NomeJogadorC NomeJogadorD
                case "IJ":
                    if (parts.Length >= 3)
                        _gameController.InitializeGame(parts.Skip(1).ToList());
                    else
                        InvalidCommand(cmd);
                    break;

                // LD NomeJogador
                case "LD":
                    if (parts.Length == 2)
                        _gameController.LaunchDice(parts[1]);
                    else
                        InvalidCommand(cmd);
                    break;

                // CE NomeJogador
                case "CE":
                    if (parts.Length == 2)
                        _playerController.BuySpace(parts[1]);
                    else
                        InvalidCommand(cmd);
                    break;

                // DJ
                case "DJ":
                    if (parts.Length == 1)
                        _gameController.DisplayGame();
                    else
                        InvalidCommand(cmd);
                    break;

                // TT NomeJogador
                case "TT":
                    if (parts.Length == 2)
                        _gameController.EndTurn(parts[1]);
                    else
                        InvalidCommand(cmd);
                    break;

                // PA NomeJogador
                case "PA":
                    if (parts.Length == 2)
                        _playerController.PayRent(parts[1]);
                    else
                        InvalidCommand(cmd);
                    break;

                // CC NomeJogador NomeDoEspaço
                case "CC":
                    if (parts.Length == 3)
                        _playerController.BuildHouse(parts[1], parts[2]);
                    else
                        InvalidCommand(cmd);
                    break;

                // TC NomeJogador
                case "TC":
                    if (parts.Length == 2)
                        _playerController.DrawCard(parts[1]);
                    else
                        InvalidCommand(cmd);
                    break;

                default:
                    InvalidCommand(cmd);
                    break;
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Erro ao processar comando: {ex.Message}");
        }
    }

    private void InvalidCommand(string command)
    {
        System.Console.WriteLine("Instrução inválida.");
    }
}

