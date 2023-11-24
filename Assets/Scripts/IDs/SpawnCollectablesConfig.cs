using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnCollectablesConfig
{
    [SerializeField]
    private CollectableID collectableID;
    public string Name => collectableID.Value;

    [SerializeField]
    private int _maxItemsSpawned;
    public int MaxItemSpawned => _maxItemsSpawned;

    [SerializeField]
    private int _respawnTime;
    public int RespawnTime => _respawnTime;

    public KeyValuePair<string, int> CollectableMaxItems => new KeyValuePair<string, int>(collectableID.Value, _maxItemsSpawned);

    public KeyValuePair<string, int> CollectableRespawnTime => new KeyValuePair<string, int>(collectableID.Value, _respawnTime);
}
