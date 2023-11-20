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
        var collectable = Instantiate(_prefabCollectable, position + new Vector3(0, _collectableDatasDict[name].OffsetY, 0), Quaternion.identity);
        collectable.SetData(_collectableDatasDict[name]);
        return collectable;
    }
}

//TODO: придумать механизм спавна експ орб:
//размещение в queue всех доступных к спавну элементов ->
//спавн и вызов метода State(подумать о генерации координат ->
//вызов метода Taken (event у Unit через triggerEnter), в котором вызывается State(false) и объект добавляется в queue доступных для спавна

//TODO: поэксперементировать с разными типами collectables:
//добавить 2 типа експ орб (оранж, рэд) ->
//решить где будет хранится и обрабатыватся инфа о collectable (предварительно в CollecablesGenerator) ->
//подумать о rb для експ орб (эффект рассыпания по земле) 
