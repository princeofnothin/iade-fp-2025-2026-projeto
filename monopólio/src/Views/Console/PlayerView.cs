namespace Monopólio.Views.Console;

using Monopólio.Models;
using Monopólio.Repositories;

public static class PlayerView
{
    public static void DisplayAllPlayers(IPlayerRepository repository) { }
    public static void DisplayPlayerDetails(Player player) { }
    public static void DisplayPlayerHistory(Player player) { }
    public static string? RequestPlayerName() => System.Console.ReadLine();
    public static decimal RequestInitialMoney() => 5000;
    public static void ShowPlayerActionError(string action, string error) { }
    public static void ShowPlayerActionSuccess(string action, string message) { }
}
