using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_CoinCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;

    private void Start()
    {
        PlayerResources.OnMoneyChanged.AddListener(UpdateCoins);
        UpdateCoins();
    }

    private void UpdateCoins()
    {
        coinText.text = PlayerResources.Instance.PlayerMoney.ToString();
    }
}
