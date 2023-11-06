using JetBrains.Annotations;
using LevelMapGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance { get; private set; }

    [SerializeField]
    private Tile _prefabTile;

    [SerializeField]
    private TilesConfig _tilesConfig;

    private Dictionary<char, TileData> _tileDatasDict = new Dictionary<char, TileData>();
    private void Awake()
    {
        foreach (var tileData in _tilesConfig.Tiles)
        {
            _tileDatasDict.Add(tileData.TileSymbol,tileData);
        }

        Instance = this;
    }

    public Tile Create(char tileSymbol, Vector3 postion)
    {
        var tile = Instantiate(_prefabTile, postion, Quaternion.identity);
        tile.SetData(_tileDatasDict[tileSymbol]);
        return tile;
    }
}
