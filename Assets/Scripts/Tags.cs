using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TagsConfig", menuName = "Configs/TagsConfig")]
public class  TagsConfig : ScriptableObject
{
    [SerializeField]
    private List<Tag> _tags = new List<Tag>();
    public List<Tag> Tags => _tags;
}

[Serializable]
public class Tag
{
    [SerializeField]
    private string _name;
    public string Name => _name;

    /*public const string PLAYER = "Player";
    public const string ENEMY = "Enemy";*/
}

//TODO: 1. Сделать ScriptableObject (как configs) и разместить все теги там.
//      2. Сделать на основе tags - TagID и разместить его в SpawnUnitCongig в UnitGenerator 
