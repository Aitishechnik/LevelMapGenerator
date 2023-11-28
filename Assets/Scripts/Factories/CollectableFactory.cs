using LevelMapGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : MonoBehaviour
{
    public static CollectableFactory Instance { get; private set; }

    private CollectablesPool _pool;

    [SerializeField]
    private Collectable _prefabCollectable;

    [SerializeField]
    private CollectablesConfig _collectablesConfig;
    public CollectablesConfig CollectablesConfig => _collectablesConfig;

    private Dictionary<string, CollectableData> _collectableDatasDict = new Dictionary<string, CollectableData>();

    private void Awake()
    {
        _pool = new CollectablesPool(transform, _prefabCollectable);

        foreach(var collectableData in _collectablesConfig.Collectables)
        {
            _collectableDatasDict.Add(collectableData.Name, collectableData);
        }

        Instance = this;
    }

    public Collectable Create(string name, Vector3 position)
    {
        var collectable = _pool.GetObject();
        collectable.transform.position = position + new Vector3(0, _collectableDatasDict[name].OffsetY, 0);
        collectable.SetPool(_pool);
        collectable.SetData(_collectableDatasDict[name]);
        return collectable;
    }
}
