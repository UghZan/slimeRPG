using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelManager LevelManager;
    [SerializeField] AttackManager AttackManager;
    [SerializeField] PlayerStats PlayerStats;
    [SerializeField] PlayerHealth PlayerHealth;
    [SerializeField] UI_Upgrades uiUpgradesManager;
    //This ensures that some of the scripts that are dependent on each other are initialized in right order
    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1080, 1920, true); //eww

        PlayerStats.InitStats();
        PlayerHealth.Init();
        LevelManager.Init();
        AttackManager.Init();
        uiUpgradesManager.CreateElements();
    }
    
    
}
