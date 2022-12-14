using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [Header("References")]
    public GameObjectPool enemyPool;

    [Header("Spawn Settings")]
    [SerializeField] Transform spawnAnchor;
    [SerializeField] int spawnXOffset;
    [SerializeField] int spawnZOffset;

    public bool FinishedSpawning;
    public List<EnemyInstance> LevelEnemies;
    public int EnemiesLeft;

    public static UnityEvent<EnemyInstance> OnEnemyCreated = new UnityEvent<EnemyInstance>();
    public static UnityEvent<EnemyInstance> OnEnemyKilled = new UnityEvent<EnemyInstance>();
    public static UnityEvent OnAllEnemiesKilled = new UnityEvent();

    private void Start()
    {
        LevelEnemies = new List<EnemyInstance>();
    }

    public void SpawnEnemies(EnemyWaveData waveData, int levelBonus)
    {
        FinishedSpawning = false;
        float playerEnemyLevelBonus = PlayerStats.Instance.GetStat("enemy_lvl_bonus").GetCurrentStatValue();
        foreach(EnemyData enemy in waveData.waveEnemies)
        {
            GameObject instanceGO = enemyPool.Pool.Get();
            instanceGO.transform.position = spawnAnchor.position + new Vector3(Random.Range(-spawnXOffset, spawnXOffset), 0, Random.Range(-spawnZOffset, spawnZOffset));

            EnemyInstance instance = instanceGO.GetComponent<EnemyInstance>();
            instance.SetParentPool(enemyPool.Pool).SetParentManager(this).SetData(enemy).SetLevel(levelBonus + waveData.waveLevel + (int)playerEnemyLevelBonus).CalculateValues().SetActive(true);
            LevelEnemies.Add(instance);
            EnemiesLeft++;

            Instantiate(enemy.enemyVisual, instanceGO.transform.position + enemy.enemyVisualOffset, Quaternion.identity, instanceGO.transform);
            OnEnemyCreated.Invoke(instance);
        }
        FinishedSpawning = true;
    }

    public EnemyInstance GetEnemy()
    {
        return LevelEnemies[0];
    }

    public void OnEnemyDead(EnemyInstance enemy)
    {
        PlayerResources.Instance.AddMoney(enemy.GetCost());

        OnEnemyKilled.Invoke(enemy);
        LevelEnemies.Remove(enemy);
        EnemiesLeft--;
        if (EnemiesLeft == 0)
            OnAllEnemiesDead();
    }

    void OnAllEnemiesDead()
    {
        FinishedSpawning = false;
        OnAllEnemiesKilled.Invoke();
    }
}
