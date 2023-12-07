using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : UnitController
{
    public EnemyUnitController(Unit unit) : base(unit)
    {

    }

    public override void ManualUpdate()
    {
        _unit.GoToTile(Game.Instance.Player.PlayerPosition);
    }
}
