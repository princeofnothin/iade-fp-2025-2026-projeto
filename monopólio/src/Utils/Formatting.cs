namespace Monopólio.Utils;

/// <summary>
/// Helpers para formatação de texto
/// </summary>
public static class Formatting
{
    /// <summary>
    /// Formata um valor monetário
    /// </summary>
    public static string FormatMoney(decimal amount)
    {
        return $"€{amount:F2}";
    }

    /// <summary>
    /// Formata um valor monetário de forma abreviada
    /// </summary>
    public static string FormatMoneyShort(decimal amount)
    {
        return $"€{amount:F0}";
    }

    /// <summary>
    /// Formata o nome de um espaço
    /// </summary>
    public static string FormatSpaceName(string name)
    {
        return name.ToUpper();
    }

    /// <summary>
    /// Formata o nome de um jogador
    /// </summary>
    public static string FormatPlayerName(string name)
    {
        return $"[{name.ToUpper()}]";
    }

    /// <summary>
    /// Cria uma linha de separação
    /// </summary>
    public static string CreateSeparator(int length = 50, char character = '=')
    {
        return new string(character, length);
    }

    /// <summary>
    /// Centraliza um texto numa linha
    /// </summary>
    public static string CenterText(string text, int width = 50)
    {
        if (text.Length >= width)
            return text;

        int totalPadding = width - text.Length;
        int leftPadding = totalPadding / 2;
        int rightPadding = totalPadding - leftPadding;

        return new string(' ', leftPadding) + text + new string(' ', rightPadding);
    }

    /// <summary>
    /// Formata um valor com unidade
    /// </summary>
    public static string FormatWithUnit(double value, string unit)
    {
        return $"{value:F2} {unit}";
    }

    /// <summary>
    /// Cria uma tabela simples de 2 colunas
    /// </summary>
    public static string CreateTable(List<(string key, string value)> rows, int columnWidth = 30)
    {
        var result = new System.Text.StringBuilder();
        result.AppendLine(CreateSeparator(columnWidth * 2 + 3));

        foreach (var (key, value) in rows)
        {
            string paddedKey = key.PadRight(columnWidth);
            string paddedValue = value.PadRight(columnWidth);
            result.AppendLine($"| {paddedKey} | {paddedValue} |");
        }

        result.AppendLine(CreateSeparator(columnWidth * 2 + 3));
        return result.ToString();
    }

    /// <summary>
    /// Trunca um texto se for muito longo
    /// </summary>
    public static string Truncate(string text, int maxLength, string suffix = "...")
    {
        if (text.Length <= maxLength)
            return text;

        return text.Substring(0, maxLength - suffix.Length) + suffix;
    }

    /// <summary>
    /// Formata uma lista com vírgulas
    /// </summary>
    public static string FormatList(IEnumerable<string> items)
    {
        return string.Join(", ", items);
    }
}
