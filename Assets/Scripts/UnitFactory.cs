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

    private Dictionary<string, UnitData> _unitDatasDict = new Dictionary<string, UnitData>();
    private void Start()
    {
        foreach (var unitData in _unitsConfig.Units)
        {
            _unitDatasDict.Add(unitData.Type, unitData);
        }

        Instance = this;

        // Test: Spawn units of all types
        foreach(var type in _unitDatasDict.Keys)
        {
            Create(type, _mapGenerator.GetWalkable());
        }
        //-------------------------------
    }

    public Unit Create(string type, Tile tile)
    {                             
        var unit = Instantiate(_prefabUnit);        
        unit.MoveToTile(tile, true);
        unit.SetData(_unitDatasDict[type]);
        unit.AttachToTile(tile);
        return unit;
    }
}
