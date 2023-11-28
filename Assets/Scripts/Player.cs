using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public PlayerUnitController UnitController { get; private set; }
    public Unit PlayerUnit { get; private set; }
    public void SetPlayerUnit(PlayerUnitController playerUnitController, Unit unit)
    {
        UnitController = playerUnitController;
        PlayerUnit = unit;
    }


}
