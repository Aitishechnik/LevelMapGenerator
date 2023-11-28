using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    private UnitsPool _pool;

    [SerializeField]
    PlayerInput _playerInput;

    [SerializeField]
    private MapGenerator _mapGenerator;
    public static UnitFactory Instance { get; private set; }

    [SerializeField]
    private Unit _prefabUnit;

    [SerializeField]
    private UnitsConfig _unitsConfig;

    private Dictionary<string, UnitData> _unitDatasDict = new Dictionary<string, UnitData>();
    public Dictionary<string, UnitData> UnitDatasDict => _unitDatasDict;
    private void Start()
    {
        _pool = new UnitsPool(transform, _prefabUnit);
        

        foreach (var unitData in _unitsConfig.Units)
        {
            _unitDatasDict.Add(unitData.Name, unitData);
        }

        Instance = this;
    }

    public Unit Create(string type, Tile tile, bool isControllable)
    {                             
        var unit = Instantiate(_prefabUnit);

        unit.ThisUnitData.Controller = isControllable ? new PlayerUnitController() : new EnemyUnitController();
        if (isControllable)
            _playerInput.SetCamera(unit);
        unit.MoveToTile(tile, true);
        unit.SetData(_unitDatasDict[type], isControllable);
        unit.AttachToTile(tile);
        return unit;
    }
}
