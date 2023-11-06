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

    public Unit Create(Tile tile) // TODO: 1. Добавить параметры для настройки Unit. 2. Подумать о методе спавна юнита (в том числе controlable). 
    {                             // 3. Как будет осуществляться передача инфы о тайле юниту (возможно евент)?
        var unit = Instantiate(_prefabUnit);
        
        unit.MoveToTile(tile, true);
        return unit;
    }
}
