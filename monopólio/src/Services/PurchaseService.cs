namespace Monopólio.Services;

using Monopólio.Models;

public class PurchaseService
{
    public (bool success, string message) TryBuySpace(Player player, Space space, GameState gameState)
    {
        if (space == null)
            return (false, "Espaço inválido.");

        if (space.Type != SpaceType.Property)
            return (false, "Este espaço não está para venda.");

        if (space.Owner != null)
            return (false, "O espaço já se encontra comprado.");

        if (!player.CanAfford(space.Price))
            return (false, "O jogador não tem dinheiro suficiente para adquirir o espaço.");

        // Efetuar compra
        player.DeductMoney(space.Price);
        space.Purchase(player);
        return (true, "Espaço comprado.");
    }

    public (bool success, string message) TryBuildHouse(Player player, Space space, GameState gameState)
    {
        if (space == null)
            return (false, "Espaço inválido.");

        if (space.Type != SpaceType.Property)
            return (false, "Não é possível comprar casa no espaço indicado.");

        if (space.Owner?.Name != player.Name)
            return (false, "O jogador não possui este espaço.");

        if (space.NumberOfHouses >= 4)
            return (false, "Não é possível comprar casa no espaço indicado.");

        if (space.Color.HasValue)
        {
            var colorProperties = gameState.Board.GetAllSpaces()
                .Where(s => s.Type == SpaceType.Property && s.Color == space.Color)
                .ToList();

            var playerOwnedOfColor = colorProperties.Where(s => s.Owner?.Name == player.Name).Count();
            if (playerOwnedOfColor != colorProperties.Count)
                return (false, "O jogador não possui todos os espaços da cor.");
        }

        decimal houseCost = GetHouseCost(space);
        if (!player.CanAfford(houseCost))
            return (false, "O jogador não possui dinheiro suficiente.");

        player.DeductMoney(houseCost);
        space.AddHouse(houseCost);
        return (true, "Casa adquirida.");
    }

    public decimal GetHouseCost(Space property) => property.Price * 0.6m;

    public IEnumerable<Space> GetBuildableProperties(Player player, Board board)
    {
        return board.GetAllSpaces()
            .Where(s => s.Type == SpaceType.Property && s.Owner?.Name == player.Name && s.NumberOfHouses < 4);
    }
}
