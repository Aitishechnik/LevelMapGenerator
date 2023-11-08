using LevelMapGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
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
    private float _offsetY =1f;
    
    public float OffsetY => _offsetY;

    [SerializeField]
    private string _type;
    public string Type => _type;

    [SerializeField]
    private bool _isControlable;
    public bool IsControlable => _isControlable;

    [SerializeField]
    private Mesh _mesh;
    public Mesh Mesh => _mesh; 

    [SerializeField]
    private Material _material;
    public Material Material => _material;
}
