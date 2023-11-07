using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

// TODO: 1. �������� ��� ����������� ������� ����������� ����� �������� 

public class Unit : MonoBehaviour 
{   
    public Tile CurrentTile { get; private set; }

    [SerializeField]
    private MeshRenderer _meshRenderer;

    [SerializeField]
    private MeshFilter _meshFilter;

    public bool IsMoving { get; private set; }

    public IEnumerator MoveSmoothly(Tile targetTile, float moveSpeed)
    {
        IsMoving = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = targetTile.transform.position;

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
            CurrentTile.IsOccupied = false;

        CurrentTile = tile;
        CurrentTile.IsOccupied = true;
        transform.position = tile.transform.position + Vector3.up;
    }

    public void SetData(UnitData unitData)
    {
        _meshFilter.mesh = unitData.Mesh;
        _meshRenderer.material = unitData.Material;
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

        if(isTeleport)
            AttachToTile(tile);
        else
        {
            if (CurrentTile.IsNeighbour(tile))
            {
                //AttachToTile(tile);
                StartCoroutine(MoveSmoothly(tile, 0.5f));
            }
            else
            {
                throw new Exception("Is not a neighbour.");
            }
        }           
    }
}
