using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "New Stat")]
public class UpgradeableStat : ScriptableObject
{
    [Header("Visual Stats")]
    public string statName;
    public string statVisualName;
    public string statDescription;
    public Sprite statIcon;

    [Header("Upgrade Stats")]
    public float startingValue = 1f;
    public float gainPerLevel = 0.1f;
    public int baseCost = 10;
    //upgrade cost = base * mul * lvl + lvl * add
    public int addCostGain = 1;
    public float mulCostGain = 1;
}
[System.Serializable]
public class UpgradeableStatInstance
{
    public readonly UpgradeableStat data;
    public int currentLevel { get; private set; }
    float cachedStatValue;

    public UnityEvent OnStatChanged = new UnityEvent();

    public UpgradeableStatInstance(UpgradeableStat statData)
    {
        data = statData;
        currentLevel = 0;
        cachedStatValue = CalculateStatValue();
    }


    public void IncreaseLevel(int levels)
    {
        currentLevel += levels;
        cachedStatValue = CalculateStatValue();
        OnStatChanged.Invoke();
    }

    public float GetCurrentStatValue()
    {
        return cachedStatValue;
    }

    public float CalculateStatValue()
    {
        return data.startingValue + currentLevel * data.gainPerLevel;
    }

    public int GetUpgradeCost()
    {
        return Mathf.CeilToInt(data.mulCostGain * (currentLevel + 1) * data.baseCost) + (currentLevel + 1) * data.addCostGain;
    }
}

