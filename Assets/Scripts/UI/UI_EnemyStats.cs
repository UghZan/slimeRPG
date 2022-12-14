using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UI_EnemyStats : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] GameObjectPool windowPool;
    [SerializeField] Canvas canvas;

    private void Start()
    {
        EnemyManager.OnEnemyCreated.AddListener(CreateStatsWindowForEnemy);
    }

    void CreateStatsWindowForEnemy(EnemyInstance enemy)
    {
        GameObject window = windowPool.Pool.Get();
        window.transform.SetParent(canvas.transform);
        UI_EnemyStatsWindow windowStats = window.GetComponent<UI_EnemyStatsWindow>();
        windowStats.SetParentPool(windowPool);
        windowStats.Init(Camera.main, enemy);
    }
}
