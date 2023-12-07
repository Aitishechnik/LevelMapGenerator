using LevelMapGenerator;
using System;
using System.Collections.Generic;
using TMPro;
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
    private TextMeshPro _textMeshProUGUI;

    public TextMeshPro DebugText => _textMeshProUGUI;

    [SerializeField]
    public bool IsOccupied => _attachedUnit != null;

    public bool IsHavingCollectable { get; private set; } = false;
    public bool IsWalkable { get; private set; } = false;

    public void SetCollectableBinding(bool havingCollectable)
    {
        IsHavingCollectable = havingCollectable;
    }

    public void AttachUnit(Unit unit)
    {
        _attachedUnit = unit;
    }
    public void DetachUnit()
    {
        _attachedUnit = null;
    }

    public float MoveCost => _tileData.MoveCost;
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

    public void GetFreeNeighbours(List<Tile> availbleTiles)
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
        return tile != null && tile.IsWalkable;
    }

    public void SetData(TileData tileData)
    {
        _tileData = tileData;
        transform.localScale = _tileData.Size;
        _meshRenderer.material = _tileData.Material;
        IsWalkable = _tileData.IsWalkable;        
    }

    public event Action<Tile> OnTileClick;

    public void ProcessTileClick()
    {
        OnTileClick?.Invoke(this);
    }

    public bool IsNeighbour(Tile tile)
    {
        return tile == UpperTile || tile == LeftTile || tile == LowerTile || tile == RightTile;
    }

    public void ClearDebugText()
    {
        DebugText.text = string.Empty;
    }
}
