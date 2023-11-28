using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController : UnitController
{
    public Tile CurrentTarget => _unit.CurrentTarget;

    private void Start()
    {
        Game.Instance.Player.SetPlayerUnit(this, _unit);
        Tile.OnTileClick += _unit.GoToTile;
    }
}
