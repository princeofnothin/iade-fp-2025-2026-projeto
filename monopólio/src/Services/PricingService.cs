namespace Monopólio.Services;

public class PricingService
{
    public decimal GetSpacePrice(string spaceName) => 100;
    public decimal CalculateHouseCost(decimal price) => price * 0.6m;
    public decimal CalculateRent(decimal price) => price * 0.25m;
    public decimal CalculateTotalConstructionCost() => 100;
    public bool IsValidPrice(decimal price) => price > 0;
    public object GetPricingTable() => new { };
}
