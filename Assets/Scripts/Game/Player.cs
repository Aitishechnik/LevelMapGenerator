using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Tile PlayerPosition => PlayerUnit.CurrentTile;

    public Unit PlayerUnit { get; private set; }

    public void SetPlayerUnit(Unit unit)
    {
        PlayerUnit = unit;
    }
}
