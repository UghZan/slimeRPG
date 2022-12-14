using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    public static UpgradeManager Instance;

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void UpgradeStat(string statName)
    {
        UpgradeStat(playerStats.GetStat(statName));
    }

    public void UpgradeStat(UpgradeableStatInstance instance)
    {
        if (PlayerResources.Instance.TrySubstractMoney((ulong)instance.GetUpgradeCost()))
        {
            instance.IncreaseLevel(1);
            playerStats.OnStatsUpdated.Invoke();
        }
    }
}
