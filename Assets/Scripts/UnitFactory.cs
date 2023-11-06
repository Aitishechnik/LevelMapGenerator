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

        // Test: Spawn units of all types
        Create(UnitType.Hero,_mapGenerator.GetWalkable());
        Create(UnitType.Maniac,_mapGenerator.GetWalkable());
        Create(UnitType.Civilian,_mapGenerator.GetWalkable());
        Create(UnitType.Policeman,_mapGenerator.GetWalkable());
        //-------------------------------
    }

    public Unit Create(UnitType type, Tile tile)
    {                             
        var unit = Instantiate(_prefabUnit);        
        unit.MoveToTile(tile, true);
        unit.SetData(_unitDatasDict[type]);
        return unit;
    }
}
