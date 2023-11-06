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

    [SerializeField]
    private float _timeBetweenSteps = 1f;

    private bool _isWalkingAllowed;
    public IEnumerator RandomMove(Unit unit, float seconds)
    {
        _isWalkingAllowed = false;
        Tile tile = null;

        while (tile == null)
        {
            var randomizer = Random.Range(0, 4);

            switch (randomizer)
            {
                
                case 0:
                tile = _unit.CurrentTile.UpperTile == null ? null : (_unit.CurrentTile.UpperTile.IsWalkable ? _unit.CurrentTile.UpperTile : null); break;
                case 1:
                tile = _unit.CurrentTile.LeftTile == null ? null : (_unit.CurrentTile.LeftTile.IsWalkable ? _unit.CurrentTile.LeftTile : null); break;
                case 2:
                tile = _unit.CurrentTile.LowerTile == null ? null : (_unit.CurrentTile.LowerTile.IsWalkable ? _unit.CurrentTile.LowerTile : null); break;
                case 3:
                tile = _unit.CurrentTile.RightTile == null ? null : (_unit.CurrentTile.RightTile.IsWalkable ? _unit.CurrentTile.RightTile : null); break;  
            }            
        }

        yield return new WaitForSeconds(seconds);

        unit.MoveToTile(tile);
        _isWalkingAllowed = true;
    }

    private void Awake()
    {
        _isWalkingAllowed = true;
    }

    private void Update()
    {
        if (_isWalkingAllowed)
            StartCoroutine(RandomMove(_unit, _timeBetweenSteps));
    }
}
