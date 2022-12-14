using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] ScrollManager scrollManager;

    [Header("Settings")]
    public float TimeBetweenWaves;
    public LevelData CurrentLevel;
    [Space(8)]
    public bool WaveInProgress;
    int currentWave = 0;
    int levelBonus = 0;

    public static UnityEvent OnWaveStart = new UnityEvent();
    public static UnityEvent OnWaveEnd = new UnityEvent();

    public void Init()
    {
        scrollManager.StartScroll();
        EnemyManager.OnAllEnemiesKilled.AddListener(EndWave);
        StartCoroutine(Helper.DelayThenAction(TimeBetweenWaves, StartWave));
    }

    void StartWave()
    {
        OnWaveStart.Invoke();
        scrollManager.EndScroll();
        WaveInProgress = true;
        enemyManager.SpawnEnemies(CurrentLevel.levelWaves[currentWave], CurrentLevel.levelLvlBonus + CurrentLevel.levelLvlGrowthPerWave * currentWave + levelBonus);
    }

    
    void EndWave()
    {
        OnWaveEnd.Invoke();
        currentWave++;
        if (currentWave == CurrentLevel.levelWaves.Length)
        {
            currentWave = 0;
            levelBonus++;
        }
        scrollManager.StartScroll();
        WaveInProgress = false;
        StartCoroutine(Helper.DelayThenAction(TimeBetweenWaves, StartWave));
    }
}
