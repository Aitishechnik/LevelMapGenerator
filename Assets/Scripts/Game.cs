using Collectables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private CollectablesAccounting _collectablesAccounting;
    public CollectablesAccounting CollectablesAccounting => _collectablesAccounting;

    public static Game Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }


}
