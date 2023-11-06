using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using LevelMapGenerator;

public class Unit : MonoBehaviour
{   
    public Tile CurrentTile { get; private set; }

    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private MeshFilter _meshFilter;
    public void AttachToTile(Tile tile)
    {
        CurrentTile = tile;
        transform.position = tile.transform.position + Vector3.up;
    }

    public void SetData(UnitData unitData)
    {
        _meshFilter.mesh = unitData.Mesh;
        _meshRenderer.material = unitData.Material;

    }

    public void MoveToTile(Tile tile, bool isTeleport = false)
    {
        if(isTeleport)
            AttachToTile(tile);
        else
        {
            if (CurrentTile.IsNeighbour(tile))
            {
                AttachToTile(tile);
            }
            else
            {
                throw new Exception("Is not a neighbour.");
            }
        }           
    }
}
