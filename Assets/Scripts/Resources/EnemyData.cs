using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public GameObject enemyVisual;
    public Vector3 enemyVisualOffset; //considering that we instantiate enemy visual for it, we need to take it's size into account
    public float enemyUIStatsOffset; //for making sure that enemy's model doesn't overlap with it's stats
    //public ParticleSystem hitEffect;

    [Header("Enemy Stats")]
    public int enemyStartingLvl;
    [Space(8)]
    public float enemyHP;
    public float enemyHPGainPerLevel;
    [Space(8)]
    public float enemyDamage;
    public float enemyDamageGainPerLevel;
    [Space(8)]
    public float enemySpeed;
    public float enemySpeedGainPerLevel;
    [Space(8)]
    public float enemyAttackSpeed;
    public float enemyAttackSpeedGainPerLevel;
    [Space(8)]
    public float enemyCoinValue;
    public float enemyCoinValueGainPerLevel;
}
