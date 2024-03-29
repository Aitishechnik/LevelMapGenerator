using System.Collections.Generic;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    private UnitsPool _pool;

    

    [SerializeField]
    PlayerInput _playerInput;

    public static UnitFactory Instance { get; private set; }

    [SerializeField]
    private Unit _prefabUnit;

    [SerializeField]
    private UnitsConfig _unitsConfig;

    private Dictionary<string, UnitData> _unitDatasDict = new Dictionary<string, UnitData>();

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
            Game.Instance.Player.SetPlayerUnit(unit);
            var playerUnitController = new PlayerUnitController(unit);
            Game.Instance.UnitControllerHandler.Add(playerUnitController);
            _playerInput.SetUnit(unit, playerUnitController);
        }
        else
        {
            var enemyUnitController = new EnemyUnitController(unit);
            Game.Instance.UnitControllerHandler.Add(enemyUnitController);
        }

        unit.MoveToTile(tile, true);
        unit.SetData(_unitDatasDict[type], isControllable);
        unit.SetStats(_unitDatasDict[type].Stats);
        unit.AttachToTile(tile);
        return unit;
    }
}
