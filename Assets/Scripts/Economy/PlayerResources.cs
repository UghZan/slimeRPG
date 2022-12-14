using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources Instance;
    public ulong PlayerMoney;

    public static UnityEvent OnMoneyChanged = new UnityEvent();
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public bool TrySubstractMoney(ulong amount)
    {
        if (PlayerMoney < amount) return false;

        PlayerMoney -= amount;
        OnMoneyChanged.Invoke();
        return true;
    }

    public void AddMoney(ulong amount)
    {
        PlayerMoney += amount;
        OnMoneyChanged.Invoke();
    }
}
