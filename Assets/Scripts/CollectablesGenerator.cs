using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collectables;
using TMPro;

public class CollectablesGenerator : MonoBehaviour
{
    private const float RESPAWN_TIME = 5f;
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

            var collectable = CollectableFactory.Instance.Create("LesserExpOrb", tile.transform.position);
            _availibleOrbs.Enqueue(collectable);
        }
    }

    private void Start()
    {
        Collectable.OnCollectEvent += OnCollect;
        _collectablesAccounting.UpdateTextTempalte();
        InitExpOrbs();
    }

    private void OnCollect(Collectable collectable)
    {
        StartCoroutine(CollectableSpawn(collectable));
        _collectablesAccounting.UpdateTextInfo(collectable);
    }
    private IEnumerator CollectableSpawn(Collectable collectable)
    {
        collectable.State(false);
        yield return new WaitForSeconds(RESPAWN_TIME);

        Tile tile;

        do
        {
            tile = _mapGenerator.GetWalkable();
        } while (tile.IsOccupied);

        collectable.transform.position = tile.transform.position + new Vector3(0, collectable.ThisCollectableData.OffsetY, 0);
        collectable.State(true);
    }
}
