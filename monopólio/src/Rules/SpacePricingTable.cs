namespace Monopólio.Rules;

/// <summary>
/// Tabela de preços para os espaços do tabuleiro
/// </summary>
public static class SpacePricingTable
{
    // Dicionário com os preços de cada espaço
    private static readonly Dictionary<string, decimal> Prices = new()
    {
        // Propriedades Teal
        { "Teal1", 100m },
        { "Teal2", 120m },

        // Propriedades White
        { "White1", 140m },
        { "White2", 160m },

        // Propriedades Pink
        { "Pink1", 180m },
        { "Pink2", 200m },

        // Propriedades Red
        { "Red1", 220m },
        { "Red2", 240m },

        // Propriedades Yellow
        { "Yellow1", 260m },
        { "Yellow2", 280m },

        // Propriedades Green
        { "Green1", 300m },
        { "Green2", 320m },

        // Propriedades Blue
        { "Blue1", 340m },
        { "Blue2", 360m },

        // Propriedades Dark Blue
        { "DarkBlue1", 380m },
        { "DarkBlue2", 400m }
    };

    /// <summary>
    /// Obtém o preço de um espaço
    /// </summary>
    public static decimal GetPrice(string spaceName)
    {
        return Prices.TryGetValue(spaceName, out var price) ? price : 0m;
    }

    /// <summary>
    /// Calcula o custo de uma casa (60% do preço)
    /// </summary>
    public static decimal CalculateHouseCost(decimal propertyPrice)
    {
        return propertyPrice * 0.6m;
    }

    /// <summary>
    /// Obtém todos os espaços com preço
    /// </summary>
    public static IEnumerable<string> GetAllPricedSpaces()
    {
        return Prices.Keys;
    }

    /// <summary>
    /// Verifica se um espaço tem preço definido
    /// </summary>
    public static bool HasPrice(string spaceName)
    {
        return Prices.ContainsKey(spaceName);
    }
}
