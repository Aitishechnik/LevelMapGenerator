using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> where T : class
{
    protected Transform _objectTransform;
    protected T _prefab;
    protected List<T> _poolsObjects = new List<T>();

    public Pool(Transform poolsParent, T prefab, int amount = 10)
    {
        _objectTransform = poolsParent;
        _prefab = prefab;
        for (int i = 0; i < amount; i++)
        {
            CreateObject();
        }
    }

    protected abstract T CreateObject();
    public abstract T GetObject();
    public abstract void Return(T obj);
}
