using LevelMapGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    [SerializeField]
    private MapGenerator _mapGenerator;
    public static UnitFactory Instance { get; private set; }

    [SerializeField]
    private Unit _prefabUnit;

    [SerializeField]
    private UnitsConfig _unitsConfig;

    private Dictionary<UnitType, UnitData> _unitDatasDict = new Dictionary<UnitType, UnitData>();
    private void Start()
    {
        foreach (var unitData in _unitsConfig.Units)
        {
            _unitDatasDict.Add(unitData.Type, unitData);
        }

        Instance = this;

        Create(_mapGenerator.GetWalkable());
    }

    public Unit Create(Tile tile)
    {                             
        var unit = Instantiate(_prefabUnit);
        
        unit.MoveToTile(tile, true);
        return unit;
    }
}
