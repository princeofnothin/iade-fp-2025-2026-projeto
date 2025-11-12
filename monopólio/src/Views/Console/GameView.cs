namespace Monopólio.Views.Console;

public static class GameView
{
    public static void ShowMainMenu() { }
    public static void ShowHelp() { }
    public static void ShowSuccess(string msg) => System.Console.WriteLine(msg);
    public static void ShowError(string msg) => System.Console.WriteLine(msg);
    public static void ShowWarning(string msg) => System.Console.WriteLine(msg);
    public static void ShowInfo(string msg) => System.Console.WriteLine(msg);
    public static void ShowGameStatus() { }
    public static void RequestCommand() { }
    public static void ShowSeparator() { }
    public static void Pause() { }
    public static void ShowList(string[] items) { }
}
