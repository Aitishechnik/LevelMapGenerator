using System.Collections;
using UnityEngine;
using Collectables;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class CollectablesGenerator : MonoBehaviour
{

    [SerializeField]
    private List<SpawnCollectablesConfig> _generateParams;

    private int _counter = 0;
    private Dictionary<string, int> _collectablesCurrentAmount = new Dictionary<string, int>();

    [SerializeField]
    private CollectablesAccounting _collectablesAccounting;

    [SerializeField]
    private MapGenerator _mapGenerator;

    private int _maxCollectablesOnScene;

    private void Start()
    {   
        foreach(var value in _generateParams)
        {
            _maxCollectablesOnScene += value.MaxItemSpawned;
        }

        for(int i = 0; i < CollectableFactory.Instance.CollectablesConfig.Collectables.Count; i++)
        {
            _collectablesCurrentAmount.Add(_generateParams[i].Name, 0);
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

    private IEnumerator SpawnRoutine()
    {
        Tile tile;
        int rnd;
        var amount = _generateParams.Count;
        //var collectableDataAmount = CollectableFactory.Instance.CollectablesConfig.Collectables.Count;

        while (true)
        {
            if(_counter >= _maxCollectablesOnScene)
            {
                yield return null;
            }
            else
            {
                do
                {
                    tile = _mapGenerator.GetWalkable();
                } while (tile.IsOccupied);

                rnd = Random.Range(0, _generateParams.Count);

                for (int i = 0; i < _generateParams.Count; i++)
                {
                    rnd = (rnd + i) % amount;
                    if (_collectablesCurrentAmount.TryGetValue(_generateParams[rnd].Name, out int currentValue) &&
                        currentValue < _generateParams[rnd].MaxItemSpawned)
                    {
                        break;
                    }
                }


                var name = _generateParams[rnd].Name;               
                yield return new WaitForSeconds(_generateParams[rnd].RespawnTime);
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
