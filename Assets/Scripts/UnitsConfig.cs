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
    private UnitType _type;

    public UnitType Type { get => _type; }

    [SerializeField]
    private bool _isControlable;

    public bool IsControlable { get => _isControlable; }
}
