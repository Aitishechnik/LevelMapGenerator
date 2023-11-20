using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesGenerator : MonoBehaviour
{
    private Queue<Collectable> _availibleOrbs = new Queue<Collectable>();

    [SerializeField]
    private int _maxOrbs = 5;

    [SerializeField]
    private MapGenerator _mapGenerator;

    private void Start()
    {
        Tile tile;
        for (int i = 0; i < _maxOrbs; i++)
        {           
            do
            {
                tile = _mapGenerator.GetWalkable();
            } while (tile.IsOccupied);

            var collectable = CollectableFactory.Instance.Create("LesserExpSphere", tile.transform.position);

        }
    }
}
