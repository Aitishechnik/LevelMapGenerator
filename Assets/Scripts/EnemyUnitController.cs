using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitController : UnitController
{
    private void Update()
    {
        _unit.GoToTile(Game.Instance.Player.PlayerUnit.CurrentTile);
    }
}
