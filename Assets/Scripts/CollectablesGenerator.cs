using System.Collections;
using UnityEngine;
using Collectables;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class CollectablesGenerator : MonoBehaviour
{
    [SerializeField]
    public CollectableID collectableID;

    private int _counter = 0;
    private Dictionary<string, int> _collectablesCurrentAmount = new Dictionary<string, int>();

    [SerializeField]
    private CollectablesAccounting _collectablesAccounting;

    [SerializeField]
    private MapGenerator _mapGenerator;

    private void Start()
    {   
        for(int i = 0; i < CollectableFactory.Instance.CollectablesConfig.Collectables.Count; i++)
        {
            _collectablesCurrentAmount.Add(CollectableFactory.Instance.CollectablesConfig.Collectables[i].Name, 0);
        }
        
        StartCoroutine(SpawnRoutine());
        
        _collectablesAccounting.UpdateTextTempalte();
    }

    private void OnCollect(Collectable collectable)
    {
        _counter--;
        _collectablesCurrentAmount[collectable.ThisCollectableData.Name] -= 1;
        collectable.OnCollectEvent -= OnCollect;
        _collectablesAccounting.UpdateTextInfo(collectable);
        collectable.ReturnToPool();
    }

    private float GetCollectableRespawnTime(string name)
    {
        foreach(var data in CollectableFactory.Instance.CollectablesConfig.Collectables)
        {
            if(data.Name == name)
                return data.RespawnTime;
        }

        throw new ArgumentException("Wrong Collectable name input");
    }

    private IEnumerator SpawnRoutine()
    {
        Tile tile;
        int rnd;
        var collectableDataAmount = CollectableFactory.Instance.CollectablesConfig.Collectables.Count;

        while (true)
        {
            if(_counter >= CollectableFactory.Instance.MaxCollectablesOnScene())
            {
                yield return null;
            }
            else
            {
                do
                {
                    tile = _mapGenerator.GetWalkable();
                } while (tile.IsOccupied);

                rnd = Random.Range(0, collectableDataAmount);

                for (int i = 0; i < collectableDataAmount; i++)
                {
                    rnd = (rnd + i) % collectableDataAmount;
                    if (_collectablesCurrentAmount.TryGetValue(CollectableFactory.Instance.CollectablesConfig.Collectables[rnd].Name, out int currentValue) &&
                        currentValue < CollectableFactory.Instance.CollectablesConfig.Collectables[rnd].MaxItemsOnScene)
                    {
                        break;
                    }
                }


                var name = CollectableFactory.Instance.CollectablesConfig.Collectables[rnd].Name;               
                yield return new WaitForSeconds(GetCollectableRespawnTime(name));
                SetCollectableSpawn(name, tile);
            }
        }
    }
    private void SetCollectableSpawn(string name, Tile tile)
    {
        var collectable = CollectableFactory.Instance.Create(name, tile.transform.position);
        _collectablesCurrentAmount[name] += 1;
        collectable.OnCollectEvent += OnCollect;
        _counter++;
    }
}
