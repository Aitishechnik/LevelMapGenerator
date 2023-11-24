using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace Collectables
{
    public class CollectablesAccounting : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _collectablesBalanceUI;

        public int Experience { get; private set; }

        public int Gold { get; private set; }

        private const string EXP = "EXP";

        private void Awake()
        {
            Experience = PlayerPrefs.GetInt(EXP);
        }

        public void UpdateTextInfo(Collectable collectable)
        {
            switch (collectable.ThisCollectableData.Type)
            {
                case CollectableType.Exp:
                    Experience += collectable.ThisCollectableData.Expirience;
                    PlayerPrefs.SetInt(EXP, Experience);
                    PlayerPrefs.Save();
                    
                    UpdateTextTempalte();
                    break;

                case CollectableType.Currency:
                    Gold += collectable.ThisCollectableData.Gold;
                    break;
            }
        }

        public void UpdateTextTempalte()
        {
            _collectablesBalanceUI.text = $"Exp: {Experience}\nGold: {Gold}";
        }
    }
}

