namespace Monopólio.Services;

using Monopólio.Models;

public class RentService
{
    public decimal CalculateRent(Space space) => space.CalculateRent();

    public (bool success, string message) TryPayRent(Player payer, Space space)
    {
        if (space == null || space.Owner == null || space.Type != SpaceType.Property)
            return (false, "Não é necessário pagar aluguer.");

        if (space.Owner.Name == payer.Name)
            return (false, "Não é necessário pagar aluguer.");

        decimal rent = CalculateRent(space);
        if (!payer.CanAfford(rent))
            return (false, "O jogador não tem dinheiro suficiente.");

        payer.DeductMoney(rent);
        space.Owner.AddMoney(rent);
        return (true, "Aluguer pago.");
    }

    public string GetRentCalculationDetails(Space space) => $"{CalculateRent(space)}";
    public bool GeneratesRent(Space space) => space.Type == SpaceType.Property && space.Owner != null;
}
