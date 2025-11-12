namespace Monopólio.Services;

using Monopólio.Models;

public class TurnService
{
    private readonly ValidationService _validationService;
    private readonly MovementService _movementService;

    public TurnService(ValidationService validationService, MovementService movementService)
    {
        _validationService = validationService;
        _movementService = movementService;
    }

    public void StartTurn()
    {
        // Implementar
    }

    public void RollDice()
    {
        // Implementar
    }

    public void MovePlayer()
    {
        // Implementar
    }

    public void ProcessSpaceEffect()
    {
        // Implementar
    }

    public void EndTurn()
    {
        // Implementar
    }

    public bool ValidateTurnStart()
    {
        return true;
    }

    public string GetTurnStateDescription()
    {
        return "";
    }

    public int GetTurnNumber()
    {
        return 0;
    }
}
