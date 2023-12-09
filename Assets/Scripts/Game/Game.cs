using Collectables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private CollectablesAccounting _collectablesAccounting;

    [SerializeField]
    private MapGenerator _mapGenerator;

    public MapGenerator MapGenerator => _mapGenerator;

    [SerializeField]
    private UnitControllerHandler _unitControllerHandler;
    public UnitControllerHandler UnitControllerHandler => _unitControllerHandler;

    public Player Player { get; private set; } = new Player();
    public CollectablesAccounting CollectablesAccounting => _collectablesAccounting;
    public static Game Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }


}
