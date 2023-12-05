using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGenerator : MonoBehaviour
{
    [SerializeField]
    private List<SpawnUnitConfig> _generateParams;

    private const float RESPAWN_TIME = 1;

    [SerializeField]
    private Unit _unit;

    [SerializeField]
    private MapGenerator _mapGenerator;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        Tile tile;
        int _counter = 0;

        while (true)
        {
            if (_counter >= _generateParams.Count)
            {
                yield return null;
            }
            else
            {
                for(int i = 0; i < _generateParams[_counter].Amount; i++)
                {
                    do
                    {
                        tile = _mapGenerator.GetWalkable();
                    } while (tile.IsOccupied);

                    yield return new WaitForSeconds(RESPAWN_TIME);
                    SpawnUnit(_generateParams[_counter].Name, tile, _generateParams[_counter].IsControlable);
                }
                _counter++;
            }
        }
    }

    private void SpawnUnit(string name, Tile tile, bool isControllable)
    {
        UnitFactory.Instance.Create(name, tile, isControllable);
    }
}