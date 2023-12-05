using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController : UnitController
{
    public Tile CurrentTarget => _unit.CurrentTarget;
    public static Tile playerPosition;
    private void Start()
    {
        StartCoroutine(UpdatePlayerPositionRoutine());
        Tile.OnTileClick += _unit.GoToTile;
    }

    private IEnumerator UpdatePlayerPositionRoutine()
    {
        while (true)
        {
            playerPosition = _unit.CurrentTile;
            yield return null;
        }
    }
}
