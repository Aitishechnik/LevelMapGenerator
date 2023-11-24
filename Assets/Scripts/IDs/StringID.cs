using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class StringID
{
    [SerializeField]
    protected string value;
    public string Value => value;

    public StringID(string value)
    {
        this.value = value;
    }
}
