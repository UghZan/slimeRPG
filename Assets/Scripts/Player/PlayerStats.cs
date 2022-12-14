using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [SerializeField] UpgradeableStat[] stats;
    public Dictionary<string, UpgradeableStatInstance> StatInstances;

    public UnityEvent OnStatsUpdated = new UnityEvent();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    public void InitStats()
    {
        StatInstances = new Dictionary<string, UpgradeableStatInstance>();
        for (int i = 0; i < stats.Length; i++)
        {
            StatInstances.Add(stats[i].statName, new UpgradeableStatInstance(stats[i]));
        }
        OnStatsUpdated.Invoke();
    }

    public UpgradeableStatInstance GetStat(string name)
    {
        return StatInstances[name];
    }
}
