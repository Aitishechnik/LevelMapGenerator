using LevelMapGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Tile UpperTile { get; private set; }
    public Tile LeftTile { get; private set; }
    public Tile LowerTile { get; private set; }
    public Tile RightTile { get; private set; }

    [SerializeField]
    private MeshRenderer _meshRenderer;

    private TileData _tileData;

    protected void SetNeighbourTile(Tile neighbour, int side)
    {
        switch (side)
        {
            case 0: UpperTile = neighbour; break;
            case 1: LeftTile = neighbour; break;
            case 2: LowerTile = neighbour; break;
            case 3: RightTile = neighbour; break;
        }
    }

    public void SetData(TileData tileData)
    {
        transform.localScale = tileData.Size;
        _meshRenderer.material = tileData.Material;
        _tileData = tileData;
    }
    protected void OnMouseDown()
    {
        Debug.Log($"{transform.position}");
    }
}
