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
    private int _groundTilesAmount = 10;
    [SerializeField]
    private int _wallTilesAmount = 10;
    [SerializeField]
    private int _wallMaxLength = 10;

    private MatrixMap _matrixMap;
    void Awake()
    {
        _matrixMap = new MatrixMap(_height, _width, _groundTilesAmount, _wallTilesAmount, _wallMaxLength);
        GenerteTilesField();
    }


    private void GenerteTilesField()
    {
        for(int i = 0; i < _height;  i++)
        {
            for(int j = 0; j < _width; j++)
            {
                var pos = new Vector3(transform.position.x + (j * _distance), _tilesHeight, transform.position.z + (i * _distance));
                _tiles.Add(TilesFactory.Instance.Create(_matrixMap.Matrix[i, j], pos));
            }
        }
        Debug.Log(_tiles.Count + " tiles in total");
    }
}
