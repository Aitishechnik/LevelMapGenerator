using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitController
{
    protected Unit _unit;

    public UnitController(Unit unit)
    {
        _unit = unit;
        unit.OnUnitDie += OnDie;
    }

    private void OnDie()
    {
        Game.Instance.UnitControllerHandler.Remove(this);
    }

    public abstract void ManualUpdate();
}
