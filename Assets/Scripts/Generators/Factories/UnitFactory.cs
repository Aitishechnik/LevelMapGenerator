using System.Collections.Generic;
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

    [SerializeField]
    private PlayerUnitController _playerUnitController;
    [SerializeField]
    private EnemyUnitController _enemyUnitController;

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
        var unit = _pool.GetObject();

        if (isControllable)
        {
            unit.gameObject.GetComponent<EnemyUnitController>().enabled = false;
            _playerInput.SetCamera(unit);
        }           
        else
            unit.gameObject.GetComponent<PlayerUnitController>().enabled = false;

        unit.MoveToTile(tile, true);
        unit.SetData(_unitDatasDict[type], isControllable);
        unit.AttachToTile(tile);
        return unit;
    }
}