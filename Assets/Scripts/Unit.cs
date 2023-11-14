using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Unit : MonoBehaviour 
{
    public const float TRANSITION_TIME = 0.5f;

    public Tile CurrentTile { get; private set; }

    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private MeshFilter _meshFilter;

    public UnitData ThisUnitData { get; private set; }

    public bool IsMoving { get; private set; }

    public IEnumerator MoveSmoothly(Tile targetTile, float moveSpeed)
    {
        IsMoving = true;
        targetTile.AttachUnit(this);
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = targetTile.transform.position + new Vector3(0, ThisUnitData.OffsetY, 0);

        float elapsedTime = 0f;

        while (elapsedTime < moveSpeed)
        {
            float t = elapsedTime / moveSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        AttachToTile(targetTile);
        IsMoving = false;
    }



    public void AttachToTile(Tile tile) 
    {
        if (CurrentTile != null)
            CurrentTile.DetachUnit();

        CurrentTile = tile;
        CurrentTile.AttachUnit(this);
        transform.position = tile.transform.position + Vector3.up;
    }

    public void SetData(UnitData unitData)
    {
        ThisUnitData = unitData;
        _meshFilter.mesh = ThisUnitData.Mesh;
        _meshRenderer.material = ThisUnitData.Material;
    }

    public void MoveToTile(Tile tile, bool isTeleport = false)
    {
        if (IsMoving)
        {
            throw new Exception("Unit is moving");
        }

        if (tile.IsOccupied)
        {
            throw new Exception("Tile is occupied");
        }

        if(tile == null)
        {
            throw new Exception("Tile == null");
        }

        if(isTeleport)
            AttachToTile(tile);
        else
        {
            if (CurrentTile.IsNeighbour(tile))
            {
                StartCoroutine(MoveSmoothly(tile, TRANSITION_TIME));
            }
            else
            {
                throw new Exception("Is not a neighbour.");
            }
        }           
    }
}
