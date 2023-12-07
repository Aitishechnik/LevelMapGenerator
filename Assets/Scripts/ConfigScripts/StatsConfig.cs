using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsConfig", menuName = "Configs/StatsConfig")]
public class StatsConfig : ScriptableObject
{
    [SerializeField]
    private List<Stats> _stats;

    public List<Stats> Stats { get { return _stats; } }
}

[Serializable]
public class Stats
{
    [SerializeField]
    [Range(0.1f,1)]
    private float _movingSpeed;
    public float MovingSpeed => _movingSpeed;

    [SerializeField]
    private int _hp;
    public int HP => _hp;
}
