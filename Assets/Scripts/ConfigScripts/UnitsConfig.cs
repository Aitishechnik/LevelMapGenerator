using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitsConfig", menuName = "Configs/UnitsConfig")]
public class UnitsConfig : ScriptableObject
{
    [SerializeField]
    private List<UnitData> _units;

    public List<UnitData> Units { get { return _units; } }
}

[Serializable]
public class UnitData
{
    [SerializeField]
    private TagID _tagID;
    public string Tag => _tagID.Value;

    [SerializeField]
    private float _offsetY =1f;   
    public float OffsetY => _offsetY;

    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private Mesh _mesh;
    public Mesh Mesh => _mesh; 

    [SerializeField]
    private Material _material;
    public Material Material => _material;

    [SerializeField]
    private StatsData _stats;
    public StatsData Stats => _stats;

    [SerializeField]
    private ProjectileRotator _projectileRotator;
    public ProjectileRotator ProjectileRotator => _projectileRotator;
}
