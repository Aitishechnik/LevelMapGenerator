using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController : UnitController
{
    public Tile CurrentTarget => _unit.CurrentTarget;    

    public PlayerUnitController(Unit unit) : base(unit)
    {
        Game.Instance.MapGenerator.OnTileClick += _unit.GoToTile;
    }

    public override void ManualUpdate()
    {
        
    }
}
