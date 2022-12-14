using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealthBar : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Slider playerHPSlider;

    private void Update()
    {
        playerHPSlider.value = Mathf.Lerp(playerHPSlider.value, playerHealth.PlayerHealthValue / playerHealth.PlayerMaxHealthValue, Time.deltaTime * 15f);
    }
}
