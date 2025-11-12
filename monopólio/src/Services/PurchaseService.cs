namespace Monopólio.Services;

using Monopólio.Models;

public class PurchaseService
{
    public void BuySpace(Player player, Space space) { }
    public void BuildHouse(Player player, Space property) { }
    public decimal GetHouseCost(Space property) => property.Price * 0.6m;
    public decimal GetTotalConstructionCost() => 0;
    public bool CanBuildHouses(Player player) => true;
    public IEnumerable<Space> GetBuildableProperties(Player player, Board board) => Enumerable.Empty<Space>();
}
