using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class EnemyInstance : MonoBehaviour
{
    private IObjectPool<GameObject> parentPool;
    [SerializeField] private EnemyManager parentManager;
    [SerializeField] private EnemyData enemyData;

    public float enemyMaxHP { get; private set; }
    public float enemyCurrentHP { get; private set; }
    public float enemyCurrentDamage { get; private set; }
    public float enemyCurrentSpeed { get; private set; }
    public float enemyCurrentAttackSpeed { get; private set; }
    public float enemyLvl { get; private set; }

    private int levelBonus = 0;
    private bool isActive, isAttacking;

    public static UnityEvent<float, EnemyInstance> OnDamageReceived = new UnityEvent<float, EnemyInstance>();

    public EnemyInstance CalculateValues()
    {
        enemyLvl = enemyData.enemyStartingLvl + levelBonus;
        enemyMaxHP = enemyData.enemyHP + enemyLvl * enemyData.enemyHPGainPerLevel;
        enemyCurrentHP = enemyMaxHP;
        enemyCurrentDamage = enemyData.enemyDamage + enemyLvl * enemyData.enemyDamageGainPerLevel;
        enemyCurrentAttackSpeed = enemyData.enemyAttackSpeed + enemyLvl * enemyData.enemyAttackSpeedGainPerLevel;
        enemyCurrentSpeed = enemyData.enemySpeed + enemyLvl * enemyData.enemySpeedGainPerLevel;
        return this;
    }

    public ulong GetCost()
    {
        return (ulong)(enemyData.enemyCoinValue + enemyLvl * enemyData.enemyCoinValueGainPerLevel);
    }

    public bool GetActive() => isActive;
    public EnemyData GetData() => enemyData;

    public EnemyInstance SetData(EnemyData _enemyData)
    {
        enemyData = _enemyData;
        return this;
    }
    public EnemyInstance SetLevel(int level)
    {
        levelBonus = level;
        return this;
    }

    public EnemyInstance SetActive(bool status)
    {
        isActive = status;
        return this;
    }

    public EnemyInstance SetParentPool(IObjectPool<GameObject> _parentPool)
    {
        parentPool = _parentPool;
        return this;
    }
    
    public EnemyInstance SetParentManager(EnemyManager _parentManager)
    {
        parentManager = _parentManager;
        return this;
    }

    public void Update()
    {
        if(isActive)
        {
            if(!isAttacking)
                transform.Translate(-Vector3.right * enemyCurrentSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerAttackZone"))
        {
            isAttacking = true;
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (isAttacking)
        {
            yield return new WaitForSeconds(enemyCurrentAttackSpeed);
            PlayerHealth.Instance.UpdateHealth(-enemyCurrentDamage);
        }

    }

    public void DamageEnemy(float damage)
    {
        OnDamageReceived.Invoke(damage, this);
        enemyCurrentHP -= damage;
        if(enemyCurrentHP <= 0)
        {
            isAttacking = false;
            SetActive(false);
            parentManager.OnEnemyDead(this);
            parentPool.Release(gameObject);
        }
    }
}
