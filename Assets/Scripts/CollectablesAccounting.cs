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
        private GameData _saveData;
        public int Experience { get; private set; }

        public int Gold { get; private set; }

        private void Awake()
        {
            _saveData = SaveSystem.Instance.Load();
            if(_saveData == null)
                _saveData = new GameData();
            Experience = _saveData.Experience;
            Gold = _saveData.Gold;
        }
        public void UpdateTextInfo(Collectable collectable)
        {
            switch (collectable.ThisCollectableData.Type)
            {
                case CollectableType.Exp:
                    Experience += collectable.ThisCollectableData.Expirience;
                    _saveData.SetExperience(Experience);
                    UpdateTextTempalte();
                    break;

                case CollectableType.Currency:
                    Gold += collectable.ThisCollectableData.Gold;
                    _saveData.SetGold(Gold);
                    break;
            }
        }

        public void UpdateTextTempalte()
        {
            _collectablesBalanceUI.text = $"Exp: {Experience}\nGold: {Gold}";
        }

        private void OnApplicationQuit()
        {
            SaveSystem.Instance.Save(_saveData);
        }
    }
}

