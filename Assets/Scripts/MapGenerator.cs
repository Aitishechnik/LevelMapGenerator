using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using LevelMapGenerator;

public class MapGenerator : MonoBehaviour
{
    private List<Tile> _tiles = new List<Tile>();

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
                var pos = new Vector3(transform.position.z + (i * _distance)/*transform.position.x + (j * _distance)*/, _tilesHeight, transform.position.x + (j * _distance)/*transform.position.z + (i * _distance)*/);
                _tiles.Add(TilesFactory.Instance.Create(_matrixMap.Matrix[i, j], pos));
            }
        }

        Neighbourhood(_tiles, _matrixMap.Matrix.GetLength(0), _matrixMap.Matrix.GetLength(1));
        Debug.Log(_tiles.Count + " tiles in total");
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
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j - width], 0);
                if (j > 0)
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j - 1], 1);
                if (i * width < neighbours.Count - width)
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j + width], 2);
                if (j < width - 1)
                    neighbours[iterator + j].SetNeighbourTile(neighbours[iterator + j + 1], 3);
            }
        }
    }
}
