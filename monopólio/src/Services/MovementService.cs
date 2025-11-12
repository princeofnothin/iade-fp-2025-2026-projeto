namespace Monopólio.Services;

using Monopólio.Models;

public class MovementService
{
    private readonly Board _board;

    public MovementService(Board board)
    {
        _board = board;
    }

    public void MovePlayer(Player player, int steps)
    {
        int newPosition = (player.Position + steps) % 49;
        player.Position = newPosition;
    }

    public void MovePlayerTo(Player player, int position)
    {
        player.Position = position % 49;
    }

    public Space? GetCurrentSpace(Player player)
    {
        return _board.GetSpaceByLinearIndex(player.Position);
    }

    public void SendToJail(Player player)
    {
        player.IsInPrison = true;
        player.TurnsInPrison = 0;
        var prison = _board.GetAllSpaces()
            .FirstOrDefault(s => s.Type == SpaceType.Prison);
        if (prison != null)
        {
            player.Position = _board.CoordinatesToLinearIndex(prison.Row, prison.Column);
        }
    }

    public void ReleaseFromJail(Player player)
    {
        player.IsInPrison = false;
        player.TurnsInPrison = 0;
    }

    public void IncrementJailTurn(Player player)
    {
        if (player.IsInPrison)
        {
            player.TurnsInPrison++;
        }
    }

    public bool IsOnGo(Player player)
    {
        return player.Position == 0;
    }

    public bool IsOnPrison(Player player)
    {
        return player.IsInPrison;
    }

    public bool IsOnFreePark(Player player)
    {
        var space = _board.GetSpaceByLinearIndex(player.Position);
        return space?.Type == SpaceType.FreePark;
    }

    public int CalculateDistance(int from, int to)
    {
        if (to >= from)
            return to - from;
        else
            return (49 - from) + to;
    }

    public bool CrossesGo(int from, int to)
    {
        return to < from;
    }
}
