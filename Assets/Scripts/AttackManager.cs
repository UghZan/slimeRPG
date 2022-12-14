using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerStats stats;
    [SerializeField] LevelManager levelManager;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] GameObject player;
    [SerializeField] GameObjectPool projectilePool;

    float cachedAttackSpeed, cachedAttackPower, cachedProjSpeed;
    EnemyInstance cachedEnemy;
    float attackTimer = 0.0f;
    [SerializeField] float attackDistance;

    bool shouldAttack = false;

    public void Init()
    {
        LevelManager.OnWaveStart.AddListener(() =>
        {
            shouldAttack = true;
            cachedEnemy = null;//so that it works correctly with pooling
        });
        LevelManager.OnWaveEnd.AddListener(() => shouldAttack = false);
        stats.OnStatsUpdated.AddListener(CacheStats);
        CacheStats();
    }

    void CacheStats()
    {
        cachedAttackSpeed = stats.GetStat("attack_speed").GetCurrentStatValue();
        cachedAttackPower = stats.GetStat("attack_power").GetCurrentStatValue();
        cachedProjSpeed = stats.GetStat("projectile_speed").GetCurrentStatValue();
    }

    private void Update()
    {
        if (!shouldAttack) return;

        attackTimer += Time.deltaTime;
        if (attackTimer > 1 / cachedAttackSpeed)
        {
            Attack();
            attackTimer = 0;
        }
    }

    void Attack()
    {
        if (!enemyManager.FinishedSpawning) return;
        if (cachedEnemy == null || !cachedEnemy.GetActive())
        {
            cachedEnemy = FindClosestEnemy();
        }
        if (cachedEnemy == null) return;

        GameObject projectile = projectilePool.Pool.Get();
        Transform projTransform = projectile.transform;
        StartCoroutine(ProjectileMovement(projTransform));
    }

    EnemyInstance? FindClosestEnemy()
    {
        List<EnemyInstance> enemies = enemyManager.LevelEnemies;

        float minDistance = 10000;
        EnemyInstance? pick = null;
        foreach (EnemyInstance instance in enemies)
        {
            float distance = Mathf.Abs(instance.transform.position.x - player.transform.position.x);
            if (distance > attackDistance) continue;
            if (distance < minDistance)
            {
                pick = instance;
                minDistance = distance;
            }
        }
        return pick;
    }

    IEnumerator ProjectileMovement(Transform proj)
    {
        Vector3 startPos = player.transform.position;
        Vector3 endPos = cachedEnemy.transform.position;
        float speed = cachedProjSpeed;
        float progress = 0;
        while (progress < 1f)
        {
            proj.transform.position = Vector3.Slerp(startPos, endPos, progress);
            progress += Time.deltaTime * speed;
            yield return null;
        }
        cachedEnemy.DamageEnemy(cachedAttackPower);
        projectilePool.Pool.Release(proj.gameObject);
        yield return null;
    }
}
