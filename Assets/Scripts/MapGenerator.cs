using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelMapGenerator;

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
    [SerializeField]
    private bool _proceduralGeneration = true;

    [SerializeField]
    private string _fileAddress;

    private MatrixMap _matrixMap;
    private void Awake()
    {
        if (_proceduralGeneration)
            _matrixMap = new MatrixMap(_height, _width, _groundTilesPotential, _wallTilesPotential, _wallMaxLength);
        else
            _matrixMap = new MatrixMap(_fileAddress);
        GenerteTilesField();
    }

    private void GenerteTilesField()
    {
        for(int i = 0; i < _matrixMap.Matrix.GetLength(0);  i++)
        {
            for(int j = 0; j < _matrixMap.Matrix.GetLength(1); j++)
            {
                var pos = new Vector3(transform.position.x + (j * _distance), _tilesHeight, transform.position.z + (i * _distance));
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

        var list = new List<Tile>();

        for(int i = 0;i < count; i++)
        {
            var randomIndex = Random.Range(0, _walkableTiles.Count);
            for(int j = 0; j < _walkableTiles.Count; j++)
            {
                var index = (randomIndex + j) % _walkableTiles.Count;
                if (list.Contains(_walkableTiles[index]))
                    continue;

                list.Add(_walkableTiles[index]);
                break;
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
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j - width], Tile.LOW);
                if (j > 0)
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j - 1], Tile.LEFT);
                if (i * width < neighbours.Count - width)
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j + width], Tile.UPPER);
                if (j < width - 1)
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j + 1], Tile.RIGHT);
            }
        }
    }
}
