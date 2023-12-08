using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatsData : IDamagable
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

    private float _nextDamageTime;

    public void SetHP(int minValue)
    {
        _hp = minValue;
    }

    public void ReceiveDamage(Damage damage)
    {
        if (Time.time < _nextDamageTime)
            return;

        _hp -= damage.Value;
        _nextDamageTime = Time.time + _invulnerabiltyPeriod;
    }
}
