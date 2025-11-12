namespace Monopólio.Models;

/// <summary>
/// Representa um espaço no tabuleiro
/// </summary>
public class Space
{
    public string Name { get; set; }
    public SpaceType Type { get; set; }
    public decimal Price { get; set; }
    public Player? Owner { get; set; }
    public int NumberOfHouses { get; set; }  // Max 4
    public int Row { get; set; }  // Linha na matriz 7x7
    public int Column { get; set; }  // Coluna na matriz 7x7
    public PropertyColor? Color { get; set; }  // Cor da propriedade (se aplicável)
    public bool CardDrawn { get; set; } = false;  // Se carta foi tirada

    public Space(string name, SpaceType type, decimal price = 0, PropertyColor? color = null)
    {
        Name = name;
        Type = type;
        Price = price;
        Owner = null;
        NumberOfHouses = 0;
        Row = 0;
        Column = 0;
        Color = color;
    }

    /// <summary>
    /// Verifica se o espaço pode ser comprado
    /// </summary>
    public bool CanBePurchased()
    {
        return Type == SpaceType.Property;
    }

    /// <summary>
    /// Verifica se o espaço está disponível para compra
    /// </summary>
    public bool IsAvailable()
    {
        return CanBePurchased() && Owner == null;
    }

    /// <summary>
    /// Adquire o espaço para um jogador
    /// </summary>
    public bool Purchase(Player player)
    {
        if (!IsAvailable() || !player.CanAfford(Price))
            return false;

        Owner = player;
        player.OwnedProperties.Add(this);
        player.DeductMoney(Price);
        return true;
    }

    /// <summary>
    /// Calcula a renda a pagar pelo espaço
    /// Fórmula: preço * 0,25 + preço * 0,75 * casas
    /// </summary>
    public decimal CalculateRent()
    {
        if (Owner == null)
            return 0;

        decimal baseRent = Price * 0.25m;
        decimal houseRent = Price * 0.75m * NumberOfHouses;
        return baseRent + houseRent;
    }

    /// <summary>
    /// Adiciona uma casa ao espaço (máximo 4)
    /// </summary>
    public bool AddHouse(decimal houseCost)
    {
        if (Owner == null)
            return false;

        if (NumberOfHouses >= 4)  // Máximo 4 casas
            return false;

        if (!Owner.CanAfford(houseCost))
            return false;

        Owner.DeductMoney(houseCost);
        NumberOfHouses++;
        return true;
    }
}
