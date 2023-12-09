using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatsData
{
    [SerializeField]
    [Range(0.1f,1)]
    private float _movingSpeed;
    public float MovingSpeed => _movingSpeed;

    [SerializeField]
    private int _hp;
    public int HP => _hp;

    [SerializeField]
    private float _invulnerabiltyPeriod = 1f;
    public float InvulnerabiltyPeriod => _invulnerabiltyPeriod;
}
