using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGenerator : MonoBehaviour
{
    [SerializeField]
    private List<SpawnUnitConfig> _generateParams;

    private const int MAX_COUNT = 5;
    private const float RESPAWN_TIME = 1;

    [SerializeField]
    private Unit _unit;

    [SerializeField]
    private MapGenerator _mapGenerator;

    private int _unitsAmount = 0;

    int _counter;

    private void Start()
    {
        SetUnitsAmount();
        StartCoroutine(SpawnRoutine());
    }

    private void SetUnitsAmount()
    {
        foreach (var amount in _generateParams)
        {
            _unitsAmount += amount.Amount;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        Tile tile;


        while (true)
        {
            if (_counter >= _unitsAmount)
            {
                yield return null;
            }
            else
            {
                do
                {
                    tile = _mapGenerator.GetWalkable();
                } while (tile.IsOccupied);

                yield return new WaitForSeconds(RESPAWN_TIME);
                SpawnUnit(_generateParams[_counter].Name, tile, _generateParams[_counter].IsControlable);
                _counter++;
            }
        }
    }

    private void SpawnUnit(string name, Tile tile, bool isControllable)
    {
        var spawnedUnit = Instantiate(_unit);
        spawnedUnit.AttachToTile(tile);
        spawnedUnit.SetData(UnitFactory.Instance.UnitDatasDict[name], isControllable);
    }
}