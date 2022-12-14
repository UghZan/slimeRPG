using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class UI_StatElement : MonoBehaviour
{
    UI_Upgrades parent;

    UpgradeableStatInstance keptInstance;
    public UpgradeableStatInstance KeptStat => keptInstance;
    [SerializeField] Image statIconImage;
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statLvlText;
    [SerializeField] TextMeshProUGUI statValueText;
    [SerializeField] TextMeshProUGUI statUpgradeCostText;
    [SerializeField] Button upgradeButton;

    public void SetParent(UI_Upgrades _parent) => parent = _parent;
    public void Init(UpgradeableStatInstance instance)
    {
        keptInstance = instance;
        statIconImage.sprite = instance.data.statIcon;
        statNameText.text = instance.data.statVisualName;
        SetValues();
        upgradeButton.onClick.AddListener(CallUpgrade);
    }

    public void SetValues()
    {
        statLvlText.text = (keptInstance.currentLevel + 1).ToString();
        statValueText.text = $"{keptInstance.GetCurrentStatValue()} + {keptInstance.data.gainPerLevel}";
        statUpgradeCostText.text = keptInstance.GetUpgradeCost().ToString();
    }

    public void CallUpgrade()
    {
        parent.UpgradeStat(this);
    }
}
