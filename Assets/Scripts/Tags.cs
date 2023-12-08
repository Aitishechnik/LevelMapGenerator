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

//TODO: 1. ������� ScriptableObject (��� configs) � ���������� ��� ���� ���.
//      2. ������� �� ������ tags - TagID � ���������� ��� � SpawnUnitCongig � UnitGenerator 
