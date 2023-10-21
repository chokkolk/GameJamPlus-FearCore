using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public enum PlayerType
    {
        None,
        Brother,
        Sister
    }

    public List<Respawn> Respawns;

    public Respawn GetRespawnByPlayerType(PlayerType playerType)
    {
        return Respawns.First(x => x.PlayerType == playerType);
    }

    public void SetPlayerTypeAvailability(PlayerType playerType, bool availability)
    {
        Respawns.First(x => x.PlayerType == playerType).IsAvailable = availability;
    }

    public PlayerType? GetNextPlayerTypeAvailable()
    {
        PlayerType? playerType = Respawns.FirstOrDefault(x => x.IsAvailable)?.PlayerType;

        return playerType == null ? PlayerType.None : playerType;
    }
}
