using LevelMapGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableFactory : MonoBehaviour
{
    public static CollectableFactory Instance { get; private set; }

    [SerializeField]
    private Collectable _prefabCollectable;

    [SerializeField]
    private CollectablesConfig _collectablesConfig;

    private Dictionary<string, CollectableData> _collectableDatasDict = new Dictionary<string, CollectableData>();

    private void Awake()
    {
        foreach(var collectableData in _collectablesConfig.Collectables)
        {
            _collectableDatasDict.Add(collectableData.Name, collectableData);
        }

        Instance = this;
    }

    public Collectable Create(string name, Vector3 position)
    {
        var collectable = Instantiate(_prefabCollectable, position, Quaternion.identity);
        collectable.SetData(_collectableDatasDict[name]);
        return collectable;
    }
}
