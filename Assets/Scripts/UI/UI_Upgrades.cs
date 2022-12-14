using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Upgrades : MonoBehaviour
{
    [SerializeField] UpgradeManager upgradeManager;
    [SerializeField] GameObject uiUpgradeElement;
    [SerializeField] Transform upgradesParent;
    // Start is called before the first frame update
    public void CreateElements()
    {
        foreach(KeyValuePair<string, UpgradeableStatInstance> pair in PlayerStats.Instance.StatInstances)
        {
            GameObject uiElement = Instantiate(uiUpgradeElement, upgradesParent);
            UI_StatElement stat = uiElement.GetComponent<UI_StatElement>();
            stat.SetParent(this);
            stat.Init(pair.Value);
        }
    }

    public void UpgradeStat(UI_StatElement initiator)
    {
        upgradeManager.UpgradeStat(initiator.KeptStat);
        initiator.SetValues();
    }
}
