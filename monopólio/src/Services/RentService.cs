namespace Monopólio.Services;

using Monopólio.Models;

public class RentService
{
    public decimal CalculateRent(Space space) => space.CalculateRent();
    public void ProcessRentPayment(Player payer, Space property) { }
    public string GetRentCalculationDetails(Space space) => "";
    public bool GeneratesRent(Space space) => space.Type == SpaceType.Property && space.Owner != null;
}
