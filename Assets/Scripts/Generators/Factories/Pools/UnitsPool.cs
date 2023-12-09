using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsPool : Pool<Unit>
{
    public UnitsPool(Transform poolsParent, Unit prefab, int amount = 10) : base(poolsParent, prefab, amount) { }
    public override Unit GetObject()
    {
        if (_poolsObjects.Count == 0)
        {
            CreateObject();
        }

        var unit = _poolsObjects[_poolsObjects.Count - 1];
        unit.gameObject.SetActive(true);
        _poolsObjects.Remove(unit);
        return unit;
    }

    public override void Return(Unit obj)
    {
        obj.gameObject.SetActive(false);
        _poolsObjects.Add(obj);
        obj.transform.position = _objectTransform.position;
    }

    protected override Unit CreateObject()
    {
        var unit = GameObject.Instantiate(_prefab, _objectTransform);
        unit.gameObject.SetActive(false);
        unit.SetPool(this);
        _poolsObjects.Add(unit);
        return unit;
    }
}
