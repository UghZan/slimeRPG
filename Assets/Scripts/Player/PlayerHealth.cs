using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public float PlayerHealthValue;
    public float PlayerMaxHealthValue { get; private set; }
    UpgradeableStatInstance playerHealthStatInstance;

    public static UnityEvent<float> OnPlayerHealthChanged = new UnityEvent<float>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void Init()
    {
        playerHealthStatInstance = PlayerStats.Instance.GetStat("player_health");
        playerHealthStatInstance.OnStatChanged.AddListener(UpdateMaxHealth);
        UpdateMaxHealth();
        ResetHealth();
    }

    public void UpdateMaxHealth()
    {
        PlayerMaxHealthValue = playerHealthStatInstance.GetCurrentStatValue();
        ResetHealth();
    }

    public void ResetHealth()
    {
        PlayerHealthValue = PlayerMaxHealthValue;
    }
    
    public void UpdateHealth(float delta)
    {
        PlayerHealthValue = Mathf.Max(0, PlayerHealthValue + delta);
        OnPlayerHealthChanged.Invoke(Mathf.Abs(delta));
    }
}
