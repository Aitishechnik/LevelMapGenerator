using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using LevelMapGenerator;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private float _distance = 1.0f;
    [SerializeField]
    private float _tilesHeiht = 0f;
    [SerializeField]
    private TileGround _tileGroundPrefab;
    [SerializeField]
    private TileRiver _tileRiverPrefab;
    [SerializeField]
    private TileWall _tileWallPrefab;
    [SerializeField]
    private int _height = 10;
    [SerializeField]
    private int _width = 10;
    [SerializeField]
    private int _groundTilesAmount = 10;

    private MatrixMap _matrixMap;
    void Start()
    {
        _matrixMap = new MatrixMap(_height, _width, _groundTilesAmount);
        GenerteTilesField();
    }

    private void GenerteTilesField()
    {
        for(int i = 0; i < _height;  i++)
        {
            for(int j = 0; j < _width; j++)
            {
                if (_matrixMap.Matrix[i,j] == _matrixMap.GROUND)
                {
                    Instantiate(_tileGroundPrefab, new Vector3(transform.position.x + (j * _distance), _tilesHeiht, transform.position.z + (i * _distance)), Quaternion.identity);
                }
                
                if(_matrixMap.Matrix[i, j] == _matrixMap.RIVER)
                {
                    Instantiate(_tileRiverPrefab, new Vector3(transform.position.x + (j * _distance), _tilesHeiht, transform.position.z + (i * _distance)), Quaternion.identity);
                }

                if (_matrixMap.Matrix[i, j] == _matrixMap.WALL)
                {
                    Instantiate(_tileWallPrefab, new Vector3(transform.position.x + (j * _distance), _tilesHeiht, transform.position.z + (i * _distance)), Quaternion.identity);
                }
            }
        }
    }
}
