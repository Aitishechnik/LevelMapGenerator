using LevelMapGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public const int UPPER = 0;
    public const int LEFT = 1;
    public const int LOW = 2;
    public const int RIGHT = 3;
    public Tile UpperTile { get; private set; }
    public Tile LeftTile { get; private set; }
    public Tile LowerTile { get; private set; }
    public Tile RightTile { get; private set; }

    private Unit _attachedUnit;
    
    [SerializeField]
    private MeshRenderer _meshRenderer;

    private TileData _tileData;

    [SerializeField]

    public bool IsOccupied => _attachedUnit != null;

    public bool IsWalkable { get; private set; } = false;

    public void AttachUnit(Unit unit)
    {
        _attachedUnit = unit;
    }
    public void DetachUnit()
    {
        _attachedUnit = null;
    }

    public void SetNeighbourTile(Tile neighbour, int side)
    {
        switch (side)
        {
            case UPPER: UpperTile = neighbour; break;
            case LEFT: LeftTile = neighbour; break;
            case LOW: LowerTile = neighbour; break;
            case RIGHT: RightTile = neighbour; break;
        }
    }

    public void GetFreeNeibours(List<Tile> availbleTiles)
    {
        availbleTiles.Clear();

        if (GetProperTile(UpperTile))
            availbleTiles.Add(UpperTile);
        if (GetProperTile(LeftTile))
            availbleTiles.Add(LeftTile);
        if(GetProperTile(RightTile))
            availbleTiles.Add(RightTile);
        if(GetProperTile(LowerTile))
            availbleTiles.Add(LowerTile);
    }

    private bool GetProperTile(Tile tile)
    {
        return tile != null && tile.IsWalkable && !tile.IsOccupied;
    }

    public void SetData(TileData tileData)
    {
        _tileData = tileData;
        transform.localScale = _tileData.Size;
        _meshRenderer.material = _tileData.Material;
        IsWalkable = _tileData.IsWalkable;        
    }

    private void OnMouseDown()
    {
        Debug.Log($"{_tileData.TileSymbol} : {transform.position} : {IsWalkable} {(IsOccupied ? "OCCUPIED" : "FREE")}");
    }

    public bool IsNeighbour(Tile tile)
    {
        return tile == UpperTile || tile == LeftTile || tile == LowerTile || tile == RightTile;
    }
}
