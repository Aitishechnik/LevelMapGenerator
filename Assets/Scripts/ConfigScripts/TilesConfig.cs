using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelMapGenerator
{
    [CreateAssetMenu(fileName = "TilesConfig", menuName = "Configs/TilesConfig")]
    public class TilesConfig : ScriptableObject
    {
        [SerializeField]
        private List<TileData> tiles;

        public List<TileData> Tiles { get { return tiles; } }
    }

    [Serializable]
    public class TileData
    {
        [SerializeField]
        private Vector3 _size;
        public Vector3 Size { get => _size; }

        [SerializeField]
        private Material _material;
        public Material Material { get => _material; }

        [SerializeField]
        private char _tileSymbol;
        public char TileSymbol { get => _tileSymbol; }

        [SerializeField]
        private bool _isWalkable;
        public bool IsWalkable { get => _isWalkable; }

        [SerializeField]
        private float _moveCost;
        public float MoveCost { get => _moveCost; }

    }
}
