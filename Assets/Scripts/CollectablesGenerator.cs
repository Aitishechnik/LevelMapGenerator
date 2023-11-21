using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collectables;

public class CollectablesGenerator : MonoBehaviour
{
    private const float RESPAWN_TIME = 5f;
    private const float INIT_TIME_GAP = 5f;
    [SerializeField]
    private CollectablesAccounting _collectablesAccounting;

    private Queue<Collectable> _availibleOrbs = new Queue<Collectable>();

    [SerializeField]
    private int _maxOrbs = 5;

    [SerializeField]
    private MapGenerator _mapGenerator;

    private void InitExpOrbs()
    {
        Tile tile;
        for (int i = 0; i < _maxOrbs; i++)
        {
            do
            {
                tile = _mapGenerator.GetWalkable();
            } while (tile.IsOccupied);

            var rnd = Random.Range(0,2);

            if(rnd == 0)
            {
                var collectable = CollectableFactory.Instance.Create("LesserExpOrb", tile.transform.position);
                _availibleOrbs.Enqueue(collectable);
            }
            if (rnd == 1)
            {
                var collectable = CollectableFactory.Instance.Create("LargeExpOrb", tile.transform.position);
                _availibleOrbs.Enqueue(collectable);
            }
            
        }
    }

    private void Start()
    {
        Collectable.OnCollectEvent += OnCollect;
        _collectablesAccounting.UpdateTextTempalte();
        InitExpOrbs();
        StartCoroutine(InitAllCollectables());
    }

    private void OnCollect(Collectable collectable)
    {
        StartCoroutine(CollectableSpawn(collectable, RESPAWN_TIME));
        _collectablesAccounting.UpdateTextInfo(collectable);
    }
    private IEnumerator CollectableSpawn(Collectable collectable, float time)
    {        
        collectable.State(false);
        _availibleOrbs.Enqueue(collectable);
        yield return new WaitForSeconds(time);

        Tile tile;

        do
        {
            tile = _mapGenerator.GetWalkable();
        } while (tile.IsOccupied);

        collectable.transform.position = tile.transform.position + new Vector3(0, collectable.ThisCollectableData.OffsetY, 0);
        collectable.State(true);
    }

    private IEnumerator InitAllCollectables()
    {
        while(_availibleOrbs.Count > 0)
        {
            StartCoroutine(CollectableSpawn(_availibleOrbs.Dequeue(), 0));
            yield return new WaitForSeconds(INIT_TIME_GAP);
        }
    }
}
