using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectablesConfig", menuName = "Configs/CollectablesConfig")]
public class CollectablesConfig : ScriptableObject
{
    [SerializeField]
    private List<CollectableData> _collectables;

    public List<CollectableData> Collectables { get { return _collectables; } }
}

public enum CollectableType { Exp, Currency, PowerUp, Skill, Item }
[Serializable]
public class CollectableData
{
    [SerializeField]
    private int _maxItemsOnScene;
    public int MaxItemsOnScene => _maxItemsOnScene < 0 ? 0 : _maxItemsOnScene;

    [SerializeField]
    private float _respawnTime;
    public float RespawnTime => _respawnTime < 0 ? 0 : _respawnTime;

    [SerializeField]
    private CollectableType _type;
    public CollectableType Type => _type;

    [SerializeField]
    private int _expiriece;
    public int Expirience => _expiriece;

    [SerializeField]
    private int _gold;
    public int Gold => _gold;

    [SerializeField]
    private float _offsetY = 1f;
    public float OffsetY => _offsetY;

    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private Vector3 _size;
    public Vector3 Size => _size;

    [SerializeField]
    private Material _material;
    public Material Material => _material;

    [SerializeField]
    private Mesh _mesh;
    public Mesh Mesh => _mesh;
}

