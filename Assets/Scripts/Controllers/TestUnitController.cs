using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestUnitController : MonoBehaviour
{
    [SerializeField]
    private Unit _unit;


    private List<Tile> _neighbourTiles = new List<Tile>();
    public Tile RandomMove(Unit unit)
    {
        unit.CurrentTile.GetFreeNeighbours(_neighbourTiles);

        if(_neighbourTiles.Count == 0)
            return null;
        return _neighbourTiles[Random.Range(0, _neighbourTiles.Count)];
    }

    private void Update()
    {
        if (!_unit.IsMoving)
        {
            var result = RandomMove(_unit);
            if (result != null)
                _unit.MoveToTile(result);
        }       
    }
}
