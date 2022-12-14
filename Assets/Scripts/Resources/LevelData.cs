using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Level Data")]
public class LevelData : ScriptableObject
{
    public EnemyWaveData[] levelWaves;
    public string levelName;
    public int levelLvlBonus;
    public int levelLvlGrowthPerWave;
}

[System.Serializable]
public class EnemyWaveData
{
    public EnemyData[] waveEnemies;
    public int waveLevel;
}
