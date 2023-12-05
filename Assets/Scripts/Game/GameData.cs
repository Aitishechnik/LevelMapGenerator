using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int Gold { get; private set; }
    public int Experience { get; private set; }

    public void SetGold(int gold)
    {
        Gold = gold;
    }

    public void SetExperience(int experience)
    {
        Experience = experience;
    }
}
