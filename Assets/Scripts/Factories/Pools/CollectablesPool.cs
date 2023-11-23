using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesPool
{
    private Transform _objectTransform;
    private Collectable _collectablePrefab;
    private List<Collectable> _poolsObjects = new List<Collectable>();
    public CollectablesPool(Transform poolsParent, Collectable prefab, int amount = 10)
    {
        _objectTransform = poolsParent;
        _collectablePrefab = prefab;
        for (int i = 0; i < amount;  i++)
        {
            CreateObject();
        }
    }
    private Collectable CreateObject()
    {
        var collectable = GameObject.Instantiate(_collectablePrefab, _objectTransform);
        collectable.gameObject.SetActive(false);
        collectable.SetPool(this);
        _poolsObjects.Add(collectable);
        return collectable;
    }

    public Collectable GetCollectable()
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

    public void Return(Collectable collectable)
    {
        collectable.gameObject.SetActive(false);
        _poolsObjects.Add(collectable);
        collectable.transform.parent = _objectTransform;
    }
}
