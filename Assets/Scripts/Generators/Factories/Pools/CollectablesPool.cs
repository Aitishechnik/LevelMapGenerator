using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesPool : Pool<Collectable>
{
    public CollectablesPool(Transform poolsParent, Collectable prefab) : base(poolsParent, prefab) { }

    protected override Collectable CreateObject()
    {
        var collectable = GameObject.Instantiate(_prefab, _objectTransform);
        collectable.gameObject.SetActive(false);
        collectable.SetPool(this);
        _poolsObjects.Add(collectable);
        return collectable;
    }

    public override Collectable GetObject()
    {
        if(_poolsObjects.Count == 0)
        {
            CreateObject();            
        }

        var collectable = _poolsObjects[_poolsObjects.Count - 1];
        collectable.gameObject.SetActive(true);
        _poolsObjects.Remove(collectable);
        return collectable;
    }

    public override void Return(Collectable obj)
    {
        obj.gameObject.SetActive(false);
        _poolsObjects.Add(obj);
        obj.transform.parent = _objectTransform;
    }
}
