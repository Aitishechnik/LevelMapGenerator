using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using LevelMapGenerator;
using JetBrains.Annotations;

public class MapGenerator : MonoBehaviour
{
    private List<Tile> _tiles = new List<Tile>();
    private List<Tile> _walkableTiles = new List<Tile>();
    [SerializeField]
    private float _distance = 1.0f;
    [SerializeField]
    private float _tilesHeight = 0f;

    [SerializeField]
    private int _height = 10;
    [SerializeField]
    private int _width = 10;
    [SerializeField]
    private int _groundTilesPotential = 10;
    [SerializeField]
    private int _wallTilesPotential = 10;
    [SerializeField]
    private int _wallMaxLength = 10;

    private MatrixMap _matrixMap;
    private void Awake()
    {
        _matrixMap = new MatrixMap(_height, _width, _groundTilesPotential, _wallTilesPotential, _wallMaxLength);
        GenerteTilesField();
    }

    private void GenerteTilesField()
    {
        for(int i = 0; i < _height;  i++)
        {
            for(int j = 0; j < _width; j++)
            {
                var pos = new Vector3(transform.position.z + (i * _distance), _tilesHeight, transform.position.x + (j * _distance));
                _tiles.Add(TileFactory.Instance.Create(_matrixMap.Matrix[i, j], pos));
                if (_tiles[_tiles.Count - 1].IsWalkable)
                    _walkableTiles.Add(_tiles[_tiles.Count - 1]);
            }
        }

        Neighbourhood(_tiles, _matrixMap.Matrix.GetLength(0), _matrixMap.Matrix.GetLength(1));
        Debug.Log(_tiles.Count + " tiles in total");
    }

    public Tile GetWalkable()
    {   
        return _walkableTiles[Random.Range(0, _walkableTiles.Count)];
    }

    public List<Tile> GetWalkables(int count)
    {
        if(count > _walkableTiles.Count)
            count = _walkableTiles.Count;

        bool addTile;
        var list = new List<Tile>();

        for(int i = 0;i < count; i++)
        {
            var tile = GetWalkable();
            addTile = true;

            for(int j = 0; j < list.Count; j++)
            {
                if(tile == list[j])
                {
                    addTile = false; break;
                }
            }

            if(addTile)
            {
                list.Add(tile);
            }
        }
        return list;
    }

    private void Neighbourhood(List<Tile> neighbours, int height, int width)
    {
        int iterator = 0;
        for (int i = 0; i < height; i++)
        {
            iterator = i * width;
            for (int j = 0; j < width; j++)
            {
                if (i * width > width - 1)
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j - width], Tile.UPPER);
                if (j > 0)
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j - 1], Tile.LEFT);
                if (i * width < neighbours.Count - width)
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j + width], Tile.LOW);
                if (j < width - 1)
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j + 1], Tile.RIGHT);
            }
        }
    }
}
