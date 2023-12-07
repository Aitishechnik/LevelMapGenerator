using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatsFactory : MonoBehaviour
{
    public static UnitStatsFactory Instance;

    [SerializeField]
    private StatsConfig _statsConfig;
    public StatsConfig StatsConfig => _statsConfig;

    private void Start()
    {
        Instance = this;
    }
}
