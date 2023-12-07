using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnUnitConfig
{
    [SerializeField]
    private UnitID _unitID;
    public string Name => _unitID.Value;

    [SerializeField]
    private bool _isControlable = false;
    public bool IsControlable => _isControlable;

    [SerializeField]
    private int _amount;
    public int Amount => _isControlable ? 1 : _amount;

    [SerializeField]
    private int _statusConfigIndex;
    public int StatusConfigIndex => _statusConfigIndex;
}
