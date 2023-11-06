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
    
    [SerializeField]
    private MeshRenderer _meshRenderer;

    private TileData _tileData;

    public bool IsWalkable { get; private set; } = false;

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

    public void SetData(TileData tileData)
    {
        transform.localScale = tileData.Size;
        _meshRenderer.material = tileData.Material;
        IsWalkable = tileData.IsWalkable;
        _tileData = tileData;
    }
    private void OnMouseDown()
    {
        Debug.Log($"{_tileData.TileSymbol} : {transform.position} : {IsWalkable}");
    }

    public bool IsNeighbour(Tile tile)
    {
        return tile == UpperTile || tile == LeftTile || tile == LowerTile || tile == RightTile;
    }
}
